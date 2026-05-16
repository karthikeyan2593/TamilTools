using Microsoft.AspNetCore.Mvc;

namespace TamilTools.Controllers
{
    public class UnitController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Convert(double value, string fromUnit, string toUnit, string category)
        {
            double result = 0;

            switch (category)
            {
                case "length":
                    result = ConvertLength(value, fromUnit, toUnit);
                    break;
                case "weight":
                    result = ConvertWeight(value, fromUnit, toUnit);
                    break;
                case "temperature":
                    result = ConvertTemperature(value, fromUnit, toUnit);
                    break;
                case "volume":
                    result = ConvertVolume(value, fromUnit, toUnit);
                    break;
            }

            ViewBag.Result = Math.Round(result, 4);
            ViewBag.FromUnit = fromUnit;
            ViewBag.ToUnit = toUnit;
            ViewBag.Value = value;
            ViewBag.Category = category;
            ViewBag.InputValue = value;

            return View("Index");
        }

        private double ConvertLength(double value, string from, string to)
        {
            // Convert to meters first
            double meters = from switch
            {
                "inch" => value * 0.0254,
                "foot" => value * 0.3048,
                "yard" => value * 0.9144,
                "mile" => value * 1609.344,
                "cm" => value * 0.01,
                "meter" => value,
                "km" => value * 1000,
                _ => value
            };

            return to switch
            {
                "inch" => meters / 0.0254,
                "foot" => meters / 0.3048,
                "yard" => meters / 0.9144,
                "mile" => meters / 1609.344,
                "cm" => meters / 0.01,
                "meter" => meters,
                "km" => meters / 1000,
                _ => meters
            };
        }

        private double ConvertWeight(double value, string from, string to)
        {
            // Convert to kg first
            double kg = from switch
            {
                "pound" => value * 0.453592,
                "ounce" => value * 0.0283495,
                "ton_us" => value * 907.185,
                "kg" => value,
                "gram" => value * 0.001,
                _ => value
            };

            return to switch
            {
                "pound" => kg / 0.453592,
                "ounce" => kg / 0.0283495,
                "ton_us" => kg / 907.185,
                "kg" => kg,
                "gram" => kg / 0.001,
                _ => kg
            };
        }

        private double ConvertTemperature(double value, string from, string to)
        {
            // Convert to Celsius first
            double celsius = from switch
            {
                "fahrenheit" => (value - 32) * 5 / 9,
                "celsius" => value,
                "kelvin" => value - 273.15,
                _ => value
            };

            return to switch
            {
                "fahrenheit" => celsius * 9 / 5 + 32,
                "celsius" => celsius,
                "kelvin" => celsius + 273.15,
                _ => celsius
            };
        }

        private double ConvertVolume(double value, string from, string to)
        {
            // Convert to liters first
            double liters = from switch
            {
                "gallon_us" => value * 3.78541,
                "quart" => value * 0.946353,
                "pint" => value * 0.473176,
                "cup" => value * 0.236588,
                "fl_oz" => value * 0.0295735,
                "liter" => value,
                "ml" => value * 0.001,
                _ => value
            };

            return to switch
            {
                "gallon_us" => liters / 3.78541,
                "quart" => liters / 0.946353,
                "pint" => liters / 0.473176,
                "cup" => liters / 0.236588,
                "fl_oz" => liters / 0.0295735,
                "liter" => liters,
                "ml" => liters / 0.001,
                _ => liters
            };
        }
    }
}