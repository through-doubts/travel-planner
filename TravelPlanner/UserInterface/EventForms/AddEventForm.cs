using System;
using TravelPlanner.Application;

namespace TravelPlanner.UserInterface.EventForms
{
    sealed class AddEventForm : TravelEventForm
    {
        public AddEventForm(IApplication app) : base(app)
        {
        }

        protected override EventHandler GetSaveButtonHandler()
        {
            return (sender, args) =>
            {
                if (!TryCreateEvent(out var newEvent))
                {
                    ShowCreateEventError();
                    return;
                }
                App.UserSessionHandler.CurrentTravelEvents.Add(newEvent);
                Close();
            };
        }
    }
}