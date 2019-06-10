using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TravelPlanner.Application;

namespace TravelPlanner.UserInterface
{
    sealed class ApplicationForm : ChooseOptionForm
    {
        private readonly IApplication app;

        public ApplicationForm(IApplication app) : base(app.UserSessionHandler.GetTravelsNames)
        {
            this.app = app;
            Size = new Size(800, 600);
            Text = "Путешествия";
        }

        private Button GetAddButton()
        {
            var addButton = Elements.GetButton("Добавить путешествие", (sender, args) =>
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
            return travelButton;
        }

        protected override List<Button> GetButtons()
        {
            return new List<Button> {GetAddButton()};
        }

        protected override Button GetOptionButton(string optionName)
        {
            return GetTravelButton(optionName);
        }
    }
}
