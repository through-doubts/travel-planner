using System.Collections.Generic;
using System.Windows.Forms;
using TravelPlanner.Application;
using TravelPlanner.Domain;

namespace TravelPlanner.UserInterface
{
    class EditEventForm : TravelEventForm
    {
        private readonly ITravelEvent travelEvent;

        public EditEventForm(IApplication app, IEnumerable<string> cities, ITravelEvent travelEvent) : base(app, cities)
        {
            this.travelEvent = travelEvent;
        }

        protected override Button GetSaveButton()
        {
            var saveButton = Elements.GetButton("Сохранить", (sender, args) =>
            {
                var newEvent = App.EventHandler.GetEvent(EventTypeBox.Text,
                    StartPicker.Value, EndPicker.Value,
                    AmountPicker.Value, CurrencyBox.Text, SubEventTypeBox.Text);
                App.UserSessionHandler.ReplaceEvent(travelEvent, newEvent);
                Close();
            });
            saveButton.Dock = DockStyle.Bottom;
            return saveButton;
        }
    }
}