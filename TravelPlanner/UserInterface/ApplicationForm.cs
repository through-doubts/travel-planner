using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
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
            base(app.UserSessionHandler.Travels.GetItems, (() => app))
        {
            this.app = app;
            this.getPathForm = getPathForm;
            this.getEnterForm = getEnterForm;
            Size = new Size(800, 600);
            Text = "Путешествия";
            FormClosing += OnClose;
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

        public Button GetTravelButton(Travel travel, Func<IApplication> getApp)
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
            travelButton.ContextMenuStrip = GetTravelButtonStrip(travel, getApp);
            return travelButton;
        }

        private ContextMenuStrip GetTravelButtonStrip(Travel travel, Func<IApplication> getApp)
        {
            var contextMenu = new ContextMenuStrip();
            var delete = new ToolStripMenuItem("Удалить");
            var export = new ToolStripMenuItem("Экспортировать");
            var currentApp = getApp();
            export.DropDownItems.AddRange(currentApp.FormatsHandler.GetFormatsNames()
                .Select(x => new ToolStripMenuItem(x, null, (sender, args) =>
                {
                    var format = app.FormatsHandler.GetFormatByName(x);
                    var dialog = new SaveFileDialog {Filter = $"{x} files (*.{x})|*.{x}|All files (*.*)|*.*"};
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        format.SaveTravel(dialog.FileName, travel);
                    }
                })).ToArray());
            delete.Click += (sender, args) =>
            {
                app.UserSessionHandler.Travels.Delete(travel);
                UpdateTable();
            };
            contextMenu.Items.Add(delete);
            contextMenu.Items.Add(export);
            return contextMenu;
        }

        protected override List<Button> GetButtons()
        {
            return new List<Button> {GetAddButton()};
        }

        protected override Button GetOptionButton(Travel option, Func<IApplication> getApp = null)
        {
            return GetTravelButton(option, getApp);
        }

        private void OnClose(object sender, CancelEventArgs args)
        {
            app.UserSessionHandler.SaveUsers();
        }
    }
}
