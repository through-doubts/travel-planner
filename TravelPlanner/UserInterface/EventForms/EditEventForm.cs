using System;
using TravelPlanner.Application;
using TravelPlanner.Domain;

namespace TravelPlanner.UserInterface.EventForms
{
    class EditEventForm : TravelEventForm
    {
        private readonly ITravelEvent travelEvent;

        public EditEventForm(IApplication app, ITravelEvent travelEvent) : base(app)
        {
            this.travelEvent = travelEvent;
            InitControlsDefaultValues();
        }

        private void InitControlsDefaultValues()
        {
            EventTypeBox.Text = travelEvent.Name;
            SubEventTypeBox.Text = travelEvent.Type;
            StartPicker.Value = travelEvent.Dates[0];
            EndPicker.Value = travelEvent.Dates[1];
            CurrencyBox.Text = travelEvent.Cost.Currency.ToString();
            AmountPicker.Value = travelEvent.Cost.Amount;
            for (var i = 0; i < travelEvent.Locations.Length; i++)
            {
                LocationBoxes[i].Text = travelEvent.Locations[i].Name;
            }
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
                App.UserSessionHandler.CurrentTravelEvents.Replace(travelEvent, newEvent);
                Close();
            };
        }
    }
}