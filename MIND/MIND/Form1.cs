using MIND.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MIND
{
    public partial class Form1 : Form
    {


        public static Form1 main;

        public static string baseFamilyName = "Times New Roman";
        public static float emSize = 14.25F;


        string file = null;
        bool isRedactor = true;
        bool isPrev = true;
        Image mark = Image.FromFile(Application.StartupPath + "\\Resourse\\mark.jpg");
        SimpleLines simpleLines;
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
            main = this;
            simpleLines = new SimpleLines(textBox1.Text);
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

            simpleLines = new SimpleLines(textBox1.Text);

            panel2.Controls.Clear();
            panel2.Controls.Add(simpleLines.value);
        }

        private void новыйФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (file != null || textBox1.Text != "")
                if (MessageBox.Show("Вы уверены? Не сохраненные данные будут потеряны!", "Новый документ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
            textBox1.Text = "";
            file = null;
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (file != null || textBox1.Text != "")
                if (MessageBox.Show("Вы уверены? Не сохраненные данные будут потеряны!", "Новый документ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = Application.StartupPath;
            fileDialog.Title = "Открыть";
            fileDialog.CheckPathExists = true;
            fileDialog.CheckFileExists = true;
            fileDialog.ShowDialog();
            try
            {
                file = fileDialog.FileName;
                textBox1.Text = File.ReadAllText(file);
            }
            catch { file = null; }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(file != null)
            {
                File.WriteAllText(file, textBox1.Text);
            }
            else
            {
                сохранитьКакToolStripMenuItem_Click(sender, e);
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = Application.StartupPath;
            saveFile.Title = "Сохранить как";
            saveFile.FileOk += new CancelEventHandler(SaveAs);
            saveFile.ShowDialog();
        }

        private void SaveAs(object sender, CancelEventArgs e)
        {
            file = (sender as SaveFileDialog).FileName;
            File.WriteAllText(file, textBox1.Text);
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ссылкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Enabled = false;
            LinkInsert linkInsert = new LinkInsert(this);
            linkInsert.Show();
        }
    }
}
