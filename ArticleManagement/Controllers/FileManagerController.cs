using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AM.Service;
using AM.Service.GoogleDriveService;
using ArticleManagement.Controllers.BaseController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticleManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagerController : BaseControllers
    {
        private readonly IGoogleDriveService _googleDriveService;
        public FileManagerController(IGoogleDriveService googleDriveService,   ILoggerService logService
            ) : base(logService)
        {
            _googleDriveService = googleDriveService;
        }

        [HttpGet]
        public ActionResult GetFiles()
        {
            var data = _googleDriveService.files();
            return JsonResult(data,true);
        }


        [HttpGet("upload")]
        public ActionResult UploadFile()
        {
            var data = _googleDriveService.UploadFile();
            return JsonResult(data, true);
        }


    }
}