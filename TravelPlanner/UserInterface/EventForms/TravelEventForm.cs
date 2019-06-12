using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;
using TravelPlanner.Application;
using TravelPlanner.Domain;
using TravelPlanner.Infrastructure;
using TravelPlanner.Infrastructure.Extensions;

namespace TravelPlanner.UserInterface.EventForms
{
    abstract class TravelEventForm : FormWithTable
    {
        protected readonly IApplication App;
        protected DateTimePicker StartPicker;
        protected DateTimePicker EndPicker;
        protected ComboBox EventTypeBox;
        protected ComboBox SubEventTypeBox;
        protected ComboBox CurrencyBox;
        protected NumericUpDown AmountPicker;
        protected List<MetroTextBox> LocationBoxes;

        protected TravelEventForm(IApplication app)
        {
            App = app;
            Size = new Size(800, 600);
            Text = "Событие";

            InitControls();

            Controls.Add(InitTable());
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private void InitControls()
        {
            StartPicker = Elements.GeTimePicker("Дата1");
            EndPicker = Elements.GeTimePicker("Дата2");
            EventTypeBox = Elements.TypeBox(App.EventHandler.GetEventsNames(), "Тип события");
            SubEventTypeBox = Elements.TypeBox(
                App.EventHandler.GetEventSubTypes(App.EventHandler.GetEventsNames()[0]),
                "Подтип события");
            EventTypeBox.SelectedIndexChanged += (sender, args) =>
            {
                SubEventTypeBox.DataSource = App.EventHandler.GetEventSubTypes(EventTypeBox.Text);
                LocationBoxes = GetLocationBoxes(EventTypeBox.Text);
                UpdateTable();
            };
            CurrencyBox = Elements.TypeBox(Enum.GetNames(typeof(Currency)), "Валюта");
            AmountPicker = new NumericUpDown { DecimalPlaces = 2, Maximum = 100000, Name = "Стоимость" };
            LocationBoxes = GetLocationBoxes(EventTypeBox.Text);
        }

        private List<MetroTextBox> GetLocationBoxes(string eventName)
        {
            var eventType = App.EventHandler.GetEventType(eventName);
            var cities = App.LocationHandler.GetLocationsNames().ToArray();
            if (eventType == typeof(Housing))
            {
                return new List<MetroTextBox>
                {
                    Elements.CityBox(cities, "Место остановки")
                };
            }
            if (eventType == typeof(Transfer))
            {
                return new List<MetroTextBox>
                {
                    Elements.CityBox(cities, "Место отправления"),
                    Elements.CityBox(cities, "Место прибытия")
                };
            }
            throw new ArgumentException(eventName);
        }

        protected sealed override TableLayoutPanel InitTable()
        {
            var table = new TableLayoutPanel { GrowStyle = TableLayoutPanelGrowStyle.AddRows, AutoSize = true };
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
                EventTypeBox, SubEventTypeBox, StartPicker, EndPicker, AmountPicker,
                CurrencyBox
            };
            controls.InsertRange(2, LocationBoxes);
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

        protected abstract EventHandler GetSaveButtonHandler();

        private Button GetSaveButton()
        {
            var saveButton = Elements.GetButton("Сохранить", GetSaveButtonHandler());
            saveButton.Dock = DockStyle.Bottom;
            return saveButton;
        }

        private Button GetNetworkButton()
        {
            var networkButton = Elements.GetButton("Посмотреть варианты в сети", (sender, args) => { });
            networkButton.Dock = DockStyle.Fill;
            return networkButton;
        }

        protected bool TryCreateEvent(out ITravelEvent travelEvent)
        {
            var result = LocationBoxes.Select(x => x.Text).All(x => App.LocationHandler.CityExists(x));
            if (result)
            {
                travelEvent = App.EventFabric.Get(EventTypeBox.Text,
                    new[] {StartPicker.Value, EndPicker.Value},
                    LocationBoxes.Select(x => App.LocationHandler.GetLocationByName(x.Text)).ToArray(),
                    new Money((Currency) Enum.Parse(typeof(Currency), CurrencyBox.Text), AmountPicker.Value),
                    SubEventTypeBox.Text);
            }
            else
            {
                travelEvent = null;
            }

            return result;
        }

        protected void ShowCreateEventError()
        {
            MetroMessageBox.Show(this, "Не удалось создать событие, проверьте входные данные",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
