using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TravelPlanner.Application;
using System.Linq;

namespace TravelPlanner.UserInterface
{
    sealed class PathForm : ChooseOptionForm
    {
        private readonly IApplication app;

        public PathForm(IApplication app) : base(
            () => app.UserSessionHandler.GetTravelEvents().Select(e => e.ToStringValue()).ToList())
        {
            this.app = app;
            Size = new Size(800, 600);
            Text = "События";
        }

        private Button GetAddButton()
        {
            var addButton = Elements.GetButton("Добавить событие", (sender, args) =>
            {
                Hide();
                var addForm = new AddForm(app);
                addForm.ShowDialog(this);
                Show();
                UpdateTable();
            });
            return addButton;
        }

        private Button GetTravelEventButton(string eventName)
        {
            var eventButton = Elements.GetButton(eventName, (sender, args) =>
            {
                //app.ChangeCurrentTravel(travelName);
                Hide();
                var addForm = new AddForm(app);
                addForm.ShowDialog(this);
                UpdateTable();
                Show();
            });
            eventButton.ContextMenuStrip = GetTravelButtonStrip();
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
            return updateButton;
        }

        protected override List<Button> GetButtons()
        {
            return new List<Button>
            {
                GetAddButton(),
                GetUpdateButton(),
                Elements.BackButton(this, "Назад")
            };
        }

        protected override Button GetOptionButton(string optionName)
        {
            return GetTravelEventButton(optionName);
        }
    }
}
