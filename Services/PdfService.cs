using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Text;
using PdfMicroService.Models;

namespace PdfMicroService.Services
{
    public class PdfService : IPdfService
    {
        private readonly string _uploadPath;

        public PdfService(IConfiguration configuration)
        {
            _uploadPath = configuration["UploadPath"] ?? "uploads";
            Directory.CreateDirectory(_uploadPath);
        }

        public async Task<Models.PdfDocument> UploadAndSaveAsync(IFormFile file)
        {
            var id = Guid.NewGuid().ToString();
            var fileName = $"{id}_{file.FileName}";
            var filePath = Path.Combine(_uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new Models.PdfDocument
            {
                Id = id,
                FileName = fileName,
                FilePath = filePath,
                UploadDate = DateTime.UtcNow,
                AvailableFields = GetAvailableFields(filePath),
                Checkboxes = GetCheckboxes(filePath)
            };
        }

        public Task<Models.PdfDocument> GetPdfInfoAsync(string id)
        {
            // Implementazione per ottenere le informazioni del PDF
            throw new NotImplementedException();
        }

        public Task<Dictionary<string, object>> GetFieldsAsync(string id)
        {
            // Implementazione per ottenere i campi del PDF
            throw new NotImplementedException();
        }

        public Task<string> FillPdfAndGetBase64Async(string id, Dictionary<string, object> fieldValues)
        {
            // Implementazione per compilare il PDF e restituirlo come base64
            throw new NotImplementedException();
        }

        private List<string> GetAvailableFields(string filePath)
        {
            // Implementazione per ottenere i campi disponibili
            throw new NotImplementedException();
        }

        private List<string> GetCheckboxes(string filePath)
        {
            // Implementazione per ottenere le checkbox
            throw new NotImplementedException();
        }

        Task<Models.PdfDocument> IPdfService.UploadAndSaveAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }

        Task<Models.PdfDocument> IPdfService.GetPdfInfoAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
