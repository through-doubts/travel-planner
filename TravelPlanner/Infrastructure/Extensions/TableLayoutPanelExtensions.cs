using System.Collections.Generic;
using System.Windows.Forms;

namespace TravelPlanner.Infrastructure.Extensions
{
    public static class TableLayoutPanelExtensions
    {
        public static void AddControls
            (this TableLayoutPanel table, IReadOnlyList<Control> controls, int column,
            int rowFrom, int rowSpan=1)
        {
            for (var i = 0; i < controls.Count; i++)
            {
                controls[i].Dock = DockStyle.Fill;
                table.Controls.Add(controls[i], column, (rowFrom + i) * rowSpan);
                table.SetRowSpan(controls[i], rowSpan);
            }
        }

        public static void AddRows(this TableLayoutPanel table, int count, SizeType sizeType, int size)
        {
            for (var i = 0; i < count; i++)
            {
                table.RowStyles.Add(new RowStyle(sizeType, size));
            }
        }

        public static void AddControlsToRows(
            this TableLayoutPanel table, IReadOnlyList<Control> controls, int column, int rowFrom,
            SizeType rowSizeType, int rowSize, int rowSpan=1)
        {
            table.AddRows(controls.Count * rowSpan, rowSizeType, rowSize);
            table.AddControls(controls, column, rowFrom, rowSpan);
        }

        public static void SwapCells
            (this TableLayoutPanel table, TableLayoutPanelCellPosition cell1, TableLayoutPanelCellPosition cell2)
        {
            var c1 = table.GetControlFromPosition(cell1.Column, cell1.Row);
            var c2 = table.GetControlFromPosition(cell2.Column, cell2.Row);
            var pos1 = table.GetCellPosition(c1);
            table.SetCellPosition(c1, table.GetCellPosition(c2));
            table.SetCellPosition(c2, pos1);
        }
    }
}