using System.Collections.Generic;
using TravelPlanner.Application;
using TravelPlanner.Domain;

namespace TravelPlanner.UserInterface
{
    class TravelEventFormFactory
    {
        private readonly IApplication app;
        private readonly IEnumerable<string> cities;

        public TravelEventFormFactory(IApplication app, IEnumerable<string> cities)
        {
            this.app = app;
            this.cities = cities;
        }

        public TravelEventForm CreateAddForm() => new AddEventForm(app, cities);

        public TravelEventForm CreateEditForm(ITravelEvent travelEvent) => new EditEventForm(app, cities, travelEvent);
    }
}