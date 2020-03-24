using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace MedicalSystem
{
    public partial class EnterData : Form
    {
        private List<TextBox> Inputs;

        public EnterData()
        {
            InitializeComponent();

            Inputs = new List<TextBox>();
            var propInfo = typeof(Patient).GetProperties();
            for (int i = 0; i < propInfo.Length; i++)
            {
                var property = propInfo[i];

                var textBox = CreateTextBox(i, property);
                Controls.Add(textBox);
                Inputs.Add(textBox);
            }
        }

        public bool? ShowForm()
        {
            var form = new EnterData();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var patient = new Patient();

                foreach (var textBox in Inputs)
                {
                    patient.GetType().InvokeMember(textBox.Tag.ToString(),
                                                   BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty,
                                                   Type.DefaultBinder,
                                                   patient,
                                                   new object[] { textBox.Text });
                }

                var result = Program.SystemController.DataNetwork.Predict().Output;

                return result == 1.0;
            }

            return null;
        }

        private TextBox CreateTextBox(int number, PropertyInfo property)
        {
            var textBox = new TextBox
            {
                Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right),
                Location = new Point(30, 20 * number + 10 + 10 * number),
                Name = "textBox" + number,
                Size = new Size(300, 22),
                TabIndex = 0,
                Text = property.Name,
                Tag = property.Name,
                ForeColor = Color.Gray,
                Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Italic, GraphicsUnit.Point, 204)
            };

            textBox.GotFocus  += TextBox_GotFocus;
            textBox.LostFocus += TextBox_LostFocus;

            return textBox;
        }

        private void TextBox_LostFocus(object sender, System.EventArgs e)
        {
            var textBox = sender as TextBox;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = textBox.Tag.ToString();
                textBox.ForeColor = Color.Gray;
                textBox.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Italic, GraphicsUnit.Point, 204);
            }
        }

        private void TextBox_GotFocus(object sender, System.EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == textBox.Tag.ToString())
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
                textBox.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
            }
        }

        private void predictButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
