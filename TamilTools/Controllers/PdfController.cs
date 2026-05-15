using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
                    return Content("No PDF uploaded");
                }

                string uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/temp"
                );

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string inputPath = Path.Combine(
                    uploadsFolder,
                    Guid.NewGuid() + ".pdf"
                );

                string outputPath = Path.Combine(
                    uploadsFolder,
                    Guid.NewGuid() + "-compressed.pdf"
                );

                using (var stream = new FileStream(inputPath, FileMode.Create))
                {
                    pdfFile.CopyTo(stream);
                }

                string gsPath =
                    @"C:\Program Files\gs\gs10.07.0\bin\gswin64c.exe";

                string args =
                    $"-sDEVICE=pdfwrite " +
                    $"-dCompatibilityLevel=1.4 " +
                    $"-dPDFSETTINGS=/screen " +
                    $"-dNOPAUSE -dQUIET -dBATCH " +
                    $"-sOutputFile=\"{outputPath}\" " +
                    $"\"{inputPath}\"";

                Process process = new Process();

                process.StartInfo.FileName = gsPath;
                process.StartInfo.Arguments = args;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;

                process.Start();
                process.WaitForExit();

                byte[] fileBytes = System.IO.File.ReadAllBytes(outputPath);

                return File(
                    fileBytes,
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