namespace CaliburnMefBoostrapperPoc.Services
{
    using Caliburn.Micro;

    /// <summary>
    /// ViewModel Factory Wrapper Interface
    /// </summary>
    /// <typeparam name="T">IScreen template.</typeparam>
    public interface IViewModelFactory<T>
        where T : IScreen
    {
        #region Methods

        /// <summary>
        /// Shows the view model.
        /// </summary>
        /// <returns>ViewModel.</returns>
        T CreateViewModel();

        #endregion
    }
}