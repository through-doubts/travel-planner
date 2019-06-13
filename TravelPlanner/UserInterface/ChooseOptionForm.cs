using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TravelPlanner.Infrastructure.Extensions;

namespace TravelPlanner.UserInterface
{
    public abstract class ChooseOptionForm<TOption> : FormWithTable
    {
        private readonly Func<List<TOption>> getOptions;

        protected ChooseOptionForm(Func<List<TOption>> getOptions)
        {
            this.getOptions = getOptions;
            Controls.Add(InitTable());
        }

        protected sealed override TableLayoutPanel InitTable()
        {
            var majorTable = new TableLayoutPanel();
            majorTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            majorTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));

            majorTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            majorTable.Controls.Add(InitOptionsTable(), 0, 0);
            majorTable.Controls.Add(InitMenuTable(), 1, 0);
            majorTable.Dock = DockStyle.Fill;
            return majorTable;
        }

        private TableLayoutPanel InitMenuTable() => InitButtonTable(GetButtons(), 100);

        private TableLayoutPanel InitOptionsTable()
        {
            var optionsTable = InitButtonTable(getOptions()
                .Select(GetOptionButton)
                .Select(b =>
                {
                    b.MouseDown += Button_MouseDown;
                    b.MouseMove += Button_MouseMove;
                    b.MouseUp += Button_MouseUp;
                    b.MouseClick += Button_MouseClick;
                    return b;
                })
                .ToList(), 100);
            optionsTable.DragOver += TableLayoutPanel_DragOver;
            optionsTable.AllowDrop = true;
            optionsTable.AutoScroll = true;
            return optionsTable;
        }

        private TableLayoutPanel InitButtonTable(List<Button> buttons, int rowHeight)
        {
            var table = new TableLayoutPanel {GrowStyle = TableLayoutPanelGrowStyle.AddRows, AutoSize = true};
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            table.AddControlsToRows(buttons, 0, 0, SizeType.Absolute, rowHeight);

            table.Dock = DockStyle.Top;
            return table;
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            ((Button)sender).Tag = new object();
        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            var button = (Button)sender;
            if (button.Tag != null)
                button.DoDragDrop(sender, DragDropEffects.Move);
        }

        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            ((Button)sender).Tag = null;
        }

        private void Button_MouseClick(object sender, MouseEventArgs e)
        {
            Text = ((Control)sender).Text;
        }

        private void TableLayoutPanel_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(Button)))
                return;
            var tableLayoutPanel = (TableLayoutPanel) sender;

            e.Effect = e.AllowedEffect;
            var draggedButton = (Button)e.Data.GetData(typeof(Button));

            var pt = tableLayoutPanel.PointToClient(new Point(e.X, e.Y));
            var button = (Button)tableLayoutPanel.GetChildAtPoint(pt);

            if (button != null)
            {
                var pos = tableLayoutPanel.GetPositionFromControl(button);
                tableLayoutPanel.Controls.Add(draggedButton, pos.Column, pos.Row);
                draggedButton.Tag = null;
            }
        }

        protected abstract List<Button> GetButtons();
        protected abstract Button GetOptionButton(TOption option);
    }
}