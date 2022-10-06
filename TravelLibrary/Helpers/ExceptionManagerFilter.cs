using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TravelLibrary.Helpers
{
    public class ExceptionManagerFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostEnviroment;
        private readonly IModelMetadataProvider _modelMetaDataProvider;


        public ExceptionManagerFilter(IWebHostEnvironment webHostEnvironment, IModelMetadataProvider modelMetadataProvider)
        {
            _hostEnviroment = webHostEnvironment;
            _modelMetaDataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            context.Result = new JsonResult("something went wrong " + _hostEnviroment.ApplicationName
                + "Exception type: " + context.Exception.GetType());
        }
    }
}

