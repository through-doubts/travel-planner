using System;
using System.Collections.Generic;
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
            var optionsTable = InitButtonTable(getOptions().Select(GetOptionButton).ToList(), 100);
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

        protected abstract List<Button> GetButtons();
        protected abstract Button GetOptionButton(TOption option);
    }
}