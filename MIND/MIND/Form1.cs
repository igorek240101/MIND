using MIND.Library;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MIND
{
    public partial class Form1 : Form
    {

        Timer timer = new Timer();

        public static Form1 main;

        public static string baseFamilyName = "Times New Roman";
        public static float emSize = 14.25F;
        List<LinesText> linesTexts = new List<LinesText>();

        string file = null;
        bool isRedactor = true;
        bool isPrev = true;
        Image mark = Image.FromFile(Application.StartupPath + "\\Resourse\\mark.jpg");
        SimpleLines simpleLines;
        public Form1()
        {
            InitializeComponent();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            splitContainer1.Panel1.Location = new Point(0, 0);
            splitContainer1.Panel2.Location = new Point(Size.Width / 2, 0);
            splitContainer1.SplitterDistance = Width / 2;
            редакторToolStripMenuItem.Image = mark;
            превьюToolStripMenuItem.Image = mark;
            textBox1.Location = new Point(5, 5);
            textBox1.Size = new Size(splitContainer1.Panel1.Width - 25, 29);
            main = this;
            simpleLines = new SimpleLines(textBox1.Text);
            linesTexts.Add(simpleLines);
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
            if (isRedactor && isPrev)
            {
                splitContainer1.Visible = true;
                splitContainer1.Panel1.Visible = true;
                splitContainer1.Panel2.Visible = true;
                splitContainer1.Panel1.Size = new Size(Size.Width / 2, Size.Height - 24);
                splitContainer1.Panel2.Size = new Size(Size.Width / 2, Size.Height - 24);
                splitContainer1.Panel2.Location = new Point(Size.Width / 2, 24);
                редакторToolStripMenuItem.Image = mark;
                превьюToolStripMenuItem.Image = mark;
                textBox1.Size = new Size(splitContainer1.Panel1.Width - 25, textBox1.Height);
                splitContainer1.Location = new Point(0, 0);
                splitContainer1.IsSplitterFixed = false;
                if (splitContainer1.SplitterDistance == 0 || splitContainer1.SplitterDistance == Width) splitContainer1.SplitterDistance = Width / 2;
            }
            else
            {
                if (isRedactor)
                {
                    splitContainer1.Visible = true;
                    splitContainer1.Panel1.Visible = true;
                    splitContainer1.Panel2.Visible = false;
                    splitContainer1.Panel1.Size = Size;
                    редакторToolStripMenuItem.Image = mark;
                    превьюToolStripMenuItem.Image = null;
                    textBox1.Size = new Size(splitContainer1.Panel1.Width - 25, textBox1.Height);
                    try
                    {
                        splitContainer1.SplitterDistance = Width;
                    }
                    catch { }
                    splitContainer1.Location = new Point(0, 0);
                    splitContainer1.IsSplitterFixed = true;
                }
                else
                {
                    if (isPrev)
                    {
                        splitContainer1.Visible = true;
                        splitContainer1.Panel1.Visible = false;
                        splitContainer1.Panel2.Visible = true;
                        splitContainer1.Panel2.Size = Size;
                        splitContainer1.Panel2.Location = new Point(0, 24);
                        редакторToolStripMenuItem.Image = null;
                        превьюToolStripMenuItem.Image = mark;
                        splitContainer1.SplitterDistance = 0;
                        splitContainer1.Location = new Point(-4, 0);
                        splitContainer1.IsSplitterFixed = true;
                    }
                    else
                    {
                        splitContainer1.Visible = false;
                        редакторToolStripMenuItem.Image = null;
                        превьюToolStripMenuItem.Image = null;
                    }
                }
            }
            textBox_Resize();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Mod_Change();
        }

        private void textBox_Resize()
        {
            int maxsize = splitContainer1.Panel1.Width - 25;
            for (int i = 0; i < textBox1.Lines.Length; i++)
                if (textBox1.Padding.Horizontal + TextRenderer.MeasureText(textBox1.Lines[i], textBox1.Font).Width + 22 > maxsize) maxsize = textBox1.Padding.Horizontal + TextRenderer.MeasureText(textBox1.Lines[i], textBox1.Font).Width + 22;
            textBox1.Size = new Size(maxsize, (textBox1.Text.Split('\n').Length - 1) * 22 + 29);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox_Resize();
            if (textBox1.Text.Length > 200)
            {

                timer.Stop();
                timer.Start();
            }
            else
            {
                simpleLines = new SimpleLines(textBox1.Text);
                simpleLines.value.Location = new Point(10, 0);
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(simpleLines.value);
                linesTexts[0] = simpleLines;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            textBox_Resize();

            simpleLines = new SimpleLines(textBox1.Text);
            simpleLines.value.Location = new Point(10, 0);
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(simpleLines.value);
            linesTexts[0] = simpleLines;
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
            if (file != null)
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

        private void изображениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Enabled = false;
            ImageInsert imageInsert = new ImageInsert(this);
            imageInsert.Show();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            textBox_Resize();
        }

        private void полужирныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int start = textBox1.SelectionStart, end = textBox1.SelectionLength + textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(end, "**");
            textBox1.Text = textBox1.Text.Insert(start, "**");
        }

        private void курсивToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int start = textBox1.SelectionStart, end = textBox1.SelectionLength + textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(end, "*");
            textBox1.Text = textBox1.Text.Insert(start, "*");
        }

        private void полужирныйКурсивToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int start = textBox1.SelectionStart, end = textBox1.SelectionLength + textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(end, "***");
            textBox1.Text = textBox1.Text.Insert(start, "***");
        }

        private void подчеркиваниеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int start = textBox1.SelectionStart, end = textBox1.SelectionLength + textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(end, "~");
            textBox1.Text = textBox1.Text.Insert(start, "~");
        }

        private void зачеркнутоToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int start = textBox1.SelectionStart, end = textBox1.SelectionLength + textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(end, "~~");
            textBox1.Text = textBox1.Text.Insert(start, "~~");
        }

        private void зачеркнутоПодчеркнутоеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int start = textBox1.SelectionStart, end = textBox1.SelectionLength + textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(end, "~~~");
            textBox1.Text = textBox1.Text.Insert(start, "~~~");
        }

        private void экспортВPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PDF_Creator.Create(linesTexts);
        }
    }
}
