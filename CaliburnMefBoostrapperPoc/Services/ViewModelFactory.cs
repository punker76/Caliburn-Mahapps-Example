namespace CaliburnMefBoostrapperPoc.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using Caliburn.Micro;

    /// <summary>
    /// ViewModel Factory Wrapper
    /// </summary>
    /// <typeparam name="T">IScreen template.</typeparam>
    /// <seealso cref="CaliburnMefBoostrapperPoc.Services.IViewModelFactory{T}" />
    [Export(typeof(IViewModelFactory<>))]
    public class ViewModelFactor<T> : IViewModelFactory<T>
        where T : IScreen
    {
        #region Fields

        private ExportFactory<T> _exportFactory;
        private Dictionary<T, ExportLifetimeContext<T>> _exportLifeTimeContexts;
        private object _lock;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelFactoryWrapper" /> class.
        /// </summary>
        /// <param name="exportFactory">The export factory.</param>
        [ImportingConstructor]
        public ViewModelFactor(ExportFactory<T> exportFactory)
        {
            this._lock = new object();
            this._exportFactory = exportFactory;
            this._exportLifeTimeContexts = new Dictionary<T, ExportLifetimeContext<T>>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Shows the view model.
        /// </summary>
        /// <returns>ViewModel.</returns>
        public T CreateViewModel()
        {
            var export = this._exportFactory.CreateExport();
            lock (this._lock)
            {
                this._exportLifeTimeContexts.Add(export.Value, export);
            }

            export.Value.Deactivated += ViewModel_Deactivated;
            return export.Value;
        }

        /// <summary>
        /// Handles the Deactivated event of the ViewModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DeactivationEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void ViewModel_Deactivated(object sender, DeactivationEventArgs e)
        {
            if (e.WasClosed)
            {
                var viewModel = (T)sender;
                if (viewModel == null)
                {
                    throw new InvalidOperationException("Must be a viewmodel.");
                }
                lock (this._lock)
                {
                    var export = this._exportLifeTimeContexts[viewModel];
                    if (export == null)
                    {
                        throw new InvalidOperationException("Export not found.");
                    }

                    viewModel.Deactivated -= this.ViewModel_Deactivated;
                    export.Dispose();
                    this._exportLifeTimeContexts.Remove(viewModel);
                }
            }
        }

        #endregion
    }
}