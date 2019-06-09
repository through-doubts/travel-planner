using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TravelPlanner.Application;

namespace TravelPlanner.UserInterface
{
    class ApplicationForm : ChooseOptionForm
    {
        private readonly IApplication app;

        public ApplicationForm(IApplication app) : base(app.UserSessionHandler.GetTravelsNames)
        {
            this.app = app;
            Size = new Size(800, 600);
        }

        private Button GetAddButton()
        {
            var addButton = Elements.GetButton("Добавить", (sender, args) =>
            {
                string name;
                var enterForm = new EnterForm();
                if (enterForm.ShowDialog(this) == DialogResult.OK)
                    name = enterForm.SelectedText;
                else
                    return;
                Hide();
                app.UserSessionHandler.AddTravel(name);
                var pathForm = new PathForm(app);
                pathForm.ShowDialog(this);
                UpdateTable();
                Show();
            });
            addButton.Dock = DockStyle.Fill;
            return addButton;
        }

        public Button GetTravelButton(string travelName)
        {
            var travelButton = Elements.GetButton(travelName, (sender, args) =>
            {
                app.UserSessionHandler.ChangeCurrentTravel(travelName);
                Hide();
                var pathForm = new PathForm(app);
                pathForm.ShowDialog(this);
                UpdateTable();
                Show();
            });
            travelButton.Dock = DockStyle.Fill;
            return travelButton;
        }

        protected override IEnumerable<Button> GetButtons()
        {
            yield return GetAddButton();
        }

        protected override Button GetOptionButton(string optionName)
        {
            return GetTravelButton(optionName);
        }
    }
}
