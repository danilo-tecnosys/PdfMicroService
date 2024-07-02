using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfMicroService.Services;

namespace PdfMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IPdfService _pdfService;

        public PdfController(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPdf(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            var pdfDocument = await _pdfService.UploadAndSaveAsync(file);
            return Ok(pdfDocument);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPdfInfo(string id)
        {
            var pdfInfo = await _pdfService.GetPdfInfoAsync(id);
            if (pdfInfo == null)
                return NotFound();

            return Ok(pdfInfo);
        }

        [HttpGet("{id}/fields")]
        public async Task<IActionResult> GetPdfFields(string id)
        {
            var fields = await _pdfService.GetFieldsAsync(id);
            if (fields == null)
                return NotFound();

            return Ok(fields);
        }

        [HttpPost("{id}/fill")]
        public async Task<IActionResult> FillPdf(string id, [FromBody] Dictionary<string, object> fieldValues)
        {
            var base64Pdf = await _pdfService.FillPdfAndGetBase64Async(id, fieldValues);
            return Ok(new { Base64Pdf = base64Pdf });
        }
    }
}
