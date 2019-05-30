using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace TravelPlanner.UserInterface
{
    class PathForm : MetroForm
    {
        private readonly MetroForm addForm;

        public PathForm(MetroForm addForm)
        {
            this.addForm = addForm;
            Size = new Size(800, 600);
            ShadowType = MetroFormShadowType.None;
            InitTable();
        }

        private void InitTable()
        {
            var table = new TableLayoutPanel();
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 80));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 10));

            table.Controls.Add(GetAddButton(), 0, 1);
            table.Controls.Add(Elements.BackButton(this, "Назад"), 0, 2);

            table.Dock = DockStyle.Fill;
            Controls.Add(table);
        }

        private Button GetAddButton()
        {
            var addButton = Elements.GetButton("Добавить событие", (sender, args) =>
            {
                Hide();
                addForm.ShowDialog(this);
                Show();
            });
            addButton.Dock = DockStyle.Bottom;
            return addButton;
        }
    }
}
