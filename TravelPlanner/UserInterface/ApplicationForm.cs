using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TravelPlanner.Application;
using TravelPlanner.Domain;

namespace TravelPlanner.UserInterface
{
    sealed class ApplicationForm : ChooseOptionForm<Travel>
    {
        private readonly IApplication app;
        private readonly Func<PathForm> getPathForm;
        private readonly Func<string, EnterForm> getEnterForm;

        public ApplicationForm(IApplication app, Func<PathForm> getPathForm, Func<string, EnterForm> getEnterForm) : 
            base(app.UserSessionHandler.Travels.GetItems)
        {
            this.app = app;
            this.getPathForm = getPathForm;
            this.getEnterForm = getEnterForm;
            Size = new Size(800, 600);
            Text = "Путешествия";
        }

        private Button GetAddButton()
        {
            var addButton = Elements.GetButton("Добавить путешествие", (sender, args) =>
            {
                var enterForm = getEnterForm("Введите имя");
                if (!EnterForm.TryShowAndGetValue(this, enterForm, out var name)) return;
                Hide();
                var travel = app.TravelFabric.Get(name);
                app.UserSessionHandler.Travels.Add(travel);
                app.UserSessionHandler.ChangeCurrentTravel(travel);
                var pathForm = getPathForm();
                pathForm.ShowDialog(this);
                UpdateTable();
                Show();
            });
            return addButton;
        }

        public Button GetTravelButton(Travel travel)
        {
            var travelButton = Elements.GetButton(travel.Name, (sender, args) =>
            {
                app.UserSessionHandler.ChangeCurrentTravel(travel);
                Hide();
                var pathForm = getPathForm();
                pathForm.ShowDialog(this);
                UpdateTable();
                Show();
            });
            travelButton.ContextMenuStrip = GetTravelButtonStrip(travel);
            return travelButton;
        }

        private ContextMenuStrip GetTravelButtonStrip(Travel travel)
        {
            var contextMenu = new ContextMenuStrip();
            var delete = new ToolStripMenuItem("Удалить");
            delete.Click += (sender, args) =>
            {
                app.UserSessionHandler.Travels.Delete(travel);
                UpdateTable();
            };
            contextMenu.Items.Add(delete);
            return contextMenu;
        }

        protected override List<Button> GetButtons()
        {
            return new List<Button> {GetAddButton()};
        }

        protected override Button GetOptionButton(Travel option)
        {
            return GetTravelButton(option);
        }
    }
}
