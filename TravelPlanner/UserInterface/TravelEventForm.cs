using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MetroFramework.Controls;
using TravelPlanner.Application;
using TravelPlanner.Infrastructure;
using TravelPlanner.Infrastructure.Extensions;

namespace TravelPlanner.UserInterface
{
    abstract class TravelEventForm : FormWithTable
    {
        protected readonly IApplication App;
        protected readonly IEnumerable<string> Cities;
        protected DateTimePicker StartPicker;
        protected DateTimePicker EndPicker;
        protected ComboBox EventTypeBox;
        protected ComboBox SubEventTypeBox;
        protected ComboBox CurrencyBox;
        protected NumericUpDown AmountPicker;
        protected List<MetroTextBox> LocationBoxes;

        protected TravelEventForm(IApplication app, IEnumerable<string> cities)
        {
            this.App = app;
            Cities = cities;
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
                Enum.GetNames(App.EventHandler.GetEventSubType(App.EventHandler.GetEventsNames()[0])),
                "Подтип события");
            EventTypeBox.SelectedIndexChanged += (sender, args) =>
            {
                SubEventTypeBox.DataSource = Enum.GetNames(App.EventHandler.GetEventSubType(EventTypeBox.Text));
            };
            CurrencyBox = Elements.TypeBox(Enum.GetNames(typeof(Currency)), "Валюта");
            AmountPicker = new NumericUpDown { DecimalPlaces = 2, Maximum = 100000, Name = "Стоимость" };
            LocationBoxes = new List<MetroTextBox>
            {
                Elements.CityBox(Cities, "Место отправления"),
                Elements.CityBox(Cities, "Место прибытия")
            };
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

        protected abstract Button GetSaveButton();

        private Button GetNetworkButton()
        {
            var networkButton = Elements.GetButton("Посмотреть варианты в сети", (sender, args) => { });
            networkButton.Dock = DockStyle.Fill;
            return networkButton;
        }
    }
}
