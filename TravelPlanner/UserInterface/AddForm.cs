using System;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;
using TravelPlanner.Application;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.UserInterface
{
    class AddForm : MetroForm
    {
        private readonly IApplication app;
        private readonly DateTimePicker startPicker;
        private readonly DateTimePicker endPicker;
        private readonly ComboBox eventTypeBox;
        private readonly ComboBox subEventTypeBox;
        private readonly ComboBox currencyBox;
        private readonly NumericUpDown amountPicker;

        public AddForm(IApplication app)
        {
            this.app = app;
            Size = new Size(600, 600);
            ShadowType = MetroFormShadowType.None;
            startPicker = Elements.GeTimePicker();
            endPicker = Elements.GeTimePicker();
            eventTypeBox = Elements.TypeBox(app.GetEventsNames());
            subEventTypeBox = Elements.TypeBox(Enum.GetNames(app.EventTypes[app.GetEventsNames()[0]]));
            eventTypeBox.SelectedIndexChanged += (sender, args) =>
            {
                subEventTypeBox.DataSource = Enum.GetNames(this.app.EventTypes[eventTypeBox.Text]);
            };
            currencyBox = Elements.TypeBox(Enum.GetNames(typeof(Currency)));
            amountPicker = new NumericUpDown {Dock = DockStyle.Fill, DecimalPlaces = 2};
            InitTable();
        }

        private void InitTable()
        {
            var table = new TableLayoutPanel();
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 5));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 5));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));

            table.Controls.Add(eventTypeBox, 1, 0);
            table.Controls.Add(subEventTypeBox, 1, 1);
            table.Controls.Add(startPicker, 1, 2);
            table.Controls.Add(endPicker, 1, 3);
            table.Controls.Add(amountPicker, 1, 4);
            table.Controls.Add(currencyBox, 1, 5);
            table.Controls.Add(Elements.GetLabel("Тип события"), 0, 0);
            table.Controls.Add(Elements.GetLabel("Подтип события"), 0, 1);
            table.Controls.Add(Elements.GetLabel("Дата1"), 0, 2);
            table.Controls.Add(Elements.GetLabel("Дата2"), 0, 3);
            table.Controls.Add(Elements.GetLabel("Стоимость"), 0, 4);
            table.Controls.Add(Elements.GetLabel("Валюта"), 0, 5);
            table.Controls.Add(GetSaveButton(), 0, 6);
            table.Controls.Add(GetCancelButton(), 0, 7);
            table.Dock = DockStyle.Fill;
            Controls.Add(table);
        }

        private Button GetSaveButton()
        {
            var saveButton = Elements.GetButton("Сохранить", (sender, args) =>
            {
                var travelEvent = app.GetEvent(eventTypeBox.Text, startPicker.Value, endPicker.Value,
                    amountPicker.Value, currencyBox.Text, subEventTypeBox.Text);
                app.AddEvent(travelEvent);
                Close();
            });
            saveButton.Dock = DockStyle.Bottom;
            return saveButton;
        }

        private Button GetCancelButton()
        {
            var cancelButton = Elements.GetButton("Отмена", (sender, args) => Close());
            cancelButton.Dock = DockStyle.Bottom;
            return cancelButton;
        }
    }
}
