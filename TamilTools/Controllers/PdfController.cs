using Microsoft.AspNetCore.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TamilTools.Controllers
{
    public class PdfController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Merge()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Convert(List<IFormFile> images)
        {
            if (images == null || images.Count == 0)
            {
                ViewBag.Error = "தயவுசெய்து படங்களை தேர்வு செய்யுங்கள்!";
                return View("Index");
            }

            using var ms = new MemoryStream();
            var document = new Document(PageSize.A4);
            var writer = PdfWriter.GetInstance(document, ms);
            document.Open();

            foreach (var image in images)
            {
                if (image.Length > 0)
                {
                    using var imgStream = new MemoryStream();
                    await image.CopyToAsync(imgStream);
                    var imgBytes = imgStream.ToArray();

                    var pdfImage = iTextSharp.text.Image.GetInstance(imgBytes);
                    pdfImage.ScaleToFit(document.PageSize.Width - 40,
                                       document.PageSize.Height - 40);
                    pdfImage.Alignment = Element.ALIGN_CENTER;

                    document.Add(pdfImage);
                    document.NewPage();
                }
            }



            document.Close();

            return File(ms.ToArray(), "application/pdf", "TamilTools-converted.pdf");
        }
    }
}