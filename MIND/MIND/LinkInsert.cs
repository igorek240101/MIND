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
    public partial class LinkInsert : Form
    {
        Form1 parent;
        public LinkInsert(Form1 sender)
        {
            parent = sender;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                if (textBox3.Text != "") parent.textBox1.Text = parent.textBox1.Text.Insert(parent.textBox1.SelectionStart, "[" + textBox1.Text + "](" + textBox2.Text + " \"" + textBox3.Text + "\")");
                else parent.textBox1.Text = parent.textBox1.Text.Insert(parent.textBox1.SelectionStart, "[" + textBox1.Text + "](" + textBox2.Text + ")");
                Close();
            }
            else MessageBox.Show("Заполните поля \"Текст\" и \"Ссылка\".", "Поля не заполнены" , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LinkInsert_FormClosed(object sender, FormClosedEventArgs e)
        {
            parent.Enabled = true;
        }
    }
}
