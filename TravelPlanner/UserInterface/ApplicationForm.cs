using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;
using TravelPlanner.Application;

namespace TravelPlanner.UserInterface
{
    class ApplicationForm : MetroForm
    {
        private readonly IApplication app;

        public ApplicationForm(IApplication app)
        {
            this.app = app;
            Size = new Size(800, 600);
            ShadowType = MetroFormShadowType.None;
            InitTable();
        }

        private void InitTable()
        {
            var table = new TableLayoutPanel();
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            var travels = app.GetTravels();
            for (var i = 0; i < travels.Count; i++)
            {
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
                var i1 = i;
                var travelButton = Elements.GetButton(travels[i].ToString(), (sender, args) =>
                {
                    var createPathForm = new PathForm(app, travels[i1]);
                    createPathForm.Show(this);
                    InitTable();
                });
                travelButton.Dock = DockStyle.Fill;
                table.Controls.Add(travelButton, 0, i);
            }

            table.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
            var addButton = Elements.GetButton("Добавить", (sender, args) =>
            {
                app.AddTravel("name"); //TODO: Илюха, ты хотел ты и разбирайся
                var createPathForm = new PathForm(app);
                createPathForm.Show(this);
                InitTable();
            });
            addButton.Dock = DockStyle.Fill;
            table.Controls.Add(addButton);

            table.Dock = DockStyle.Fill;
            foreach (Control control in Controls)
            {
                if (control is TableLayoutPanel) Controls.Remove(control);
            }
            Controls.Add(table);
        }
    }
}
