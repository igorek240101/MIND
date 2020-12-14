using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MIND
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ссылкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cсылкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изображениеToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.изображениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.полужирныйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.курсивToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.полужирныйКурсивToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.подчеркиваниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.зачеркнутоToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.зачеркнутоПодчеркнутоеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заголовокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новыйФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортВHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортВPDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.видToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redactorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HtmlvisorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Black;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(933, 477);
            this.splitContainer1.SplitterDistance = 451;
            this.splitContainer1.TabIndex = 4;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // textBox1
            // 
            this.textBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(445, 36);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вставитьToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 26);
            // 
            // вставитьToolStripMenuItem
            // 
            this.вставитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ссылкуToolStripMenuItem,
            this.изображениеToolStripMenuItem,
            this.заголовокToolStripMenuItem});
            this.вставитьToolStripMenuItem.Name = "вставитьToolStripMenuItem";
            this.вставитьToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.вставитьToolStripMenuItem.Text = "Вставить";
            // 
            // ссылкуToolStripMenuItem
            // 
            this.ссылкуToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cсылкуToolStripMenuItem,
            this.изображениеToolStripMenuItem1});
            this.ссылкуToolStripMenuItem.Name = "ссылкуToolStripMenuItem";
            this.ссылкуToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.ссылкуToolStripMenuItem.Text = "Внешний источник";
            // 
            // cсылкуToolStripMenuItem
            // 
            this.cсылкуToolStripMenuItem.Name = "cсылкуToolStripMenuItem";
            this.cсылкуToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.cсылкуToolStripMenuItem.Text = "Cсылку";
            this.cсылкуToolStripMenuItem.Click += new System.EventHandler(this.linkToolStripMenuItem_Click);
            // 
            // изображениеToolStripMenuItem1
            // 
            this.изображениеToolStripMenuItem1.Name = "изображениеToolStripMenuItem1";
            this.изображениеToolStripMenuItem1.Size = new System.Drawing.Size(150, 22);
            this.изображениеToolStripMenuItem1.Text = "Изображение";
            this.изображениеToolStripMenuItem1.Click += new System.EventHandler(this.imageToolStripMenuItem_Click);
            // 
            // изображениеToolStripMenuItem
            // 
            this.изображениеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.полужирныйToolStripMenuItem,
            this.курсивToolStripMenuItem1,
            this.полужирныйКурсивToolStripMenuItem1,
            this.подчеркиваниеToolStripMenuItem,
            this.зачеркнутоToolStripMenuItem1,
            this.зачеркнутоПодчеркнутоеToolStripMenuItem});
            this.изображениеToolStripMenuItem.Name = "изображениеToolStripMenuItem";
            this.изображениеToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.изображениеToolStripMenuItem.Text = "Форматирование";
            // 
            // полужирныйToolStripMenuItem
            // 
            this.полужирныйToolStripMenuItem.Name = "полужирныйToolStripMenuItem";
            this.полужирныйToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.полужирныйToolStripMenuItem.Text = "Полужирный";
            this.полужирныйToolStripMenuItem.Click += new System.EventHandler(this.strongToolStripMenuItem_Click);
            // 
            // курсивToolStripMenuItem1
            // 
            this.курсивToolStripMenuItem1.Name = "курсивToolStripMenuItem1";
            this.курсивToolStripMenuItem1.Size = new System.Drawing.Size(218, 22);
            this.курсивToolStripMenuItem1.Text = "Курсив";
            this.курсивToolStripMenuItem1.Click += new System.EventHandler(this.italicToolStripMenuItem1_Click);
            // 
            // полужирныйКурсивToolStripMenuItem1
            // 
            this.полужирныйКурсивToolStripMenuItem1.Name = "полужирныйКурсивToolStripMenuItem1";
            this.полужирныйКурсивToolStripMenuItem1.Size = new System.Drawing.Size(218, 22);
            this.полужирныйКурсивToolStripMenuItem1.Text = "Полужирный курсив";
            this.полужирныйКурсивToolStripMenuItem1.Click += new System.EventHandler(this.strongitalicToolStripMenuItem1_Click);
            // 
            // подчеркиваниеToolStripMenuItem
            // 
            this.подчеркиваниеToolStripMenuItem.Name = "подчеркиваниеToolStripMenuItem";
            this.подчеркиваниеToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.подчеркиваниеToolStripMenuItem.Text = "Подчеркнуто";
            this.подчеркиваниеToolStripMenuItem.Click += new System.EventHandler(this.underlineToolStripMenuItem_Click);
            // 
            // зачеркнутоToolStripMenuItem1
            // 
            this.зачеркнутоToolStripMenuItem1.Name = "зачеркнутоToolStripMenuItem1";
            this.зачеркнутоToolStripMenuItem1.Size = new System.Drawing.Size(218, 22);
            this.зачеркнутоToolStripMenuItem1.Text = "Зачеркнуто";
            this.зачеркнутоToolStripMenuItem1.Click += new System.EventHandler(this.strikeoutToolStripMenuItem1_Click);
            // 
            // зачеркнутоПодчеркнутоеToolStripMenuItem
            // 
            this.зачеркнутоПодчеркнутоеToolStripMenuItem.Name = "зачеркнутоПодчеркнутоеToolStripMenuItem";
            this.зачеркнутоПодчеркнутоеToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.зачеркнутоПодчеркнутоеToolStripMenuItem.Text = "Зачеркнуто подчеркнутое";
            this.зачеркнутоПодчеркнутоеToolStripMenuItem.Click += new System.EventHandler(this.underlinestrikeoutToolStripMenuItem_Click);
            // 
            // заголовокToolStripMenuItem
            // 
            this.заголовокToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7});
            this.заголовокToolStripMenuItem.Name = "заголовокToolStripMenuItem";
            this.заголовокToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.заголовокToolStripMenuItem.Text = "Заголовок";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem2.Text = "1";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem3.Text = "2";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem4.Text = "3";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem5.Text = "4";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem6.Text = "5";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem7.Text = "6";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.видToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(933, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новыйФайлToolStripMenuItem,
            this.открытьToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.сохранитьКакToolStripMenuItem,
            this.экспортВHTMLToolStripMenuItem,
            this.экспортВPDFToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // новыйФайлToolStripMenuItem
            // 
            this.новыйФайлToolStripMenuItem.Name = "новыйФайлToolStripMenuItem";
            this.новыйФайлToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.новыйФайлToolStripMenuItem.Text = "Новый файл";
            this.новыйФайлToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить как";
            this.сохранитьКакToolStripMenuItem.Click += new System.EventHandler(this.SaveasToolStripMenuItem_Click);
            // 
            // экспортВHTMLToolStripMenuItem
            // 
            this.экспортВHTMLToolStripMenuItem.Name = "экспортВHTMLToolStripMenuItem";
            this.экспортВHTMLToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.экспортВHTMLToolStripMenuItem.Text = "Экспорт в HTML";
            this.экспортВHTMLToolStripMenuItem.Click += new System.EventHandler(this.ToHTMLToolStripMenuItem_Click);
            // 
            // экспортВPDFToolStripMenuItem
            // 
            this.экспортВPDFToolStripMenuItem.Name = "экспортВPDFToolStripMenuItem";
            this.экспортВPDFToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.экспортВPDFToolStripMenuItem.Text = "Экспорт в PDF";
            this.экспортВPDFToolStripMenuItem.Click += new System.EventHandler(this.ToPDFToolStripMenuItem_Click);
            // 
            // видToolStripMenuItem
            // 
            this.видToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.redactorToolStripMenuItem,
            this.visorToolStripMenuItem,
            this.HtmlvisorToolStripMenuItem});
            this.видToolStripMenuItem.Name = "видToolStripMenuItem";
            this.видToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.видToolStripMenuItem.Text = "Вид";
            // 
            // redactorToolStripMenuItem
            // 
            this.redactorToolStripMenuItem.Name = "redactorToolStripMenuItem";
            this.redactorToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.redactorToolStripMenuItem.Text = "Редактор";
            this.redactorToolStripMenuItem.Click += new System.EventHandler(this.redactorToolStripMenuItem_Click);
            // 
            // visorToolStripMenuItem
            // 
            this.visorToolStripMenuItem.Name = "visorToolStripMenuItem";
            this.visorToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.visorToolStripMenuItem.Text = "Превью";
            this.visorToolStripMenuItem.Click += new System.EventHandler(this.visorToolStripMenuItem_Click);
            // 
            // HtmlvisorToolStripMenuItem
            // 
            this.HtmlvisorToolStripMenuItem.Name = "HtmlvisorToolStripMenuItem";
            this.HtmlvisorToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.HtmlvisorToolStripMenuItem.Text = "Предпросмотр HTML";
            this.HtmlvisorToolStripMenuItem.Click += new System.EventHandler(this.HtmlvisorToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 501);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(933, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(250, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 523);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(203, 61);
            this.Name = "Form1";
            this.Text = "MIND";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новыйФайлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem экспортВHTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem экспортВPDFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem видToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redactorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HtmlvisorToolStripMenuItem;
        public TextBox textBox1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem вставитьToolStripMenuItem;
        private ToolStripMenuItem ссылкуToolStripMenuItem;
        private ToolStripMenuItem изображениеToolStripMenuItem;
        private StatusStrip statusStrip1;
        public ToolStripStatusLabel toolStripStatusLabel1;
        private SplitContainer splitContainer1;
        private ToolStripMenuItem cсылкуToolStripMenuItem;
        private ToolStripMenuItem изображениеToolStripMenuItem1;
        private ToolStripMenuItem полужирныйToolStripMenuItem;
        private ToolStripMenuItem курсивToolStripMenuItem1;
        private ToolStripMenuItem полужирныйКурсивToolStripMenuItem1;
        private ToolStripMenuItem подчеркиваниеToolStripMenuItem;
        private ToolStripMenuItem зачеркнутоToolStripMenuItem1;
        private ToolStripMenuItem зачеркнутоПодчеркнутоеToolStripMenuItem;
        private ToolStripMenuItem заголовокToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem toolStripMenuItem7;
        public  ToolStripProgressBar toolStripProgressBar1;
    }
}

