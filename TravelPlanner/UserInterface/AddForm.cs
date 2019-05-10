using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Controls;
using MetroFramework.Forms;

namespace TravelPlanner.UserInterface
{
    class AddForm : MetroForm
    {
        public AddForm()
        {
            Size = new Size(600, 600);

            InitTable();
        }

        private void InitTable()
        {
            var table = new TableLayoutPanel();
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 5));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));

            table.Controls.Add(Elements.TypeBox(), 1, 0);
            table.Controls.Add(Elements.GeTimePicker(), 1, 1);
            table.Controls.Add(Elements.GeTimePicker(), 1, 2);
            table.Controls.Add(new TextBox { Dock = DockStyle.Fill }, 1, 3);
            table.Controls.Add(Elements.GetLabel("Тип события"), 0, 0);
            table.Controls.Add(Elements.GetLabel("Дата1"), 0, 1);
            table.Controls.Add(Elements.GetLabel("Дата2"), 0, 2);
            table.Controls.Add(Elements.GetLabel("Стоимость"), 0, 3);
            table.Controls.Add(Elements.GetBottomButton("Сохранить"), 0, 4);
            table.Dock = DockStyle.Fill;
            Controls.Add(table);
        }
    }
}
