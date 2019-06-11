using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MetroFramework.Controls;
using TravelPlanner.Application;
using TravelPlanner.Domain;
using TravelPlanner.Infrastructure;
using TravelPlanner.Infrastructure.Extensions;

namespace TravelPlanner.UserInterface
{
    sealed class AddForm : FormWithTable
    {
        private readonly IApplication app;
        private readonly bool needToReplace;
        private readonly ITravelEvent travelEvent;
        private DateTimePicker startPicker;
        private DateTimePicker endPicker;
        private ComboBox eventTypeBox;
        private ComboBox subEventTypeBox;
        private ComboBox currencyBox;
        private NumericUpDown amountPicker;
        private List<MetroTextBox> locationBoxes;

        public AddForm(IApplication app, ITravelEvent travelEvent)
        {
            this.app = app;
            Size = new Size(800, 600);
            Text = "Событие";

            InitControls();

            if (travelEvent != null)
            {
                needToReplace = true;
                this.travelEvent = travelEvent;
                InitElementsText();
            }

            Controls.Add(InitTable());
        }

        private void InitControls()
        {
            startPicker = Elements.GeTimePicker("Дата1");
            endPicker = Elements.GeTimePicker("Дата2");
            eventTypeBox = Elements.TypeBox(app.EventHandler.GetEventsNames(), "Тип события");
            subEventTypeBox = Elements.TypeBox(
                Enum.GetNames(app.EventHandler.GetEventSubType(app.EventHandler.GetEventsNames()[0])),
                "Подтип события");
            eventTypeBox.SelectedIndexChanged += (sender, args) =>
            {
                subEventTypeBox.DataSource = Enum.GetNames(app.EventHandler.GetEventSubType(eventTypeBox.Text));
            };
            currencyBox = Elements.TypeBox(Enum.GetNames(typeof(Currency)), "Валюта");
            amountPicker = new NumericUpDown {DecimalPlaces = 2, Maximum = 100000, Name = "Стоимость"};
            locationBoxes = new List<MetroTextBox>
            {
                new MetroTextBox {Name = "Место отправления"},
                new MetroTextBox {Name = "Место прибытия"}
            };
        }

        public AddForm(IApplication app) : this(app, null) { }

        private void InitElementsText()
        {
            startPicker.Value = travelEvent.DateTimeInterval.Start;
            endPicker.Value = travelEvent.DateTimeInterval.End;
            eventTypeBox.SelectedItem = travelEvent.Name;
            currencyBox.Text = travelEvent.Cost.Currency.ToString();
            amountPicker.Value = travelEvent.Cost.Amount;
        }

        protected override TableLayoutPanel InitTable()
        {
            var table = new TableLayoutPanel {GrowStyle = TableLayoutPanelGrowStyle.AddRows, AutoSize = true};
            var controls = GetControls();
            var buttons = GetButtons();

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));
            table.AddControlsToRows(controls, 1, 0, SizeType.Absolute, 46);
            table.AddControls(controls.Select(c => c.Name).Select(Elements.GetLabel).ToList(), 0, 0);
            table.AddControlsToRows(buttons, 0, controls.Count, SizeType.Absolute, 46);

            table.Dock = DockStyle.Top;
            return table;
        }

        private List<Control> GetControls()
        {
            var controls = new List<Control>
            {
                eventTypeBox, subEventTypeBox, startPicker, endPicker, amountPicker,
                currencyBox
            };
            controls.InsertRange(2, locationBoxes);
            return controls;
        }

        private List<Button> GetButtons()
        {
            return new List<Button>
            {
                GetNetworkButton(),
                GetSaveButton(),
                Elements.BackButton(this, "Отмена")
            };
        }

        private Button GetSaveButton()
        {
            var saveButton = Elements.GetButton("Сохранить", (sender, args) =>
            {
                var newEvent = app.EventHandler.GetEvent(eventTypeBox.Text,
                    startPicker.Value, endPicker.Value,
                    amountPicker.Value, currencyBox.Text, subEventTypeBox.Text);
                if (needToReplace)
                    app.UserSessionHandler.ReplaceEvent(travelEvent, newEvent);
                else
                    app.UserSessionHandler.AddEvent(newEvent);
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