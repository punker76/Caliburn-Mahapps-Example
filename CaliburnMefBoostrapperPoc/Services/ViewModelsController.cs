namespace CaliburnMefBoostrapperPoc.Services
{
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using Caliburn.Micro;
    using CaliburnMefBoostrapperPoc.ViewModels;

    /// <summary>
    /// ViewModel Controller
    /// </summary>
    [Export(typeof(IViewModelController))]
    public class ViewModelsController : IViewModelController
    {
        #region Fields

        private CompositionContainer _container;
        private IWindowManager _windowManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelsController"/> class.
        /// </summary>
        /// <param name="testManagerFactory">The test manager factory wrapper.</param>
        /// <param name="_testFactory">The test factory wrapper.</param>
        [ImportingConstructor]
        public ViewModelsController(
            [Import("RootCompositionContainer")] CompositionContainer container,
            IWindowManager windowManager)
        {
            this._container = container;
            this._windowManager = windowManager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the and get self contained view model.
        /// </summary>
        /// <returns>Self Contained ViewModel</returns>
        public ListItemViewModel GetListItemViewModel()
        {
            return this.CreateViewModel<ListItemViewModel>();
        }

        /// <summary>
        /// Releases the export.
        /// </summary>
        private void ReleaseExport<T>(T viewModel)
            where T : IScreen
        {
            var factory = this._container.GetExportedValue<IViewModelFactory<T>>();
            factory.ReleaseExport(viewModel);
        }

        /// <summary>
        /// Creates the view model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>View model</returns>
        private T CreateViewModel<T>()
            where T : IScreen
        {
            var factory = this._container.GetExportedValue<IViewModelFactory<T>>();
            return factory.CreateViewModel();
        }

        #endregion
    }
}