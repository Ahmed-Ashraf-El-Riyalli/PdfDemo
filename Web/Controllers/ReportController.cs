using Microsoft.AspNetCore.Mvc;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;


        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }


        [HttpGet]
        public IActionResult GetReport()
        {
            string contentType = "application/octet-stream";
            string fileDownloadName = "simplePdf.pdf";

            byte[] pdfFile = _reportService.GeneratePdfReport();

            return File(pdfFile, contentType, fileDownloadName);
        }

        [HttpGet]
        public IActionResult Hi()
        {
            return Ok(new { x = "Hi" });
        }
    }
}
