using System.Collections.Generic;
using TravelPlanner.Application;
using TravelPlanner.Domain;

namespace TravelPlanner.UserInterface.EventForms
{
    class TravelEventFormFactory
    {
        private readonly IApplication app;

        public TravelEventFormFactory(IApplication app)
        {
            this.app = app;
        }

        public TravelEventForm CreateAddForm() => new AddEventForm(app);

        public TravelEventForm CreateEditForm(ITravelEvent travelEvent) => new EditEventForm(app, travelEvent);
    }
}