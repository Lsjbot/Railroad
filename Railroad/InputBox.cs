using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Railroad
{
    public partial class InputBox : Form
    {
        public InputBox(string prompt,string pretext, bool hidetext)
        {
            InitializeComponent();
            label1.Text = prompt;
            textBox1.Text = pretext;
            if (hidetext)
            {
                textBox1.PasswordChar = '*';
                textBox1.UseSystemPasswordChar = true;
            }
            textBox1.Focus();
            textBox1.Select();

            
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public string gettext()
        {
            return textBox1.Text;
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
