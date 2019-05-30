using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;
using TravelPlanner.Application;

namespace TravelPlanner.UserInterface
{
    class ApplicationForm : MetroForm
    {
        private readonly IApplication app;
        private readonly MetroForm pathForm;

        public ApplicationForm(IApplication app, MetroForm pathForm)
        {
            this.app = app;
            this.pathForm = pathForm;
            Size = new Size(800, 600);
            ShadowType = MetroFormShadowType.None;
            Controls.Add(InitTable());
        }

        private TableLayoutPanel InitTable()
        {
            var table = new TableLayoutPanel();
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            var travels = app.GetTravels();
            for (var i = 0; i < travels.Count; i++)
            {
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
                table.Controls.Add(GetTravelButton(travels[i].ToString()), 0, i);
            }

            table.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
            table.Controls.Add(GetAddButton());

            table.Dock = DockStyle.Fill;
            return table;
        }

        private void UpdateTable()
        {
            var newTable = InitTable();
            foreach (Control control in Controls)
            {
                if (control is TableLayoutPanel) Controls.Remove(control);
            }
            Controls.Add(newTable);
        }

        private Button GetAddButton()
        {
            var addButton = Elements.GetButton("Добавить", (sender, args) =>
            {
                Hide();
                app.AddTravel("name");//TODO: Илюха, ты хотел ты и разбирайся
                pathForm.ShowDialog(this);
                UpdateTable();
                Show();
            });
            addButton.Dock = DockStyle.Fill;
            return addButton;
        }

        private Button GetTravelButton(string travelName)
        {
            var travelButton = Elements.GetButton(travelName, (sender, args) =>
            {
                Hide();
                pathForm.ShowDialog(this);
                UpdateTable();
                Show();
            });
            travelButton.Dock = DockStyle.Fill;
            return travelButton;
        }
    }
}
