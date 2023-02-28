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
    public partial class FormTrainlist : Form
    {
        private playerclass pl;
        private mapclass map;
        public FormTrainlist(playerclass plpar, mapclass mappar)
        {
            InitializeComponent();
            pl = plpar;
            map = mappar;
        }

        private void newtrainbutton_Click(object sender, EventArgs e)
        {
            FormNewtrain fn = new FormNewtrain(map);
            fn.ShowDialog();
            if (fn.DialogResult == DialogResult.OK)
            {
                trainclass tc = fn.GetNewTrain();
                if (tc != null)
                {
                    pl.trains.Add(tc);
                    LB_trains.Items.Add(tc.ToString());
                }
            }
        }
    }
}
