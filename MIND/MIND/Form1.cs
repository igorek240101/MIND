﻿using MIND.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MIND
{
    public partial class Form1 : Form
    {

        Timer timer = new Timer();

        public static Form1 main;


        public static int w_len;

        public static string baseFamilyName = "Times New Roman";
        public static float emSize = 14.25F;
        List<LinesText> linesTexts = new List<LinesText>();
        public static List<LineLinesControl> lineLines = new List<LineLinesControl>();

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
            w_len = splitContainer1.Panel2.Width;
            simpleLines = new SimpleLines(textBox1.Text, 8);
            linesTexts.Add(new QuotationLines(">Стэк — это такой товарищ,\r\n>который не знает меры.\r\n>Он придёт в любой бар и напьётся в хламину.\r\n>\r\n>\r\n>Спиридонов Р.Е.", 0));
            linesTexts.Add(new HeaderLines("#### Заголовок1", 1));
            linesTexts.Add(new LineLines("***", 2));
            linesTexts.Add(new LineLines("---", 3));
            linesTexts.Add(new LineLines("___", 4));
            linesTexts[2].value.Size = new Size(splitContainer1.Panel2.Width-20, 20);
            linesTexts[3].value.Size = new Size(splitContainer1.Panel2.Width-20, 20);
            linesTexts[4].value.Size = new Size(splitContainer1.Panel2.Width-20, 20);
            linesTexts.Add(new LineCode("```\r\nstring s = Console.ReadLine();\r\ns = s.Reverse();\r\nConsole.WriteLine(s)\r\n```", 5));
            linesTexts.Add(new ChekLine("[X] Написать код", 6));
            linesTexts.Add(new TableLines("| Plugin | README |\r\n| :------: | ------ |\r\n| Dropbox | dsfsdfsdfs |\r\n| GitHub | [plugins / github / README.md][PlGh] |\r\n| Google Drive | [plugins / googledrive / README.md][PlGd] |\r\n| OneDrive | [plugins / onedrive / README.md][PlOd] |\r\n| Medium | [plugins / medium / README.md][PlMe] |\r\n| ![](a) | ![](a)| ", 7));
            linesTexts.Add(simpleLines);
            splitContainer1.Panel2.Controls.Add(linesTexts[0].value);
            splitContainer1.Panel2.Controls.Add(linesTexts[1].value);
            splitContainer1.Panel2.Controls.Add(linesTexts[2].value);
            splitContainer1.Panel2.Controls.Add(linesTexts[3].value);
            splitContainer1.Panel2.Controls.Add(linesTexts[4].value);
            splitContainer1.Panel2.Controls.Add(linesTexts[5].value);
            splitContainer1.Panel2.Controls.Add(linesTexts[6].value);
            splitContainer1.Panel2.Controls.Add(linesTexts[7].value);
            splitContainer1.Panel2.Controls.Add(linesTexts[8].value);
            linesTexts[0].value.Location = new Point(10, 10);
            linesTexts[1].value.Location = new Point(10, linesTexts[0].value.Location.Y + linesTexts[0].value.Height);
            linesTexts[2].value.Location = new Point(10, linesTexts[1].value.Location.Y + linesTexts[1].value.Height);
            linesTexts[3].value.Location = new Point(10, linesTexts[2].value.Location.Y + linesTexts[2].value.Height);
            linesTexts[4].value.Location = new Point(10, linesTexts[3].value.Location.Y + linesTexts[3].value.Height);
            linesTexts[5].value.Location = new Point(10, linesTexts[4].value.Location.Y + linesTexts[4].value.Height);
            linesTexts[6].value.Location = new Point(10, linesTexts[5].value.Location.Y + linesTexts[5].value.Height);
            linesTexts[7].value.Location = new Point(10, linesTexts[6].value.Location.Y + linesTexts[6].value.Height);
            linesTexts[8].value.Location = new Point(10, linesTexts[7].value.Location.Y + linesTexts[7].value.Height);
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
            Console.WriteLine(splitContainer1.Panel2.Width);
            textBox_Resize();
            if (textBox1.Text.Length > 200)
            {

                timer.Stop();
                timer.Start();
            }
            else
            {
                MyResetText();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            textBox_Resize();
            MyResetText();
        }

        private void MyResetText()
        {
            if (linesTexts.Count > 1)
            {
                int start = textBox1.Text.Substring(0, textBox1.SelectionStart).Split('\r').Length;
                int end = textBox1.Text.Substring(0, textBox1.SelectionStart+textBox1.SelectionLength).Split('\r').Length;
                for(int i = 0; i < linesTexts.Count; i++)
                {
                    if(linesTexts[i].startString >= start)
                    {
                        if (i != 0)
                        {
                            start = linesTexts[i - 1].startString;
                        }
                        else start = 0;
                        break;
                    }
                }
                for (int i = start; i < linesTexts.Count;)
                {
                    if (linesTexts[i].startString >= end)
                    {
                        if (i+1 < linesTexts.Count)
                        {
                            linesTexts.RemoveAt(i);
                            end = linesTexts[i].startString-1;
                        }
                        else end = textBox1.Text.Split('\r').Length-1;
                        linesTexts.RemoveAt(i);
                        break;
                    }
                    linesTexts.RemoveAt(i);
                }
            }
            else
            {
                simpleLines = new SimpleLines(textBox1.Text, 2);
                simpleLines.value.Location = new Point(10, 0);
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(simpleLines.value);
                linesTexts[0] = simpleLines;
            }
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
