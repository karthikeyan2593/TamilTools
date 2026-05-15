using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult Compress()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Compress(IFormFile pdfFile)
        {
            try
            {
                if (pdfFile == null || pdfFile.Length == 0)
                {
                    TempData["Error"] = "Please upload PDF";
                    return RedirectToAction("Compress");
                }

                using var inputStream = pdfFile.OpenReadStream();

                PdfReader reader = new PdfReader(inputStream);

                using var outputStream = new MemoryStream();

                PdfStamper stamper = new PdfStamper(
                    reader,
                    outputStream,
                    PdfWriter.VERSION_1_5
                );

                stamper.FormFlattening = true;

                stamper.Close();
                reader.Close();

                byte[] compressedBytes = outputStream.ToArray();

                Console.WriteLine("Original Size: " + pdfFile.Length);
                Console.WriteLine("Compressed Size: " + compressedBytes.Length);

                return File(
                    compressedBytes,
                    "application/pdf",
                    "compressed.pdf"
                );
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}