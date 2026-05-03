using Microsoft.AspNetCore.Mvc;

namespace TamilTools.Controllers
{
    public class HoroscopeController : Controller
    {
        // 27 Nakshatras
        private static readonly string[] Nakshatras = {
            "Ashwini", "Bharani", "Krittika", "Rohini", "Mrigashira",
            "Ardra", "Punarvasu", "Pushya", "Ashlesha", "Magha",
            "Purva Phalguni", "Uttara Phalguni", "Hasta", "Chitra",
            "Swati", "Vishakha", "Anuradha", "Jyeshtha", "Mula",
            "Purva Ashadha", "Uttara Ashadha", "Shravana", "Dhanishtha",
            "Shatabhisha", "Purva Bhadrapada", "Uttara Bhadrapada", "Revati"
        };

        // Rasi for each Nakshatra (0-based index)
        private static readonly int[] NakshatraRasi = {
            0,0,1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9,10,10,11,11,0,0,11
        };

        // Gana (0=Deva, 1=Manushya, 2=Rakshasa)
        private static readonly int[] Gana = {
            0,2,0,0,1,2,0,0,2,2,1,0,0,2,0,2,0,2,2,1,0,0,2,2,1,0,0
        };

        // Yoni (Animal symbol 0-13)
        private static readonly int[] Yoni = {
            0,1,2,3,4,5,6,7,8,9,10,11,12,13,6,7,8,9,10,11,12,13,0,1,2,3,4
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
            ViewBag.BoyNakshatra = boyNakshatra;
            ViewBag.GirlNakshatra = girlNakshatra;

            int totalScore = 0;
            int maxScore = 10;
            var results = new List<(string Name, int Score, int Max, string Status)>();

            // 1. Dina Porutham (3 points)
            int dinaVal = ((girlNakshatra - boyNakshatra + 27) % 27) + 1;
            int dinaScore = (dinaVal % 9 == 1 || dinaVal % 9 == 3 || dinaVal % 9 == 5 || dinaVal % 9 == 7) ? 1 : 0;
            results.Add(("Dina", dinaScore, 1, dinaScore == 1 ? "Good" : "Bad"));
            totalScore += dinaScore;

            // 2. Gana Porutham (1 point)
            int boyGana = Gana[boyNakshatra];
            int girlGana = Gana[girlNakshatra];
            int ganaScore = (boyGana == girlGana || boyGana == 0) ? 1 : 0;
            results.Add(("Gana", ganaScore, 1, ganaScore == 1 ? "Good" : "Bad"));
            totalScore += ganaScore;

            // 3. Mahendra Porutham (1 point)
            int mahendraVal = ((girlNakshatra - boyNakshatra + 27) % 27) + 1;
            int mahendraScore = (mahendraVal == 4 || mahendraVal == 7 || mahendraVal == 10 ||
                                 mahendraVal == 13 || mahendraVal == 16 || mahendraVal == 19 ||
                                 mahendraVal == 22 || mahendraVal == 25) ? 1 : 0;
            results.Add(("Mahendra", mahendraScore, 1, mahendraScore == 1 ? "Good" : "Bad"));
            totalScore += mahendraScore;

            // 4. Stri Deergha (1 point)
            int striVal = ((girlNakshatra - boyNakshatra + 27) % 27) + 1;
            int striScore = striVal > 7 ? 1 : 0;
            results.Add(("Stri Deergha", striScore, 1, striScore == 1 ? "Good" : "Bad"));
            totalScore += striScore;

            // 5. Yoni Porutham (1 point)
            int boyYoni = Yoni[boyNakshatra];
            int girlYoni = Yoni[girlNakshatra];
            int yoniScore = (boyYoni == girlYoni) ? 1 : 0;
            results.Add(("Yoni", yoniScore, 1, yoniScore == 1 ? "Good" : "Bad"));
            totalScore += yoniScore;

            // 6. Rasi Porutham (1 point)
            int boyRasi = NakshatraRasi[boyNakshatra];
            int girlRasi = NakshatraRasi[girlNakshatra];
            int rasiScore = (boyRasi == girlRasi || Math.Abs(boyRasi - girlRasi) == 6) ? 1 : 0;
            results.Add(("Rasi", rasiScore, 1, rasiScore == 1 ? "Good" : "Bad"));
            totalScore += rasiScore;

            // 7. Rasiyathipathi (1 point)
            int rasiyathipathiScore = (boyRasi != girlRasi) ? 1 : 0;
            results.Add(("Rasiyathipathi", rasiyathipathiScore, 1, rasiyathipathiScore == 1 ? "Good" : "Bad"));
            totalScore += rasiyathipathiScore;

            // 8. Vasiya Porutham (1 point)
            int vasiyaScore = ((boyRasi + girlRasi) % 2 == 0) ? 1 : 0;
            results.Add(("Vasiya", vasiyaScore, 1, vasiyaScore == 1 ? "Good" : "Bad"));
            totalScore += vasiyaScore;

            // 9. Rajju Porutham (1 point)
            int boyRajju = boyNakshatra % 5;
            int girlRajju = girlNakshatra % 5;
            int rajjuScore = (boyRajju != girlRajju) ? 1 : 0;
            results.Add(("Rajju", rajjuScore, 1, rajjuScore == 1 ? "Good" : "Bad"));
            totalScore += rajjuScore;

            // 10. Vedhai Porutham (1 point)
            int[] vedhaiPairs = { 0, 18, 7, 16, 9, 14, 2, 20, 4, 13, 8, 25, 11, 24, 12, 23, 15, 22, 17, 21, 19, 6, 1, 5, 3, 10, 26 };
            int vedhaiScore = (vedhaiPairs[boyNakshatra] != girlNakshatra) ? 1 : 0;
            results.Add(("Vedhai", vedhaiScore, 1, vedhaiScore == 1 ? "Good" : "Bad"));
            totalScore += vedhaiScore;

            ViewBag.Results = results;
            ViewBag.TotalScore = totalScore;
            ViewBag.MaxScore = maxScore;
            ViewBag.MatchLevel = totalScore >= 8 ? "Excellent" :
                                 totalScore >= 6 ? "Good" :
                                 totalScore >= 4 ? "Average" : "Poor";

            return View("Index");
        }
    }
}