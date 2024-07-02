using iText.Forms;
using iText.Kernel.Pdf;
using iText.Forms.Fields;

using System.Text.Json;

namespace PdfMicroService.Helpers
{
    public class PdfHelper
    {
        public Dictionary<string, object> EstraiCampi(byte[] pdfBytes)
        {
            var campi = new Dictionary<string, object>();
            using (var reader = new PdfReader(new MemoryStream(pdfBytes)))
            using (var document = new PdfDocument(reader))
            {
                var form = PdfAcroForm.GetAcroForm(document, false);
                if (form != null)
                {
                    foreach (var field in form.GetAllFormFields())
                    {
                        campi[field.Key] = field.Value.GetValueAsString();
                    }
                }
            }
            return campi;
        }

        public byte[] CompilaPdf(byte[] pdfBytes, string jsonDati)
        {
            var dati = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonDati);
            using (var reader = new PdfReader(new MemoryStream(pdfBytes)))
            using (var ms = new MemoryStream())
            {
                using (var writer = new PdfWriter(ms))
                using (var document = new PdfDocument(reader, writer))
                {
                    var form = PdfAcroForm.GetAcroForm(document, true);
                    foreach (var campo in dati)
                    {
                        if (form.GetField(campo.Key) != null)
                        {
                            form.GetField(campo.Key).SetValue(campo.Value.ToString());
                        }
                    }
                }
                return ms.ToArray();
            }
        }
    }

}
