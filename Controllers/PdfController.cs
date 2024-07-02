using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfMicroService.Services;
using System.Text.Json;

namespace PdfMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly PdfService _pdfService;

        public PdfController(PdfService pdfService)
        {
            _pdfService = pdfService;
        }

        [HttpPost("carica")]
        [Consumes("multipart/form-data")]
        //[FromForm]
        public async Task<IActionResult> CaricaPdf( IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var id = await _pdfService.CaricaEsalvaPdf(file.FileName, ms.ToArray());
            return Ok(new { Id = id });
        }

        [HttpGet("{id}/campi")]
        public async Task<IActionResult> OttieniCampi(string id)
        {
            var campi = await _pdfService.OttieniCampiPdf(id);
            return Ok(campi);
        }

        [HttpPost("{id}/compila")]
        public async Task<IActionResult> CompilaPdf(string id, [FromBody] JsonElement jsonDati)
        {
            var pdfCompilato = await _pdfService.CompilaPdf(id, jsonDati.ToString());
            return Ok(new { PdfCompilato = pdfCompilato });
        }
    }
}