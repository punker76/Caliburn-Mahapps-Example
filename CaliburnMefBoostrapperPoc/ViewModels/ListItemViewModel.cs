namespace CaliburnMefBoostrapperPoc.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ListItemViewModel : ValidationScreen
    {
        #region Fields

        private string _cell1;
        private string _cell2;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ListItemViewModel"/> class.
        /// </summary>
        [ImportingConstructor]
        public ListItemViewModel()
            : base()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the cell1.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Cell1
        {
            get
            {
                return this._cell1;
            }

            set
            {
                if (this._cell1 != value)
                {
                    this._cell1 = value;
                    this.NotifyOfPropertyChange();
                }
            }
        }

        /// <summary>
        /// Gets or sets the cell2.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Cell2
        {
            get
            {
                return this._cell2;
            }

            set
            {
                if (this._cell2 != value)
                {
                    this._cell2 = value;
                    this.NotifyOfPropertyChange();
                }
            }
        }

        #endregion
    }
}