using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIND
{
    public partial class HTML_visor : Form
    {
        public HTML_visor(string label)
        {
            InitializeComponent();
            label1.Text = label;
        }

        private void HTML_visor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1.main.Enabled = true;
        }
    }
}
