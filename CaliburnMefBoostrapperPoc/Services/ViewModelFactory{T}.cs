namespace CaliburnMefBoostrapperPoc.Services
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using Caliburn.Micro;

    /// <summary>
    /// ViewModel Factory Wrapper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="CaliburnMefBoostrapperPoc.Services.IViewModelFactory{T}" />
    [Export(typeof(IViewModelFactory<>))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ViewModelFactory<T> : IViewModelFactory<T>
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
        public ViewModelFactory(ExportFactory<T> exportFactory, IEventAggregator eventAggregator)
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
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ReleaseExport(T viewModel)
        {
            if (viewModel.IsActive)
            {
                viewModel.TryClose();
            }
            else
            {
                this.ReleaseExportForViewModel(viewModel);
            }
        }

        /// <summary>
        /// Handles the Deactivated event of the ViewModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DeactivationEventArgs"/> instance containing the event data.</param>
        private void ViewModel_Deactivated(object sender, DeactivationEventArgs e)
        {
            if (e.WasClosed)
            {
                var viewModel = (T)sender;
                if (viewModel != null)
                {
                    this.ReleaseExportForViewModel(viewModel);
                }
            }
        }

        /// <summary>
        /// Disposes the export for view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <exception cref="InvalidOperationException">Export not found.</exception>
        private void ReleaseExportForViewModel(T viewModel)
        {
            lock (this._lock)
            {
                var export = this._exportLifeTimeContexts[viewModel];
                if (export != null)
                {
                    viewModel.Deactivated -= this.ViewModel_Deactivated;
                    export.Dispose();
                    this._exportLifeTimeContexts.Remove(viewModel);
                }
            }
        }

        #endregion
    }
}