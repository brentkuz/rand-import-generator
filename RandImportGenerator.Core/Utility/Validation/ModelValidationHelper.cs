using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandImportGenerator.Core.Utility.Validation
{
    public class ModelValidationHelper : IValidationHelper
    {
        public bool IsModelValid(object model, out ICollection<ValidationResult> results)
        {
            results = new List<ValidationResult>();
            ValidationContext vCtx = new ValidationContext(model);
            return Validator.TryValidateObject(model, vCtx, results, true);
        }
    }
}
