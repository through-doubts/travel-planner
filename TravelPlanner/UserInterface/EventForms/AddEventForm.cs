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
                    ShowCreateEventError("Не удалось создать событие, проверьте входные данные");
                    return;
                }
                App.UserSessionHandler.CurrentTravelEvents.Add(newEvent);
                Close();
            };
        }
    }
}