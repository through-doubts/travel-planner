using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;
using TravelPlanner.Application;
using TravelPlanner.Domain;

namespace TravelPlanner.UserInterface
{
    class PathForm : MetroForm
    {
        private readonly MetroForm addForm;

        public PathForm(MetroForm addForm)
        {
            this.addForm = addForm;
            Size = new Size(600, 600);
            ShadowType = MetroFormShadowType.None;
            Controls.Add(GetAddButton());
        }

        private Button GetAddButton()
        {
            var addButton = Elements.GetButton("Добавить событие", (sender, args) =>
            {
                addForm.ShowDialog(this);
            });
            addButton.Dock = DockStyle.Bottom;
            return addButton;
        }
    }
}
