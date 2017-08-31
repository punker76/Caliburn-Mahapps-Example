namespace CaliburnMefBoostrapperPoc.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Caliburn.Micro;

    /// <summary>
    /// Validation Screen
    /// </summary>
    public class ValidationScreen : Screen, IDataErrorInfo
    {
        #region Fields

        private string _error;
        private Dictionary<string, string> _errors;

        #endregion

        #region Constructors

        public ValidationScreen()
        {
            this._errors = new Dictionary<string, string>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the <see cref="System.String"/> with the specified column name.
        /// </summary>
        /// <value>
        /// The <see cref="System.String"/>.
        /// </value>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get
            {
                System.ComponentModel.DataAnnotations.ValidationResult validationResult = this.Validate(columnName);
                if (validationResult != System.ComponentModel.DataAnnotations.ValidationResult.Success)
                {
                    this.AddError(columnName, validationResult.ErrorMessage);
                }
                else
                {
                    this.RemoveError(columnName);
                }

                return this.Error;
            }
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        public string Error
        {
            get
            {
                return this._error;
            }

            set
            {
                if (this._error != value)
                {
                    this._error = value;
                    this.NotifyOfPropertyChange();
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="value">The error string.</param>
        protected void AddError(string columnName, string value)
        {
            if (this._errors.ContainsKey(columnName))
            {
                this._errors[columnName] = value;
            }
            else
            {
                this._errors.Add(columnName, value);
            }

            this.Error = value;
        }

        /// <summary>
        /// Removes the error.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void RemoveError(string columnName)
        {
            this._errors.Remove(columnName);

            if (!string.IsNullOrEmpty(this.Error))
            {
                this.Error = string.Empty;
            }
            else
            {
                this.NotifyOfPropertyChange(nameof(this.Error));
            }
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        /// Returs the ValidationResult.
        /// </returns>
        protected System.ComponentModel.DataAnnotations.ValidationResult Validate(string propertyName)
        {
            var value = this.GetType().GetProperty(propertyName).GetValue(this, null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>(1);
            var context = new ValidationContext(this, null, null) { MemberName = propertyName };
            var success = Validator.TryValidateProperty(value, context, results);
            if (success)
            {
                return System.ComponentModel.DataAnnotations.ValidationResult.Success;
            }
            else
            {
                return results[0];
            }
        }

        #endregion
    }
}