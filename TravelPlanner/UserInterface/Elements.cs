using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MetroFramework.Controls;
using TravelPlanner.Infrastructure.Extensions;

namespace TravelPlanner.UserInterface
{
    class Elements
    {
        public static ComboBox TypeBox(IEnumerable<string> items, string name="")
        {
            var box = new MetroComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Name = name
            };
            box.Items.AddRange(items.OfType<object>().ToArray());
            box.SelectedIndex = 0;
            return box;
        }

        public static TextBox GetLabel(string text) => new TextBox
        {
            Text = text,
            Enabled = false
        };

        public static Button GetButton(string text, EventHandler onClick)
        {
            var button = new Button
            {
                Text = text,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                AutoSize = true,
            };
            button.Click += onClick;
            return button;
        }

        public static DateTimePicker GeTimePicker(string name="") => new DateTimePicker
        {
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "MM/dd/yyyy hh:mm:ss",
            Name = name
        };

        public static MetroTextBox CityBox(IEnumerable<string> cities, string name="") => new MetroTextBox
        {
            Name = name,
            AutoCompleteMode = AutoCompleteMode.SuggestAppend,
            AutoCompleteSource = AutoCompleteSource.CustomSource,
            AutoCompleteCustomSource = cities.ToAutoCompleteStringCollection()
        };

        public static Button BackButton(Form form, string text)
        {
            var button = GetButton(text, (sender, args) => form.Close());
            button.Dock = DockStyle.Fill;
            return button;
        }
    }
}
