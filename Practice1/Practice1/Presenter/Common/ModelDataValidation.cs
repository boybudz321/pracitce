using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice1.Presenter.Common
{
    public class ModelDataValidation
    {
        public void Validate(Object model)
        {
            string errorMessage = "";
            List<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(model);
            bool isValid = Validator.TryValidateObject(model, context, validationResults, true);
            if(isValid == false)
            {
                foreach(var item in validationResults)
                {
                    errorMessage += item.ErrorMessage + "\n";
                    throw new Exception(errorMessage);
                }
            }
        }
    }
}
