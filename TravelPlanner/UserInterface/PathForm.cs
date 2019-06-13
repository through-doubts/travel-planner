using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TravelPlanner.Application;
using TravelPlanner.Domain;
using TravelPlanner.Domain.TravelEvents;
using TravelPlanner.UserInterface.EventForms;

namespace TravelPlanner.UserInterface
{
    sealed class PathForm : ChooseOptionForm<ITravelEvent>
    {
        private readonly IApplication app;
        private readonly TravelEventFormFactory travelEventFormFactory;

        public PathForm(IApplication app, TravelEventFormFactory travelEventFormFactory) : 
            base(app.UserSessionHandler.CurrentTravelEvents.GetItems)
        {
            this.app = app;
            this.travelEventFormFactory = travelEventFormFactory;
            Size = new Size(800, 600);
            Text = "События";
        }

        private Button GetAddButton()
        {
            var addButton = Elements.GetButton("Добавить событие", (sender, args) =>
            {
                Hide();
                travelEventFormFactory.CreateAddForm().ShowDialog(this);
                UpdateTable();
                Show();
            });
            return addButton;
        }

        private Button GetTravelEventButton(ITravelEvent travelEvent)
        {
            var eventButton = Elements.GetButton(travelEvent.ToStringValue(), (sender, args) =>
            {
                Hide();
                travelEventFormFactory.CreateEditForm(travelEvent).ShowDialog(this);
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
                app.UserSessionHandler.CurrentTravelEvents.Delete(travelEvent);
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
