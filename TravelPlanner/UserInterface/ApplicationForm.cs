using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace TravelPlanner.UserInterface
{
    class ApplicationForm : MetroForm
    {
        public ApplicationForm()
        {
            Size = new Size(800, 600);
            GetAddButton();
            ShadowType = MetroFormShadowType.None;
            Controls.Add(GetAddButton());
        }

        private Button GetAddButton()
        {
            return Elements.GetBottomButton("Добавить", (sender, args) =>
            {
                var createPathForm = new PathForm();
                createPathForm.Show(this);
            });
        }

    }
}
