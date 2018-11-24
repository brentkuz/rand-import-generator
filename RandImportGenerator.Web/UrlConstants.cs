using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RandImportGenerator.Web
{
    public class UrlConstants
    {
        public string Home_Index { get; } = "/Data/SomeData";

        //CSVBuilderController
        public string CSVBuilder_CreateFile { get; } = "/CSVBuilder/CreateFile";
        public string CSVBuilder_DownloadFile { get; } = "/CSVBuilder/DownloadFile/";
    }
}