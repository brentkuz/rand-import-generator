using RandImportGenerator.Objects.ImportDefinitions.Columns;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandImportGenerator.Utility.Validation.Attributes
{
    public class CollectionIsNotEmpty : ValidationAttribute
    {
        public CollectionIsNotEmpty()
        {

        }
        public override bool IsValid(object value)
        {
            var cols = (IList)value;
            return cols.Count > 0;
        }
    }
}
