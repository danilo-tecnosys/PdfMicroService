using PdfMicroService.Models;

namespace PdfMicroService.Data
{
    public class PdfRepository
    {
        private readonly Dictionary<string, DocumentoPdf> _documenti = new Dictionary<string, DocumentoPdf>();

        public Task SalvaDocumento(DocumentoPdf documento)
        {
            _documenti[documento.Id] = documento;
            return Task.CompletedTask;
        }

        public Task<DocumentoPdf> OttieniDocumento(string id)
        {
            if (_documenti.TryGetValue(id, out var documento))
            {
                return Task.FromResult(documento);
            }
            return Task.FromResult<DocumentoPdf>(null);
        }
    }
}
