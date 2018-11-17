using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RandImportGenerator.Web.Utility.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ClientConfigurationAttribute : Attribute
    {
    }
}