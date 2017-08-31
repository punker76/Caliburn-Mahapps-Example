namespace CaliburnMefBoostrapperPoc.Services
{
    using CaliburnMefBoostrapperPoc.ViewModels;

    /// <summary>
    /// ViewModel Controller Interface
    /// </summary>
    public interface IViewModelController
    {
        #region Methods

        /// <summary>
        /// Gets the first view model.
        /// </summary>
        /// <returns>First view model.</returns>
        ListItemViewModel GetListItemViewModel();

        #endregion
    }
}