using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Globalization;

namespace TravelPlanner.Infrastructure.Countries
{
    public class GeographicDatabase
    {
        private readonly Dictionary<string, GeoCoordinate> citiesCoordinates;

        public GeographicDatabase(IEnumerable<Dictionary<string, object>> citiesInfo)
        {
            citiesCoordinates = new Dictionary<string, GeoCoordinate>();
            foreach (var entry in citiesInfo)
            {
                var city = (string) entry["city"];
                var lat = Convert.ToDouble(entry["lat"], CultureInfo.InvariantCulture);
                var lon = Convert.ToDouble(entry["lng"], CultureInfo.InvariantCulture);
                if (!citiesCoordinates.ContainsKey(city))
                    citiesCoordinates.Add(city, new GeoCoordinate(lat, lon));
            }
        }

        public IEnumerable<string> GetAllCities() => citiesCoordinates.Keys;

        public GeoCoordinate GetCityCoordinate(string cityName) => citiesCoordinates[cityName];
    }
}