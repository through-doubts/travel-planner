using System.Collections.Generic;
using System.Windows.Forms;
using TravelPlanner.Application;

namespace TravelPlanner.UserInterface
{
    sealed class AddEventForm : TravelEventForm
    {
        public AddEventForm(IApplication app, IEnumerable<string> cities) : base(app, cities)
        {
        }

        protected override Button GetSaveButton()
        {
            var saveButton = Elements.GetButton("Сохранить", (sender, args) =>
            {
                var newEvent = App.EventHandler.GetEvent(EventTypeBox.Text,
                    StartPicker.Value, EndPicker.Value,
                    AmountPicker.Value, CurrencyBox.Text, SubEventTypeBox.Text);
                App.UserSessionHandler.AddEvent(newEvent);
                Close();
            });
            saveButton.Dock = DockStyle.Bottom;
            return saveButton;
        }
    }
}