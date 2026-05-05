using Microsoft.AspNetCore.Mvc;

namespace TamilTools.Controllers
{
    public class HoroscopeController : Controller
    {
        private static readonly string[] Nakshatras = {
            "Ashwini","Bharani","Krittika","Rohini","Mrigashira",
            "Ardra","Punarvasu","Pushya","Ashlesha","Magha",
            "Purva Phalguni","Uttara Phalguni","Hasta","Chitra",
            "Swati","Vishakha","Anuradha","Jyeshtha","Mula",
            "Purva Ashadha","Uttara Ashadha","Shravana","Dhanishtha",
            "Shatabhisha","Purva Bhadrapada","Uttara Bhadrapada","Revati"
        };

        private static readonly int[] Gana = {
            0,2,0,0,1,2,0,0,2,2,1,0,0,2,0,2,0,2,2,1,0,0,2,2,1,0,0
        };

        private static readonly int[] Yoni = {
            0,1,2,3,4,5,6,7,8,9,10,11,12,13,6,7,8,9,10,11,12,13,0,1,2,3,4
        };

        private static readonly int[] NakshatraRasi = {
            0,0,1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9,10,10,11,11,0,0,11
        };

        public IActionResult Index()
        {
            ViewBag.Nakshatras = Nakshatras;
            return View();
        }

        [HttpPost]
        public IActionResult Match(int boyNakshatra, int girlNakshatra)
        {
            ViewBag.Nakshatras = Nakshatras;

            int total = 0;

            int boyGana = Gana[boyNakshatra];
            int girlGana = Gana[girlNakshatra];
            if (boyGana == girlGana) total++;

            int boyYoni = Yoni[boyNakshatra];
            int girlYoni = Yoni[girlNakshatra];
            if (boyYoni == girlYoni) total++;

            int boyRasi = NakshatraRasi[boyNakshatra];
            int girlRasi = NakshatraRasi[girlNakshatra];
            if (boyRasi == girlRasi) total++;

            ViewBag.Score = total;
            ViewBag.Result =
                total >= 3 ? "Excellent Match ❤️" :
                total == 2 ? "Good Match 👍" :
                "Not Recommended ❌";

            return View("Index");
        }
    }
}