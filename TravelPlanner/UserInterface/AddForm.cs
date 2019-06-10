using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MetroFramework.Controls;
using MetroFramework.Forms;
using TravelPlanner.Application;
using TravelPlanner.Domain;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.UserInterface
{
    sealed class AddForm : MetroForm
    {
        private readonly IApplication app;
        private DateTimePicker startPicker;
        private DateTimePicker endPicker;
        private ComboBox eventTypeBox;
        private ComboBox subEventTypeBox;
        private ComboBox currencyBox;
        private NumericUpDown amountPicker;
        private MetroTextBox cityBoxStart;
        private MetroTextBox cityBoxEnd;

        public AddForm(IApplication app, ITravelEvent travelEvent)
        {
            this.app = app;
            Size = new Size(800, 600);
            ShadowType = MetroFormShadowType.None;
            Text = "Событие";

            InitControls();

            if (travelEvent != null)
            {
                InitElementsText(travelEvent);
            }

            InitTable(11, 46);
        }

        private void InitControls()
        {
            startPicker = Elements.GeTimePicker();
            endPicker = Elements.GeTimePicker();
            eventTypeBox = Elements.TypeBox(app.EventHandler.GetEventsNames());
            subEventTypeBox = Elements.TypeBox(
                Enum.GetNames(app.EventHandler.GetEventSubType(app.EventHandler.GetEventsNames()[0])));
            eventTypeBox.SelectedIndexChanged += (sender, args) =>
            {
                subEventTypeBox.DataSource = Enum.GetNames(app.EventHandler.GetEventSubType(eventTypeBox.Text));
            };
            currencyBox = Elements.TypeBox(Enum.GetNames(typeof(Currency)));
            amountPicker = new NumericUpDown {Dock = DockStyle.Fill, DecimalPlaces = 2, Maximum = 100000};
            cityBoxStart = new MetroTextBox();
            cityBoxEnd = new MetroTextBox();
        }

        public AddForm(IApplication app) : this(app, null) { }

        private void InitElementsText(ITravelEvent travelEvent)
        {
            startPicker.Value = travelEvent.DateTimeInterval.Start;
            endPicker.Value = travelEvent.DateTimeInterval.End;
            eventTypeBox.Text = travelEvent.Name;
            currencyBox.Text = travelEvent.Cost.Currency.ToString();
            amountPicker.Value = travelEvent.Cost.Amount;
        }

        private void InitTable(int rowsCount, int rowSize)
        {
            var table = new TableLayoutPanel();
            for (var i = 0; i < rowsCount; i++)
            {
                table.RowStyles.Add(new RowStyle(SizeType.Absolute, rowSize));
            }
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));

            AddControlsTo(table);
            table.Dock = DockStyle.Fill;
            Controls.Add(table);
        }

        private void AddControlsTo(TableLayoutPanel table)
        {
            AddControlsTo(table,
                new List<Control>
                {
                    eventTypeBox, subEventTypeBox, cityBoxStart, cityBoxEnd, startPicker, endPicker, amountPicker,
                    currencyBox
                }, 1, 0);
            AddControlsTo(table,
                new List<string>
                {
                    "Тип события", "Подтип события", "Место отправления", "Место прибытия", "Дата1", "Дата2",
                    "Стоимость", "Валюта"
                }.Select(Elements.GetLabel).ToList(), 0, 0);

            table.Controls.Add(GetNetworkButton(), 0, 8);
            table.Controls.Add(GetSaveButton(), 0, 9);
            table.Controls.Add(Elements.BackButton(this, "Отмена"), 0, 10);
        }

        private void AddControlsTo(TableLayoutPanel table, IReadOnlyList<Control> controls, int column, int rowFrom)
        {
            for (var i = 0; i < controls.Count; i++)
            {
                controls[i].Dock = DockStyle.Fill;
                table.Controls.Add(controls[i], column, rowFrom + i);
            }
        }

        private Button GetSaveButton()
        {
            var saveButton = Elements.GetButton("Сохранить", (sender, args) =>
            {
                var travelEvent = app.EventHandler.GetEvent(eventTypeBox.Text, 
                    startPicker.Value, endPicker.Value,
                    amountPicker.Value, currencyBox.Text, subEventTypeBox.Text);
                app.UserSessionHandler.AddEvent(travelEvent);
                Close();
            });
            saveButton.Dock = DockStyle.Bottom;
            return saveButton;
        }

        private Button GetNetworkButton()
        {
            var networkButton = Elements.GetButton("Посмотреть варианты в сети", (sender, args) => { });
            networkButton.Dock = DockStyle.Fill;
            return networkButton;
        }
    }
}
