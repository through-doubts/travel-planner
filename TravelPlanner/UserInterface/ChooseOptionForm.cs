using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace TravelPlanner.UserInterface
{
    public abstract class ChooseOptionForm : MetroForm
    {
        private readonly Func<List<string>> getOptions;

        protected ChooseOptionForm(Func<List<string>> getOptions)
        {
            this.getOptions = getOptions;
            ShadowType = MetroFormShadowType.None;
            Controls.Add(InitTable());
        }

        protected TableLayoutPanel InitTable()
        {
            var table = new TableLayoutPanel { GrowStyle = TableLayoutPanelGrowStyle.AddRows };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            var options = getOptions();
            for (var i = 0; i < options.Count; i++)
            {
                table.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
                table.Controls.Add(GetOptionButton(options[i]), 0, i);
            }

            foreach (var button in GetButtons())
            {
                table.Controls.Add(button);
            }

            table.Dock = DockStyle.Fill;
            table.AutoScroll = true;
            return table;
        }

        protected void UpdateTable()
        {
            var newTable = InitTable();
            foreach (Control control in Controls)
            {
                if (control is TableLayoutPanel) Controls.Remove(control);
            }
            Controls.Add(newTable);
        }

        protected abstract IEnumerable<Button> GetButtons();
        protected abstract Button GetOptionButton(string optionName);
    }
}