using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace TravelPlanner.UserInterface
{
    sealed class EnterForm : MetroForm
    {
        public string SelectedText { get; private set; }
        private readonly Button okButton;
        private readonly TextBox box;

        public EnterForm(string text)
        {
            ShadowType = MetroFormShadowType.None;
            Text = text;
            Size = new Size(300, 200);
            Resizable = false;
            MaximizeBox = false;
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
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            table.Controls.Add(box, 0, 0);
            table.Controls.Add(okButton, 0, 1);

            table.Dock = DockStyle.Fill;
            Controls.Add(table);
        }

        public static bool TryShowAndGetValue(Form innerForm,EnterForm enterForm, out string value)
        {
            var result = enterForm.ShowDialog(innerForm) == DialogResult.OK;
            value = enterForm.SelectedText;
            return result;
        }
    }
}
