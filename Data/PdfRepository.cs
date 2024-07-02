using PdfMicroService.Models;

namespace PdfMicroService.Data
{
    public class PdfRepository
    {
        private readonly string _basePath;

        public PdfRepository(string basePath)
        {
            _basePath = basePath;
            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }
        }

        public async Task SalvaDocumento(DocumentoPdf documento)
        {
            string filePath = Path.Combine(_basePath, $"{documento.Id}.pdf");
            await File.WriteAllBytesAsync(filePath, documento.Contenuto);
        }

        public async Task<DocumentoPdf> OttieniDocumento(string id)
        {
            string filePath = Path.Combine(_basePath, $"{id}.pdf");
            if (File.Exists(filePath))
            {
                var content = await File.ReadAllBytesAsync(filePath);
                return new DocumentoPdf
                {
                    Id = id,
                    Contenuto = content
                    
                    
                };
            }
            return null;
        }
    }
}
