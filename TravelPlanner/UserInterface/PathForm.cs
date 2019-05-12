using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace TravelPlanner.UserInterface
{
    class PathForm : MetroForm
    {
        public PathForm()
        {
            Size = new Size(600, 600);
            GetAddButton();
            ShadowType = MetroFormShadowType.None;
            Controls.Add(GetAddButton());
        }

        private Button GetAddButton()
        {
            return Elements.GetBottomButton("Добавить событие", (sender, args) =>
            {
                var addForm = new AddForm();
                addForm.Show(this);
            });
        }
    }
}
