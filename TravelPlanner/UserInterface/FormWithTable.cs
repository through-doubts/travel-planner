using System.Windows.Forms;
using MetroFramework.Forms;

namespace TravelPlanner.UserInterface
{
    public abstract class FormWithTable : MetroForm
    {
        protected FormWithTable()
        {
            ShadowType = MetroFormShadowType.None;
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

        protected abstract TableLayoutPanel InitTable();
    }
}