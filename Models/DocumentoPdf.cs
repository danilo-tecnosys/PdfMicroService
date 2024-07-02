namespace PdfMicroService.Models
{
    public class DocumentoPdf
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public byte[] Contenuto { get; set; }
        public DateTime DataCaricamento { get; set; }
    }
}
