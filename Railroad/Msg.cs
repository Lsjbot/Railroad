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
    public partial class Msg : Form
    {
        public static Msg msgwindow;
        public Msg()
        {
            InitializeComponent();
            msgwindow = this;

            this.StartPosition = FormStartPosition.Manual;
            this.Left = -1200;
            this.Top = 1000;
            //this.Location = Screen.AllScreens[1].WorkingArea.Location;
        }

        public static void memo(string s)
        {
            msgwindow.richTextBox1.AppendText(s + "\n");
            msgwindow.richTextBox1.ScrollToCaret();
        }


    }
}
