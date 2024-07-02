using PdfMicroService.Models;

namespace PdfMicroService.Services
{
    public interface IPdfService
    {
        Task<PdfDocument> UploadAndSaveAsync(IFormFile file);
        Task<PdfDocument> GetPdfInfoAsync(string id);
        Task<Dictionary<string, object>> GetFieldsAsync(string id);
        Task<string> FillPdfAndGetBase64Async(string id, Dictionary<string, object> fieldValues);
    }
}
