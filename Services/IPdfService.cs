using PdfMicroService.Models;

namespace PdfMicroService.Services
{
    public interface IPdfService
    {
        Task<string> CaricaEsalvaPdf(string nome, byte[] contenuto);
        Task<string> OttieniCampiPdf(string id);
        Task<string> CompilaPdf(string id, string jsonDati);


    }
}
