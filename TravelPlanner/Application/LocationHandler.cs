using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Globalization;
using System.IO;
using System.Text;
using TravelPlanner.Domain;
using TravelPlanner.Infrastructure.Excel;
using TravelPlanner.Properties;

namespace TravelPlanner.Application
{
    public class LocationHandler : ILocationHandler
    {
        private readonly Dictionary<string, GeoCoordinate> citiesCoordinates;
        private readonly Dictionary<string, string> citiesInEnglish;

        public LocationHandler()
        {
            var citiesInfo = ExcelReader.TableToListOfRows(new MemoryStream(Resources.countries),
                Encoding.GetEncoding("windows-1251"));
            citiesCoordinates = new Dictionary<string, GeoCoordinate>();
            citiesInEnglish = new Dictionary<string, string>();
            foreach (var entry in citiesInfo)
            {
                var city = (string)entry["city"];
                var lat = Convert.ToDouble(entry["lat"], CultureInfo.InvariantCulture);
                var lon = Convert.ToDouble(entry["lng"], CultureInfo.InvariantCulture);
                if (!citiesCoordinates.ContainsKey(city))
                    citiesCoordinates.Add(city, new GeoCoordinate(lat, lon));
                if (!citiesInEnglish.ContainsKey(city))
                    citiesInEnglish.Add(city, (string) entry["city_en"]);
            }
        }

        public Location GetLocationByName(string name)
        {
            return new Location(citiesCoordinates[name], name);
        }

        public IEnumerable<string> GetLocationsNames()
        {
            return citiesCoordinates.Keys;
        }

        public bool CityExists(string name)
        {
            return citiesCoordinates.ContainsKey(name);
        }

        public string GetCityNameInEnglish(string name)
        {
            return citiesInEnglish[name];
        }
    }
}
