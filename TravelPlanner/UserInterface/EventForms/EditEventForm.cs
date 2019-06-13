using System;
using System.Linq;
using TravelPlanner.Application;
using TravelPlanner.Domain;
using TravelPlanner.Infrastructure.Extensions;

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
            DateTimePickers.InitializeProperty((x, p) => x.Value = p, travelEvent.Dates);
            CurrencyBox.Text = travelEvent.Cost.Currency.ToString();
            AmountPicker.Value = travelEvent.Cost.Amount;
            LocationBoxes.InitializeProperty((x, p) => x.Text = p, travelEvent.Locations.Select(loc => loc.Name));
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