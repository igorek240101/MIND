using MIND.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MIND
{
    /// <summary>
    /// Класс основной формы
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Таймер для обработки очень быстрого набор текста
        /// </summary>
        Timer timer = new Timer();

        /// <summary>
        /// Основная форма программы
        /// </summary>
        public static Form1 main;

        /// <summary>
        /// Основной шрифт
        /// </summary>
        public static string baseFamilyName = "Times New Roman";

        /// <summary>
        /// Основной размер шрифта
        /// </summary>
        public static float emSize = 14.25F;

        /// <summary>
        /// Элементы визуализации
        /// </summary>
        List<LinesText> linesTexts = new List<LinesText>();

        /// <summary>
        /// Имя текущего файла
        /// </summary>
        string file = null;

        /// <summary>
        /// Флаг - окно редактора включено
        /// </summary>
        bool isRedactor = true; 

        /// <summary>
        /// Флаг - окно препросмотра включено
        /// </summary>
        bool isPrev = true;

        /// <summary>
        /// Изображение, используемое для отображения подключенности того или иного окна
        /// </summary>
        Image mark = Image.FromFile(Application.StartupPath + "\\Resourse\\mark.jpg");

        /// <summary>
        /// Количество строк в текстовом поле
        /// </summary>
        int countOfLine = 1;
        
        /// <summary>
        /// Значение для курсора в случае смещения в связи с использованием метода <c>Insert()</c>
        /// </summary>
        int selected = -1;

        /// <summary>
        /// Создание основной формы
        /// </summary>
        /// <param name="s">Путь к файлу, который открывается в программе</param>
        public Form1(string s)
        {
            InitializeComponent();
            timer.Interval = 150;
            timer.Tick += new EventHandler(timer_Tick);

            splitContainer1.Panel1.Location = new Point(0, 0);
            splitContainer1.Panel2.Location = new Point(Size.Width / 2, 0);
            splitContainer1.SplitterDistance = Width / 2;

            redactorToolStripMenuItem.Image = mark;
            visorToolStripMenuItem.Image = mark;

            textBox1.Location = new Point(5, 5);
            textBox1.Size = new Size(splitContainer1.Panel1.Width - 25, 29);
            main = this;

            if(s != null)
            {
                file = s;
                textBox1.Text = File.ReadAllText(file);
            }

            LinesStruct.MainSearch(textBox1.Text, 0, linesTexts, splitContainer1.Panel2, 0, this);
            int lastY = 10;
            for (int i = 0; i < linesTexts.Count; i++)
            {
                linesTexts[i].value.Location = new Point(10, lastY);
                lastY += linesTexts[i].value.Height;
            }
        }

        /// <summary>
        /// Обработчик событя предпросмотра конфертации в HTML
        /// </summary>
        private void HtmlvisorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTML_visor hTML_Visor = new HTML_visor(CommonMark.CommonMarkConverter.Convert(textBox1.Text));
            Enabled = false;
            hTML_Visor.Show();
        }

        /// <summary>
        /// Обработчик событя смены свойства включения/выключения окна редактора
        /// </summary>
        private void redactorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isRedactor = !isRedactor;
            Mod_Change();
        }

        /// <summary>
        /// Обработчик событя смены свойства включения/выключения окна препросмотра
        /// </summary>
        private void visorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isPrev = !isPrev;
            Mod_Change();
        }

        /// <summary>
        /// Установка параметров окон
        /// </summary>
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
                redactorToolStripMenuItem.Image = mark;
                visorToolStripMenuItem.Image = mark;
                textBox1.Size = new Size(splitContainer1.Panel1.Width - 25, textBox1.Height);
                splitContainer1.Location = new Point(0, 0);
                splitContainer1.IsSplitterFixed = false;
                try
                {
                    splitContainer1.SplitterDistance = Width / 2;
                }
                catch { }
            }
            else
            {
                if (isRedactor)
                {
                    splitContainer1.Visible = true;
                    splitContainer1.Panel1.Visible = true;
                    splitContainer1.Panel2.Visible = false;
                    splitContainer1.Panel1.Size = Size;
                    redactorToolStripMenuItem.Image = mark;
                    visorToolStripMenuItem.Image = null;
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
                        redactorToolStripMenuItem.Image = null;
                        visorToolStripMenuItem.Image = mark;
                        splitContainer1.SplitterDistance = 0;
                        splitContainer1.Location = new Point(-4, 0);
                        splitContainer1.IsSplitterFixed = true;
                    }
                    else
                    {
                        splitContainer1.Visible = false;
                        redactorToolStripMenuItem.Image = null;
                        visorToolStripMenuItem.Image = null;
                    }
                }
            }
            textBox_Resize();
        }

        /// <summary>
        /// Обработчик события изменения размеров формы
        /// </summary>
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Mod_Change();
        }

        /// <summary>
        /// Переопределение размера текстовоо окна
        /// </summary>
        private void textBox_Resize()
        {
            int maxsize = splitContainer1.Panel1.Width - 25;
            for (int i = 0; i < textBox1.Lines.Length; i++)
                if (textBox1.Padding.Horizontal + TextRenderer.MeasureText(textBox1.Lines[i], textBox1.Font).Width + 22 > maxsize) maxsize = textBox1.Padding.Horizontal + TextRenderer.MeasureText(textBox1.Lines[i], textBox1.Font).Width + 22;
            textBox1.Size = new Size(maxsize, (textBox1.Text.Split('\n').Length - 1) * 22 + 29);
        }

        /// <summary>
        /// Обработчик событя изменения текста в текстовом поле
        /// </summary>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox_Resize();
            timer.Stop();
            timer.Start();
        }

        /// <summary>
        /// Обработчик собитя срабатывания таймера
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            textBox_Resize();
            MyResetText();
        }

        /// <summary>
        /// Пересборка разметки
        /// </summary>
        private void MyResetText()
        {
            if (selected != -1) { textBox1.SelectionStart = selected; selected = -1; }
            if (linesTexts.Count > 1)
            {
                int countOfDelete, countOfEnter;
                if (countOfLine > textBox1.Lines.Length) countOfDelete = countOfLine - textBox1.Lines.Length; else countOfDelete = 0; //Количество удаленных строк
                if (countOfLine < textBox1.Lines.Length) countOfEnter = textBox1.Lines.Length - countOfLine; else countOfEnter = 0; // Количество добавленных строк
                int start = textBox1.Text.Substring(0, textBox1.SelectionStart).Split('\r').Length - 1 - countOfEnter; // Первая строка изменений 
                int end = 0; // Последняя строка изменений
                int startString = 0; // Первый изменяемый элемент визуализации
                int endString = 0; // Последний изменяемый элемент визуализации
                bool tr = true;
                for (int i = 0; i < linesTexts.Count; i++)
                {
                    if (linesTexts[i].startString > start) //Если мы нашли первую строку изменений
                    {
                        for (int j = i; j < linesTexts.Count; j++)
                        {
                            if (start + 1 <= linesTexts[j].startString - countOfDelete + countOfEnter) //  
                            {
                                if (start + 1 == linesTexts[j].startString - countOfDelete + countOfEnter) // Если строка граничит со следующим абзацем
                                {
                                    endString = j; // Следующий абзац последний на персбоку
                                    if (j + 1 < linesTexts.Count) // Определение последний строки этого абзаца
                                    {
                                        end = linesTexts[j + 1].startString - 1 - countOfDelete + countOfEnter;
                                    }
                                    else
                                    {
                                        end = textBox1.Lines.Length - 1;
                                    }
                                }
                                else
                                {
                                    endString = j - 1; // Последний абзац на персборку - предыдущий
                                    end = linesTexts[j].startString - 1 - countOfDelete + countOfEnter;
                                }
                                tr = false;
                                break;
                            }
                        }
                        if (tr)
                        {
                            end = textBox1.Lines.Length - 1; // Последняя строка текста
                            endString = linesTexts.Count - 1; ; // Последний блок
                            tr = false;
                        }
                        if (i - 2 >= 0) // Если старт не в первом блоке
                        {
                            if (linesTexts[i - 1].startString != start)
                            {
                                start = linesTexts[i - 1].startString;
                                startString = i - 1;
                            }
                            else
                            {
                                start = linesTexts[i - 2].startString; // Первая строка блока текста для которого будет проводиться сборка
                                startString = i - 2; // Индекс первого блока визуализации
                            }
                        }
                        else { start = 0; startString = 0; }
                        break;
                    }
                }
                if (tr)
                {
                    if (linesTexts.Count > 1 && linesTexts[linesTexts.Count - 1].startString == start)
                    {
                        startString = linesTexts.Count - 2;
                        start = linesTexts[linesTexts.Count - 2].startString; // Первая строка блока текста для которого будет проводиться сборка
                        end = textBox1.Lines.Length - 1;
                        endString = linesTexts.Count - 1;
                    }
                    else
                    {
                        startString = linesTexts.Count - 1;
                        start = linesTexts[linesTexts.Count - 1].startString; // Первая строка блока текста для которого будет проводиться сборка
                        end = textBox1.Lines.Length - 1;
                        endString = linesTexts.Count - 1;
                    }
                }
                for (int i = startString; i <= endString; i++) // удаление всех пересобираеммых элементов визуализации
                {
                    splitContainer1.Panel2.Controls.Remove(linesTexts[startString].value);
                    linesTexts.RemoveAt(startString);
                }
                for (int i = startString; i < linesTexts.Count; i++) // Сдвиг старотвых строк для находящихся ниже элементов визуализации
                    linesTexts[i].startString += countOfEnter - countOfDelete;
                string s = "";
                for (int i = start; i <= end; i++) // Подготовка текста для преобразования
                {
                    s += textBox1.Lines[i] + "\r\n";
                }
                try
                {
                    LinesStruct.MainSearch(s.Substring(0, s.Length - 2), start, linesTexts, splitContainer1.Panel2, startString, this);
                }
                catch { LinesStruct.MainSearch("", start, linesTexts, splitContainer1.Panel2, startString, this); }
                // Смещение визуализации
                int lastY = 10;
                if (start > 0) lastY = linesTexts[startString - 1].value.Location.Y + linesTexts[startString - 1].value.Height+10;
                for (int i = startString; i < linesTexts.Count; i++) 
                {
                    linesTexts[i].value.Location = new Point(10, lastY);
                    lastY += linesTexts[i].value.Height+10;
                }
            }
            else
            {
                splitContainer1.Panel2.Controls.Clear();
                linesTexts.RemoveAt(0);
                LinesStruct.MainSearch(textBox1.Text, 0, linesTexts, splitContainer1.Panel2, 0, this);
                int lastY = 10;
                for (int i = 0; i < linesTexts.Count; i++)
                {
                    linesTexts[i].value.Location = new Point(10, lastY);
                    lastY += linesTexts[i].value.Height;
                }
            }
            countOfLine = textBox1.Lines.Length;
        }

        /// <summary>
        /// Создает новый файл
        /// </summary>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (file != null || textBox1.Text != "")
                if (MessageBox.Show("Вы уверены? Не сохраненные данные будут потеряны!", "Новый документ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
            textBox1.Text = "";
            file = null;
        }

        /// <summary>
        /// Открывает новый файл
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
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

        /// <summary>
        /// Сохраняет файл
        /// </summary>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (file != null)
            {
                File.WriteAllText(file, textBox1.Text);
            }
            else
            {
                SaveasToolStripMenuItem_Click(sender, e);
            }
        }

        /// <summary>
        /// Сохраняет как
        /// </summary>
        private void SaveasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = Application.StartupPath;
            saveFile.Title = "Сохранить как";
            saveFile.DefaultExt = "md";
            saveFile.FileOk += new CancelEventHandler(SaveAs);
            saveFile.ShowDialog();
        }

        /// <summary>
        /// Записывает сохранение в файл
        /// </summary>
        private void SaveAs(object sender, CancelEventArgs e)
        {
            file = (sender as SaveFileDialog).FileName;
            File.WriteAllText(file, textBox1.Text);
        }

        /// <summary>
        /// Вставляет ссылку
        /// </summary>
        private void linkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Enabled = false;
            LinkInsert linkInsert = new LinkInsert(this);
            linkInsert.Show();
        }

        /// <summary>
        /// Вставляет изображение
        /// </summary>
        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Enabled = false;
            ImageInsert imageInsert = new ImageInsert(this);
            imageInsert.Show();
        }

        /// <summary>
        /// Меняет размеры текстового поля в связи с изменением размеров панели
        /// </summary>
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            textBox_Resize();
        }

        /// <summary>
        /// Вставка выделения полужирным
        /// </summary>
        private void strongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int start = textBox1.SelectionStart, end = textBox1.SelectionLength + textBox1.SelectionStart;
            selected = end;
            textBox1.Text = textBox1.Text.Insert(end, "**");
            selected = start;
            textBox1.Text = textBox1.Text.Insert(start, "**");
        }

        /// <summary>
        /// Вставка выделения курсивом
        /// </summary>
        private void italicToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int start = textBox1.SelectionStart, end = textBox1.SelectionLength + textBox1.SelectionStart;
            selected = end;
            textBox1.Text = textBox1.Text.Insert(end, "*");
            selected = start;
            textBox1.Text = textBox1.Text.Insert(start, "*");
        }

        /// <summary>
        /// Вставка выделения полужирным курсивом
        /// </summary>
        private void strongitalicToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int start = textBox1.SelectionStart, end = textBox1.SelectionLength + textBox1.SelectionStart;
            selected = end;
            textBox1.Text = textBox1.Text.Insert(end, "***");
            selected = start;
            textBox1.Text = textBox1.Text.Insert(start, "***");
        }

        /// <summary>
        /// Вставка выделения нижним подчеркиванием
        /// </summary>
        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int start = textBox1.SelectionStart, end = textBox1.SelectionLength + textBox1.SelectionStart;
            selected = end;
            textBox1.Text = textBox1.Text.Insert(end, "~");
            selected = start;
            textBox1.Text = textBox1.Text.Insert(start, "~");
        }

        /// <summary>
        /// Вставка выделения зачеркиванием
        /// </summary>
        private void strikeoutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int start = textBox1.SelectionStart, end = textBox1.SelectionLength + textBox1.SelectionStart;
            selected = end;
            textBox1.Text = textBox1.Text.Insert(end, "~~");
            selected = start;
            textBox1.Text = textBox1.Text.Insert(start, "~~");
        }

        /// <summary>
        /// Вставка выделения пзачеркиванием и подчеркиванием
        /// </summary>
        private void underlinestrikeoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int start = textBox1.SelectionStart, end = textBox1.SelectionLength + textBox1.SelectionStart;
            selected = end;
            textBox1.Text = textBox1.Text.Insert(end, "~~~");
            selected = start;
            textBox1.Text = textBox1.Text.Insert(start, "~~~");
        }

        /// <summary>
        /// Начинает экспорт в PDF
        /// </summary>
        private void ToPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.DefaultExt = "pdf";
            fileDialog.InitialDirectory = Application.StartupPath;
            fileDialog.Title = "Экспорт в PDF";
            fileDialog.ShowDialog();
            string s = fileDialog.FileName;
            PDF_Creator.Create(linesTexts, this, s);
        }

        /// <summary>
        /// Вставка взаголовка 1 уровня
        /// </summary>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string s = "# Заголовок1";
            if (textBox1.SelectionStart > 0 && textBox1.Text[textBox1.SelectionStart - 1] != '\n') s = "\r\n" + s;
            if (textBox1.SelectionStart < textBox1.Text.Length && textBox1.Text[textBox1.SelectionStart] != '\r') s += "\r\n";
            textBox1.SelectionStart = textBox1.SelectionStart + s.Length;
            selected = textBox1.SelectionStart + s.Length;
            textBox1.Text = textBox1.Text.Insert(textBox1.SelectionStart, s);
        }

        /// <summary>
        /// Вставка взаголовка 2 уровня
        /// </summary>
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            string s = "## Заголовок2";
            if (textBox1.SelectionStart > 0 && textBox1.Text[textBox1.SelectionStart - 1] != '\n') s = "\r\n" + s;
            if (textBox1.SelectionStart < textBox1.Text.Length && textBox1.Text[textBox1.SelectionStart] != '\r') s += "\r\n";
            selected = textBox1.SelectionStart + s.Length;
            textBox1.Text = textBox1.Text.Insert(textBox1.SelectionStart, s);
        }

        /// <summary>
        /// Вставка взаголовка 3 уровня
        /// </summary>
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            string s = "### Заголовок3";
            if (textBox1.SelectionStart > 0 && textBox1.Text[textBox1.SelectionStart - 1] != '\n') s = "\r\n" + s;
            if (textBox1.SelectionStart < textBox1.Text.Length && textBox1.Text[textBox1.SelectionStart] != '\r') s += "\r\n";
            selected = textBox1.SelectionStart + s.Length;
            textBox1.Text = textBox1.Text.Insert(textBox1.SelectionStart, s);
        }

        /// <summary>
        /// Вставка взаголовка 4 уровня
        /// </summary>
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            string s = "#### Заголовок4";
            if (textBox1.SelectionStart > 0 && textBox1.Text[textBox1.SelectionStart - 1] != '\n') s = "\r\n" + s;
            if (textBox1.SelectionStart < textBox1.Text.Length && textBox1.Text[textBox1.SelectionStart] != '\r') s += "\r\n";
            selected = textBox1.SelectionStart + s.Length;
            textBox1.Text = textBox1.Text.Insert(textBox1.SelectionStart, s);
        }

        /// <summary>
        /// Вставка взаголовка 5 уровня
        /// </summary>
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            string s = "##### Заголовок5";
            if (textBox1.SelectionStart > 0 && textBox1.Text[textBox1.SelectionStart - 1] != '\n') s = "\r\n" + s;
            if (textBox1.SelectionStart < textBox1.Text.Length && textBox1.Text[textBox1.SelectionStart] != '\r') s += "\r\n";
            selected = textBox1.SelectionStart + s.Length;
            textBox1.Text = textBox1.Text.Insert(textBox1.SelectionStart, s);
        }

        /// <summary>
        /// Вставка взаголовка 6 уровня
        /// </summary>
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            string s = "###### Заголовок6";
            if (textBox1.SelectionStart > 0 && textBox1.Text[textBox1.SelectionStart - 1] != '\n') s = "\r\n" + s;
            if (textBox1.SelectionStart < textBox1.Text.Length && textBox1.Text[textBox1.SelectionStart] != '\r') s += "\r\n";
            selected = textBox1.SelectionStart + s.Length;
            textBox1.Text = textBox1.Text.Insert(textBox1.SelectionStart, s);
        }

        /// <summary>
        /// Меняет значение chekbox в текстовом поле
        /// </summary>
        public void ChengeCheckBox(object sender, EventArgs e)
        {
            ChekLine.ChekLineControl checkbox = (sender as ChekLine.ChekLineControl);
            string[] s = textBox1.Lines;
            s[checkbox.startstring] = textBox1.Lines[checkbox.startstring].Remove(1, 1);
            s[checkbox.startstring] = s[checkbox.startstring].Insert(1, checkbox.checkBox.Checked ? "X" : " ");
            textBox1.Lines = s;
        }

        /// <summary>
        /// Экспортирует файл в HTML
        /// </summary>
        private void ToHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.DefaultExt = "html";
            fileDialog.InitialDirectory = Application.StartupPath;
            fileDialog.Title = "Экспорт в HTML";
            fileDialog.ShowDialog();
            string s = fileDialog.FileName;
            try
            {
                File.WriteAllText(s, CommonMark.CommonMarkConverter.Convert(textBox1.Text));
            }
            catch { }
        }
    }
}
