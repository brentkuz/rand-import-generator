using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandImportGenerator.Core.Utility.Validation
{
    public interface IValidationHelper
    {
        bool IsModelValid(object model, out ICollection<ValidationResult> results);
    }
}
