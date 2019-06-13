using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;
using TravelPlanner.Application;
using TravelPlanner.Domain;
using TravelPlanner.Domain.TravelEvents;
using TravelPlanner.Infrastructure;
using TravelPlanner.Infrastructure.Extensions;

namespace TravelPlanner.UserInterface.EventForms
{
    abstract class TravelEventForm : FormWithTable
    {
        protected readonly IApplication App;
        protected List<DateTimePicker> DateTimePickers;
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
            EventTypeBox = Elements.TypeBox(App.EventHandler.GetEventsNames(), "Тип события");
            SubEventTypeBox = Elements.TypeBox(
                App.EventHandler.GetFieldsInfo(App.EventHandler.GetEventsNames()[0]).PossibleTypes,
                "Подтип события");
            EventTypeBox.SelectedIndexChanged += (sender, args) =>
            {
                SubEventTypeBox.DataSource = App.EventHandler.GetFieldsInfo(EventTypeBox.Text).PossibleTypes;
                DateTimePickers = GetDateTimePickers(EventTypeBox.Text);
                LocationBoxes = GetLocationBoxes(EventTypeBox.Text);
                UpdateTable();
            };
            DateTimePickers = GetDateTimePickers(EventTypeBox.Text);
            CurrencyBox = Elements.TypeBox(Enum.GetNames(typeof(Currency)), "Валюта");
            AmountPicker = new NumericUpDown { DecimalPlaces = 2, Maximum = 100000, Name = "Стоимость" };
            LocationBoxes = GetLocationBoxes(EventTypeBox.Text);
        }

        private List<MetroTextBox> GetLocationBoxes(string eventName)
        {
            var cities = App.LocationHandler.GetLocationsNames().ToArray();
            return App.EventHandler.GetEventLocationsHeaders(eventName)
                .Select(x => Elements.CityBox(cities, x)).ToList();
        }

        private List<DateTimePicker> GetDateTimePickers(string eventName)
        {
            return App.EventHandler.GetEventDatesHeaders(eventName)
                .Select(Elements.GeTimePicker).ToList();
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
                EventTypeBox, SubEventTypeBox, AmountPicker,
                CurrencyBox
            };
            controls.InsertRange(2, LocationBoxes);
            controls.InsertRange(2 + LocationBoxes.Count, DateTimePickers);
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
            var networkButton = Elements.GetButton("Посмотреть варианты в сети", (sender, args) =>
            {
                if (App.EventHandler.GetEventType(EventTypeBox.Text) != typeof(Transfer))
                {
                    ShowCreateEventError("Не реализовано");
                    return;
                }

                if (!LocationBoxes.Select(x => x.Text).All(x => App.LocationHandler.CityExists(x)))
                {
                    ShowCreateEventError("Не удалось определить город");
                    return;
                }
                var listForm = new TravelEventListForm((() => App.NetworkDataHandler.GetTransfers(
                        CurrencyBox.Text, LocationBoxes[0].Text, LocationBoxes[1].Text,
                        DateTimePickers[0].Value)),
                    "Доступные события",
                    App);
                Hide();
                if (listForm.ShowDialog(this) == DialogResult.OK)
                {
                    Close();
                    return;
                }
                UpdateTable();
                Show();
            });
            networkButton.Dock = DockStyle.Fill;
            return networkButton;
        }

        protected bool TryCreateEvent(out ITravelEvent travelEvent)
        {
            var result = LocationBoxes.Select(x => x.Text).All(x => App.LocationHandler.CityExists(x));
            if (result)
            {
                travelEvent = App.EventFabric.Get(EventTypeBox.Text,
                    DateTimePickers.Select(x => x.Value).ToArray(),
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

        protected void ShowCreateEventError(string message)
        {
            MetroMessageBox.Show(this, message,
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
