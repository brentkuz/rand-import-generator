using RandImportGenerator.Core.Logic.FileWriters;
using RandImportGenerator.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RandImportGenerator.Web.Utility
{
    public class TempDataWriter : IWriter
    {
        public TempDataWrapper TempDataWrapper { get; set; }
        public void Write(string contents)
        {
            TempDataWrapper.TempData[TempDataWrapper.Handle.ToString()] = Encoding.ASCII.GetBytes(contents);
        }
    }
}