using System;
using System.Collections.Generic;
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
                var newEvent = CreateEvent();
                App.UserSessionHandler.CurrentTravelEvents.Add(newEvent);
                Close();
            };
        }
    }
}