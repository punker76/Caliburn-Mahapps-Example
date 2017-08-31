namespace CaliburnMefBoostrapperPoc.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition;
    using Caliburn.Micro;
    using CaliburnMefBoostrapperPoc.Services;

    /// <summary>
    /// Shell ViewModel
    /// </summary>
    /// <seealso cref="Caliburn.Micro.PropertyChangedBase" />
    /// <seealso cref="CaliburnMefBoostrapperPoc.ViewModels.IShellViewModel" />
    [Export(typeof(IShellViewModel))]
    public class ShellViewModel : Screen, IShellViewModel
    {
        #region Fields

        private IViewModelController _viewModelController;
        private ObservableCollection<ListItemViewModel> _items;
        private ListItemViewModel _selectedItem;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellViewModel"/> class.
        /// </summary>
        /// <param name="viewModelManager">The view model manager.</param>
        [ImportingConstructor]
        public ShellViewModel(IViewModelController viewModelController)
        {
            this._viewModelController = viewModelController;
            this.Items = new ObservableCollection<ListItemViewModel>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or Sets the Display Name
        /// </summary>
        public override string DisplayName
        {
            get => "ShellViewModel";
            set => base.DisplayName = value;
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        public ObservableCollection<ListItemViewModel> Items
        {
            get
            {
                return this._items;
            }

            set
            {
                this._items = value;
            }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public ListItemViewModel SelectedItem
        {
            get
            {
                return this._selectedItem;
            }

            set
            {
                if (this._selectedItem != value)
                {
                    this._selectedItem = value;
                    this.NotifyOfPropertyChange();
                }
            }
        }

        #endregion
    }
}