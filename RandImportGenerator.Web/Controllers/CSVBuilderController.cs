﻿using RandImportGenerator.Core;
using RandImportGenerator.Core.Logic.Builders;
using RandImportGenerator.Core.Logic.FileWriters;
using RandImportGenerator.Web.Models;
using RandImportGenerator.Web.Models.CSVBuilder;
using RandImportGenerator.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace RandImportGenerator.Web.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class CSVBuilderController : Controller
    {
        private CSVImportBuilder builder;
        private TempDataWrapper tempDataWrapper;

        public CSVBuilderController(IImportBuilderFactory builderFactory, IWriter writer)
        {
            builder = (CSVImportBuilder)builderFactory.GetImportBuilder(FileType.CSV);
            if (writer is TempDataWriter)
            {
                tempDataWrapper = new TempDataWrapper(TempData);
                ((TempDataWriter)writer).TempDataWrapper = tempDataWrapper;
            }
            builder.SetWriter(writer);
        }

        [HttpPost]
        public JsonResult CreateFile(CSVImportDefinitionDTO dto)
        {
            try
            {
                builder.SetRowCount(dto.RowCount);
                builder.SetDelimiter(',');
                builder.SetQuoteCharacter(QuoteType.Double);
                builder.AddColumns(dto.AutoIncremented);
                builder.AddColumns(dto.Randomized);
                builder.AddColumns(dto.Dependent);
                builder.AddColumns(dto.Static);
                
                builder.BuildAndSaveFile();

                return Json(new ApiResponse(true, "") { Data = new { FileId = tempDataWrapper.Handle } });
            }
            catch(Exception ex)
            {                
                return Json(new ApiResponse(false, ex.Message ?? ex.InnerException?.Message));
            }
        }

        public FileResult DownloadFile(Guid id)
        {
            byte[] fileBytes = TempData[id.ToString()] as byte[];
            return File(fileBytes, Constants.CSVMimeType, id + Constants.CSVExtenstion); 
        }
    }
}