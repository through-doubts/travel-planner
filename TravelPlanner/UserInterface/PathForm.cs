using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TravelPlanner.Application;

namespace TravelPlanner.UserInterface
{
    class PathForm : ChooseOptionForm
    {
        private readonly AddForm addForm;

        public PathForm(IApplication app, AddForm addForm) : base(app.GetTravelEvents)
        {
            this.addForm = addForm;
            Size = new Size(800, 600);
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
            var eventButton = Elements.GetButton(eventName, (sender, args) =>
            {
                //app.ChangeCurrentTravel(travelName);
                Hide();
                addForm.ShowDialog(this);
                UpdateTable();
                Show();
            });
            eventButton.ContextMenuStrip = GetTravelButtonStrip();
            eventButton.Dock = DockStyle.Fill;
            return eventButton;
        }

        private ContextMenuStrip GetTravelButtonStrip()
        {
            var contextMenu = new ContextMenuStrip();
            var fix = new ToolStripMenuItem("Зафиксировать");
            contextMenu.Items.Add(fix);
            return contextMenu;
        }

        private Button GetUpdateButton()
        {
            var updateButton = Elements.GetButton("Обновить", (sender, args) => { });
            updateButton.Dock = DockStyle.Fill;
            return updateButton;
        }

        protected override IEnumerable<Button> GetButtons()
        {
            yield return GetAddButton();
            yield return GetUpdateButton();
            yield return Elements.BackButton(this, "Назад");
        }

        protected override Button GetOptionButton(string optionName)
        {
            return GetTravelEventButton(optionName);
        }
    }
}
