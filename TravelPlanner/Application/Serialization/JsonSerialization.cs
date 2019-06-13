using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Text;
using TravelPlanner.Application.Fabrics;
using TravelPlanner.Domain;
using TravelPlanner.Domain.TravelEvents;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Application.Serialization
{
    public class JsonSerialization : ISerialization
    {
        private readonly IFabric<ITravelEvent> eventFabric;

        public JsonSerialization(IFabric<ITravelEvent> eventFabric)
        {
            this.eventFabric = eventFabric;
        }

        public void SaveUsers(List<User> users)
        {
            var jsonString = JsonConvert.SerializeObject(users);
            using (var writer = new StreamWriter(MainApplication.SerializationFile, false, Encoding.UTF8))
            {
                writer.Write(jsonString);
            }
        }

        public List<User> LoadUsers()
        {
            string input;
            using (var reader = new StreamReader(MainApplication.SerializationFile))
            {
                input = reader.ReadToEnd();
            }
            var usersArray = JArray.Parse(input);
            var users = new List<User>();
            foreach (var userToken in usersArray)
            {
                var id = (int)userToken["Id"];
                var user = new User(id);
                users.Add(user);

                foreach (var travelToken in userToken["Travelers"])
                {
                    var travelId = (int)travelToken["Id"];
                    var travelName = (string)travelToken["Name"];
                    var travel = new Travel(travelId, travelName);
                    user.Travels.Add(travel);
                    foreach (var travelEvent in travelToken["Events"])
                    {
                        travel.Events.Add(GetTravelEventFromToken(travelEvent));
                    }
                }
            }
            return users;
        }

        private ITravelEvent GetTravelEventFromToken(JToken travelEventToken)
        {
            var eventName = (string)travelEventToken["Name"];

            var dates = travelEventToken["Dates"].Select(d => (DateTime) d).ToArray();
            var locations = travelEventToken["Locations"]
                .Select(t => new Location(t["Coordinate"].ToObject<GeoCoordinate>(), (string)t["Name"])).ToArray();

            var costToken = travelEventToken["Cost"];

            var cost = new Money(Currencies.GetCurrency((string)costToken["Currency"]["Name"]), (decimal)costToken["Amount"]);
            var type = (string)travelEventToken["Type"];

            return eventFabric.Get(eventName, dates, locations, cost, type);
        }
    }
}
