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
            if (pdfFile == null || pdfFile.Length == 0)
                return BadRequest("No file");

            string uploads =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/temp");

            Directory.CreateDirectory(uploads);

            string inputPath =
                Path.Combine(
                    uploads,
                    Guid.NewGuid() + ".pdf");

            string outputPath =
                Path.Combine(
                    uploads,
                    Guid.NewGuid() + "-compressed.pdf");

            using (var stream =
                new FileStream(inputPath, FileMode.Create))
            {
                pdfFile.CopyTo(stream);
            }

            string gsPath = @"C:\Program Files\gs\gs10.07.0\bin\gswin64c.exe";

            string args =
            $"-sDEVICE=pdfwrite " +
            $"-dCompatibilityLevel=1.4 " +
            $"-dPDFSETTINGS=/screen " +
            $"-dNOPAUSE " +
            $"-dQUIET " +
            $"-dBATCH " +
            $"-sOutputFile=\"{outputPath}\" " +
            $"\"{inputPath}\"";

            Process process = new Process();

            process.StartInfo.FileName = gsPath;
            process.StartInfo.Arguments = args;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;

            process.Start();
            process.WaitForExit();


            byte[] fileBytes =
                System.IO.File.ReadAllBytes(outputPath);

            System.IO.File.Delete(inputPath);
            System.IO.File.Delete(outputPath);

            return File(
                fileBytes,
                "application/pdf",
                "compressed.pdf");
        }
    }
}