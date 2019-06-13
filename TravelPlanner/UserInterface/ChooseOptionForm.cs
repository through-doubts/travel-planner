using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TravelPlanner.Infrastructure;
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
            const int rowSpan = 2;
            var optionsTable = InitButtonTable(getOptions()
                .Select(GetOptionButton).ToList(), 50, rowSpan, 90);
            optionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            var arrowButtons = GetArrowButtons(optionsTable, rowSpan);
            optionsTable.AddControls(arrowButtons, 1, 0);

            optionsTable.AutoScroll = true;
            return optionsTable;
        }

        private List<Button> GetArrowButtons(TableLayoutPanel optionsTable, int rowSpan)
        {
            var arrowButtons = new List<Button>();
            for (var i = 0; i < optionsTable.RowStyles.Count; i++)
            {
                if (i % 2 == 0)
                {
                    var i1 = i;
                    arrowButtons.Add(Elements.ArrowButton(Direction.Up, (sender, args) =>
                    {
                        if (i1 != 0)
                        {
                            optionsTable.SwapCells(new TableLayoutPanelCellPosition(0, i1),
                                new TableLayoutPanelCellPosition(0, i1 - rowSpan));
                        }
                    }));
                }
                else
                {
                    var i1 = i;
                    arrowButtons.Add(Elements.ArrowButton(Direction.Down, (sender, args) =>
                    {
                        if (i1 != optionsTable.RowStyles.Count - 1)
                        {
                            optionsTable.SwapCells(new TableLayoutPanelCellPosition(0, i1),
                                new TableLayoutPanelCellPosition(0, i1 + rowSpan));
                        }
                    }));
                }
            }

            return arrowButtons;
        }

        private TableLayoutPanel InitButtonTable
            (List<Button> buttons, int rowHeight, int rowSpan=1, int columnWidth=100)
        {
            var table = new TableLayoutPanel {GrowStyle = TableLayoutPanelGrowStyle.AddRows, AutoSize = true};
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnWidth));
            table.AddControlsToRows(buttons, 0, 0, SizeType.Absolute, rowHeight, rowSpan);

            table.Dock = DockStyle.Top;
            return table;
        }

        protected abstract List<Button> GetButtons();
        protected abstract Button GetOptionButton(TOption option);
    }
}