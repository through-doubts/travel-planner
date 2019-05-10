using System.Drawing;
using MetroFramework.Forms;

namespace TravelPlanner.UserInterface
{
    class PathForm : MetroForm
    {
        public PathForm()
        {
            Size = new Size(600, 600);
            AddAddButton();
        }

        private void AddAddButton()
        {
            var addButton = Elements.GetBottomButton("Добавить событие");
            addButton.Click += (sender, args) =>
            {
                var addForm = new AddForm();
                addForm.Show(this);
            };
            Controls.Add(addButton);
        }
    }
}
