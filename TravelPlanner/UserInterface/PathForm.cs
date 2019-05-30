using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;
using TravelPlanner.Application;
using TravelPlanner.Domain;

namespace TravelPlanner.UserInterface
{
    class PathForm : MetroForm
    {
        private readonly AddForm addForm;
        private readonly IApplication app;
        private List<ITravelEvent> events = new List<ITravelEvent>();

        public PathForm(IApplication app, AddForm addForm)
        {
            this.app = app;
            this.addForm = addForm;
            Size = new Size(800, 600);
            ShadowType = MetroFormShadowType.None;
            Controls.Add(InitTable());
        }

        private TableLayoutPanel InitTable()
        {
            var table = new TableLayoutPanel();

            for(var i = 0; i < events.Count; i++)
            {
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
                var travelEventButton = GetTravelEventButton(events[i].Name);
                table.Controls.Add(travelEventButton, 0, i);
            }

            table.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 10));

            table.Controls.Add(GetAddButton());
            table.Controls.Add(Elements.BackButton(this, "Назад"));

            table.Dock = DockStyle.Fill;
            return table;
        }

        private void UpdateTable()
        {
            events = app.GetTravelEvents();
            var newTable = InitTable();
            foreach (Control control in Controls)
            {
                if (control is TableLayoutPanel) Controls.Remove(control);
            }
            Controls.Add(newTable);
        }

        private Button GetAddButton()
        {
            var addButton = Elements.GetButton("Добавить событие", (sender, args) =>
            {
                Hide();
                addForm.ShowDialog(this);
                Show();
                UpdateTable();
            });
            addButton.Dock = DockStyle.Fill;
            return addButton;
        }

        private Button GetTravelEventButton(string eventName)
        {
            var eventButton = Elements.GetButton(eventName, (sender, args) => { });
            eventButton.Dock = DockStyle.Fill;
            return eventButton;
        }
    }
}
