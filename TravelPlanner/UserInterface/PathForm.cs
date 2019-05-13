using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;
using TravelPlanner.Application;
using TravelPlanner.Domain;

namespace TravelPlanner.UserInterface
{
    class PathForm : MetroForm
    {
        private readonly IApplication app;
        private readonly Travel travel;

        public PathForm(IApplication app, Travel travel)
        {
            this.app = app;
            this.travel = travel;
            Size = new Size(600, 600);
            ShadowType = MetroFormShadowType.None;
            Controls.Add(GetAddButton());
        }

        public PathForm(IApplication app) : this(app, null)
        {
        }

        private Button GetAddButton()
        {
            var addButton = Elements.GetButton("Добавить событие", (sender, args) =>
            {
                var addForm = new AddForm(app);
                addForm.Show(this);
            });
            addButton.Dock = DockStyle.Bottom;
            return addButton;
        }
    }
}
