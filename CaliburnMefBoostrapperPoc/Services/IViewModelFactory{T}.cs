namespace CaliburnMefBoostrapperPoc.Services
{
    using Caliburn.Micro;
    /// <summary>
    /// ViewModel Factory Wrapper Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IViewModelFactory<T>
        where T : IScreen
    {
        #region Methods

        /// <summary>
        /// Shows the view model.
        /// </summary>
        /// <returns>ViewModel.</returns>
        T CreateViewModel();

        /// <summary>
        /// Releases the export.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        void ReleaseExport(T viewModel);

        #endregion
    }
}