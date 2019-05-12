using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace TravelPlanner.UserInterface
{
    class AddForm : MetroForm
    {
        public AddForm()
        {
            Size = new Size(600, 600);
            ShadowType = MetroFormShadowType.None;
            InitTable();
        }

        private void InitTable()
        {
            var table = new TableLayoutPanel();
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 5));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));

            table.Controls.Add(Elements.TypeBox(), 1, 0);
            table.Controls.Add(Elements.GeTimePicker(), 1, 1);
            table.Controls.Add(Elements.GeTimePicker(), 1, 2);
            table.Controls.Add(new NumericUpDown {Dock = DockStyle.Fill, DecimalPlaces = 2}, 1, 3);
            table.Controls.Add(Elements.GetLabel("Тип события"), 0, 0);
            table.Controls.Add(Elements.GetLabel("Дата1"), 0, 1);
            table.Controls.Add(Elements.GetLabel("Дата2"), 0, 2);
            table.Controls.Add(Elements.GetLabel("Стоимость"), 0, 3);
            table.Controls.Add(GetSaveButton(), 0, 4);
            table.Controls.Add(GetCancelButton(), 0, 5);
            table.Dock = DockStyle.Fill;
            Controls.Add(table);
        }

        private Button GetSaveButton()
        {
            return Elements.GetBottomButton("Сохранить", (sender, args) => Close());
        }

        private Button GetCancelButton() => Elements.GetBottomButton("Отмена", (sender, args) => Close());
    }
}
