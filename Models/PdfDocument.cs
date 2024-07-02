namespace PdfMicroService.Models
{
    public class PdfDocument
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
        public List<string> AvailableFields { get; set; }
        public List<string> Checkboxes { get; set; }
    }
}
