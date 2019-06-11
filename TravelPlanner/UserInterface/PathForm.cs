using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TravelPlanner.Application;
using TravelPlanner.Domain;

namespace TravelPlanner.UserInterface
{
    sealed class PathForm : ChooseOptionForm<ITravelEvent>
    {
        private readonly IApplication app;
        private readonly TravelEventFormFactory addFormFactory;

        public PathForm(IApplication app, TravelEventFormFactory addFormFactory) : base(app.UserSessionHandler.GetTravelEvents)
        {
            this.app = app;
            this.addFormFactory = addFormFactory;
            Size = new Size(800, 600);
            Text = "События";
        }

        private Button GetAddButton()
        {
            var addButton = Elements.GetButton("Добавить событие", (sender, args) =>
            {
                Hide();
                addFormFactory.CreateAddForm().ShowDialog(this);
                Show();
                UpdateTable();
            });
            return addButton;
        }

        private Button GetTravelEventButton(ITravelEvent travelEvent)
        {
            var eventButton = Elements.GetButton(travelEvent.ToStringValue(), (sender, args) =>
            {
                Hide();
                addFormFactory.CreateEditForm(travelEvent).ShowDialog(this);
                UpdateTable();
                Show();
            });
            eventButton.ContextMenuStrip = GetTravelEventButtonStrip(travelEvent);
            return eventButton;
        }

        private ContextMenuStrip GetTravelEventButtonStrip(ITravelEvent travelEvent)
        {
            var contextMenu = new ContextMenuStrip();
            var fix = new ToolStripMenuItem("Зафиксировать");
            var delete = new ToolStripMenuItem("Удалить");
            delete.Click += (sender, args) =>
            {
                app.UserSessionHandler.DeleteEvent(travelEvent);
            };
            contextMenu.Items.AddRange(new ToolStripItem[] {fix, delete});
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

        protected override Button GetOptionButton(ITravelEvent option)
        {
            return GetTravelEventButton(option);
        }
    }
}
