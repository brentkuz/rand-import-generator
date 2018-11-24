using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RandImportGenerator.Web.Models
{
    public class ApiResponse
    {
        public ApiResponse()
        {
        }
        public ApiResponse(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}