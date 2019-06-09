using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace TravelPlanner.UserInterface
{ 
    class EnterForm : MetroForm
    {
        public string SelectedText { get; private set; }
        private readonly Button okButton;
        private readonly TextBox box;

        public EnterForm()
        {
            ShadowType = MetroFormShadowType.None;
            Size = new Size(300, 200);
            BorderStyle = MetroFormBorderStyle.FixedSingle;
            okButton = Elements.GetButton("OK", (sender, args) =>
            {
                DialogResult = DialogResult.OK;
                SelectedText = box.Text;
                Close();
            });
            okButton.Dock = DockStyle.Fill;
            box = new TextBox {Dock = DockStyle.Fill};
            InitTable();
        }

        private void InitTable()
        {
            var table = new TableLayoutPanel();
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            table.Controls.Add(Elements.GetLabel("Имя"), 0, 0);
            table.Controls.Add(box, 1, 0);
            table.Controls.Add(okButton, 1, 1);

            table.Dock = DockStyle.Fill;
            Controls.Add(table);
        }
    }
}
