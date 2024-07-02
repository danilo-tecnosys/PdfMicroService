using iText.Forms.Fields;
using iText.Forms;
using iText.Kernel.Pdf;
using PdfMicroService.Models;
using System.Text.Json;
using PdfMicroService.Data;
using PdfMicroService.Helpers;

namespace PdfMicroService.Services
{
    public class PdfService : IPdfService
    {
        private readonly PdfRepository _repository;
        private readonly PdfHelper _pdfHelper;

        public PdfService(PdfRepository repository, PdfHelper pdfHelper)
        {
            _repository = repository;
            _pdfHelper = pdfHelper;
        }

        public async Task<string> CaricaEsalvaPdf(string nome, byte[] contenuto)
        {
            var id = Guid.NewGuid().ToString();
            var documento = new DocumentoPdf
            {
                Id = id,
                Nome = nome,
                Contenuto = contenuto,
                DataCaricamento = DateTime.UtcNow
            };

            await _repository.SalvaDocumento(documento);
            return id;
        }

        public async Task<string> OttieniCampiPdf(string id)
        {
            var documento = await _repository.OttieniDocumento(id);
            var campi = _pdfHelper.EstraiCampi(documento.Contenuto);
            return JsonSerializer.Serialize(campi);
        }

        public async Task<string> CompilaPdf(string id, string jsonDati)
        {
            var documento = await _repository.OttieniDocumento(id);
            var pdfCompilato = _pdfHelper.CompilaPdf(documento.Contenuto, jsonDati);
            return Convert.ToBase64String(pdfCompilato);
        }
    }





}
