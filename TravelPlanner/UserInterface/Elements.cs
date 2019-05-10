using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelPlanner.UserInterface
{
    class Elements
    {
        public static ComboBox TypeBox() => new ComboBox
        {
            Dock = DockStyle.Fill
        };

        public static TextBox GetLabel(string text) => new TextBox
        {
            Dock = DockStyle.Fill,
            Text = text,
            Enabled = false
        };

        public static Button GetBottomButton(string text) => new Button
        {
            Text = text,
            BackColor = Color.Transparent,
            FlatStyle = FlatStyle.Flat,
            FlatAppearance = { BorderSize = 0 },
            Dock = DockStyle.Bottom,
            AutoSize = true
        };

        public static DateTimePicker GeTimePicker() => new DateTimePicker
        {
            Dock = DockStyle.Fill,
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "MM/dd/yyyy hh:mm:ss"
        };
    }
}
