using System.Collections.Generic;
using System.Windows.Forms;

namespace TravelPlanner.Infrastructure.Extensions
{
    public static class TableLayoutPanelExtensions
    {
        public static void AddControls(this TableLayoutPanel table, IReadOnlyList<Control> controls, int column, int rowFrom)
        {
            for (var i = 0; i < controls.Count; i++)
            {
                controls[i].Dock = DockStyle.Fill;
                table.Controls.Add(controls[i], column, rowFrom + i);
            }
        }

        public static void AddRows(this TableLayoutPanel table, int count, SizeType sizeType, int size)
        {
            for (var i = 0; i < count; i++)
            {
                table.RowStyles.Add(new RowStyle(sizeType, size));
            }
        }
    }
}