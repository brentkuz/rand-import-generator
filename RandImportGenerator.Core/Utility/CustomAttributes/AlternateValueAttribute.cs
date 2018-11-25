using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandImportGenerator.Core.Utility.CustomAttributes
{
    public class AlternateValueAttribute : Attribute
    {
        public AlternateValueAttribute(object value)
        {
            Value = value;
        }
        public object Value { get; set; }
    }
}
