using System;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace TravelPlanner.UserInterface
{
    class Elements
    {
        public static ComboBox TypeBox(object dataSource) => new MetroComboBox
        {
            Dock = DockStyle.Fill,
            DataSource = dataSource,
            DropDownStyle = ComboBoxStyle.DropDownList
        };

        public static TextBox GetLabel(string text) => new TextBox
        {
            Dock = DockStyle.Fill,
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

        public static DateTimePicker GeTimePicker() => new DateTimePicker
        {
            Dock = DockStyle.Fill,
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "MM/dd/yyyy hh:mm:ss"
        };
    }
}
