using Microsoft.AspNetCore.Mvc;

namespace TamilTools.Controllers
{
    public class BmiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(double height, double weight, string unit)
        {
            double bmi;
            double heightM;
            double weightKg;

            if (unit == "imperial")
            {
                // height in inches, weight in pounds
                heightM = height * 0.0254;
                weightKg = weight * 0.453592;
            }
            else
            {
                // height in cm, weight in kg
                heightM = height / 100;
                weightKg = weight;
            }

            bmi = weightKg / (heightM * heightM);
            bmi = Math.Round(bmi, 1);

            string category;
            string color;

            if (bmi < 18.5)
            {
                category = "Underweight";
                color = "#4fc3f7";
            }
            else if (bmi < 25)
            {
                category = "Normal Weight";
                color = "#66bb6a";
            }
            else if (bmi < 30)
            {
                category = "Overweight";
                color = "#ffa726";
            }
            else
            {
                category = "Obese";
                color = "#ef5350";
            }

            ViewBag.BMI = bmi;
            ViewBag.Category = category;
            ViewBag.Color = color;
            ViewBag.InputHeight = height;
            ViewBag.InputWeight = weight;
            ViewBag.InputUnit = unit;

            return View("Index");
        }
    }
}