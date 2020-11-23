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
    public partial class Form1 : Form
    {
        bool isRedactor = true;
        bool isPrev = true;
        Image mark = Image.FromFile(Application.StartupPath + "\\Resourse\\mark.jpg");
        public Form1()
        {
            InitializeComponent();
            panel1.Size = new Size(Size.Width / 2, Size.Height - 24); 
            panel2.Size = new Size(Size.Width / 2, Size.Height - 24);
            panel1.Location = new Point(0, 24);
            panel2.Location = new Point(Size.Width/2, 24);
            редакторToolStripMenuItem.Image = mark;
            превьюToolStripMenuItem.Image = mark;
            textBox1.Location = new Point(5, 5);
            textBox1.Size = new Size(panel1.Width-25, 29);
        }

        private void предпросмотрHtmlToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void редакторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isRedactor = !isRedactor;
            Mod_Change();
        }

        private void превьюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isPrev = !isPrev;
            Mod_Change();
        }

        private void Mod_Change()
        {
            if(isRedactor && isPrev)
            {
                panel1.Visible = true;
                panel2.Visible = true;
                panel1.Size = new Size(Size.Width / 2, Size.Height - 24);
                panel2.Size = new Size(Size.Width / 2, Size.Height - 24);
                panel2.Location = new Point(Size.Width / 2, 24);
                редакторToolStripMenuItem.Image = mark;
                превьюToolStripMenuItem.Image = mark;
                textBox1.Size = new Size(panel1.Width - 25, textBox1.Height);
            }
            else
            {
                if(isRedactor)
                {
                    panel1.Visible = true;
                    panel2.Visible = false;
                    panel1.Size = Size;
                    редакторToolStripMenuItem.Image = mark;
                    превьюToolStripMenuItem.Image = null;
                    textBox1.Size = new Size(panel1.Width - 25, textBox1.Height);
                }
                else
                {
                    if(isPrev)
                    {
                        panel1.Visible = false;
                        panel2.Visible = true;
                        panel2.Size = Size;
                        panel2.Location = new Point(0, 24);
                        редакторToolStripMenuItem.Image = null;
                        превьюToolStripMenuItem.Image = mark;
                    }
                    else
                    {
                        panel1.Visible = false;
                        panel2.Visible = false;
                        редакторToolStripMenuItem.Image = null;
                        превьюToolStripMenuItem.Image = null;
                    }
                }
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Mod_Change();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Size = new Size(panel1.Width - 25, (textBox1.Text.Split('\n').Length-1) * 22 + 29);
        }
    }
}
