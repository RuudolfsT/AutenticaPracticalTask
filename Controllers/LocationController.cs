using AutenticaPracticalTask.Models;
using Microsoft.AspNetCore.Mvc;
using AutenticaPracticalTask.Properties;

namespace AutenticaPracticalTask.Controllers
{
    public class LocationController : Controller
    {
        public JsonResult GetLocation(string cityName)
        {
            if (cityName == string.Empty || cityName == null)
            {
                string errorMessage = Resources.CityNameCannotBeEmpty;
                return Json(new { error = errorMessage });
            }

            var cityNameToCompare = cityName.ToLower();
            Location? location = GetLocationFromFile(cityNameToCompare);
            
            if (location == null)
            {
                string errorMessage = Resources.CityNotFound;
                return Json(new { error = errorMessage });
            }

            return Json(location);
        }

        public IActionResult Index()
        {
            ExtremeLocationVM extremeLocationVM = new ExtremeLocationVM();
            extremeLocationVM.Locations = GetExtremeLocations();

            return View(extremeLocationVM);
        }

        private static Location ParseLocationFromFile(string line)
        {
            string[] parts = line.Split(';');
            string name = parts[2].Replace("#", string.Empty);
            double longitude = Convert.ToDouble(parts[8].Replace("#", string.Empty));
            double latitude = Convert.ToDouble(parts[9].Replace("#", string.Empty));

            Location location = new Location
            {
                Name = name,
                Longitude = longitude,
                Latitude = latitude
            };

            return location;
        }


        private static List<Location> GetExtremeLocations()
        {
            string fileName = Resources.CSVFileName;
            List<Location> allLocations = new List<Location>();

            var validLocations = System.IO.File.ReadLines(fileName)
                .Skip(1)
                .Select(ParseLocationFromFile);

            Location location;
            validLocations = validLocations.OrderBy(x => x.Longitude);
            location = validLocations.Last(); // North
            location.Description = Resources.NorthPoint;
            allLocations.Add(location);

            location = validLocations.First(); // South
            location.Description = Resources.SouthPoint;
            allLocations.Add(location);

            validLocations = validLocations.OrderBy(x => x.Latitude);
            location = validLocations.Last(); // East
            location.Description = Resources.EastPoint;
            allLocations.Add(location);

            location = validLocations.First(); // West
            location.Description = Resources.WestPoint;
            allLocations.Add(location);

            return allLocations;
        }

        private static Location? GetLocationFromFile(string cityToFind)
        {
            string fileName = Resources.CSVFileName;

            var location = System.IO.File.ReadLines(fileName)
                .Skip(1)
                .Select(ParseLocationFromFile)
                .Where(p => p.Name.ToLower() == cityToFind)
                .FirstOrDefault();
            
            return location;
        }
    }
}