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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новыйФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортВHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортВPDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.видToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редакторToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.превьюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.предпросмотрHtmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ссылкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изображениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.жирныйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.курсивToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.полужирныйкурсивToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.зачеркнутоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.подчерукнутоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.зачеркнутоПодчеркнутоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.новыйФайлToolStripMenuItem.Click += new System.EventHandler(this.новыйФайлToolStripMenuItem_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить как";
            this.сохранитьКакToolStripMenuItem.Click += new System.EventHandler(this.сохранитьКакToolStripMenuItem_Click);
            // 
            // экспортВHTMLToolStripMenuItem
            // 
            this.экспортВHTMLToolStripMenuItem.Name = "экспортВHTMLToolStripMenuItem";
            this.экспортВHTMLToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.экспортВHTMLToolStripMenuItem.Text = "Экспорт в HTML";
            // 
            // экспортВPDFToolStripMenuItem
            // 
            this.экспортВPDFToolStripMenuItem.Name = "экспортВPDFToolStripMenuItem";
            this.экспортВPDFToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.экспортВPDFToolStripMenuItem.Text = "Экспорт в PDF";
            // 
            // видToolStripMenuItem
            // 
            this.видToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.редакторToolStripMenuItem,
            this.превьюToolStripMenuItem,
            this.предпросмотрHtmlToolStripMenuItem});
            this.видToolStripMenuItem.Name = "видToolStripMenuItem";
            this.видToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.видToolStripMenuItem.Text = "Вид";
            // 
            // редакторToolStripMenuItem
            // 
            this.редакторToolStripMenuItem.Name = "редакторToolStripMenuItem";
            this.редакторToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.редакторToolStripMenuItem.Text = "Редактор";
            this.редакторToolStripMenuItem.Click += new System.EventHandler(this.редакторToolStripMenuItem_Click);
            // 
            // превьюToolStripMenuItem
            // 
            this.превьюToolStripMenuItem.Name = "превьюToolStripMenuItem";
            this.превьюToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.превьюToolStripMenuItem.Text = "Превью";
            this.превьюToolStripMenuItem.Click += new System.EventHandler(this.превьюToolStripMenuItem_Click);
            // 
            // предпросмотрHtmlToolStripMenuItem
            // 
            this.предпросмотрHtmlToolStripMenuItem.Name = "предпросмотрHtmlToolStripMenuItem";
            this.предпросмотрHtmlToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.предпросмотрHtmlToolStripMenuItem.Text = "Предпросмотр HTML";
            this.предпросмотрHtmlToolStripMenuItem.Click += new System.EventHandler(this.предпросмотрHtmlToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.textBox1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(194, 36);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вставитьToolStripMenuItem,
            this.изменитьToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 70);
            // 
            // вставитьToolStripMenuItem
            // 
            this.вставитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ссылкуToolStripMenuItem,
            this.изображениеToolStripMenuItem});
            this.вставитьToolStripMenuItem.Name = "вставитьToolStripMenuItem";
            this.вставитьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.вставитьToolStripMenuItem.Text = "Вставить";
            // 
            // ссылкуToolStripMenuItem
            // 
            this.ссылкуToolStripMenuItem.Name = "ссылкуToolStripMenuItem";
            this.ссылкуToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ссылкуToolStripMenuItem.Text = "Ссылку";
            this.ссылкуToolStripMenuItem.Click += new System.EventHandler(this.ссылкуToolStripMenuItem_Click);
            // 
            // изображениеToolStripMenuItem
            // 
            this.изображениеToolStripMenuItem.Name = "изображениеToolStripMenuItem";
            this.изображениеToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.изображениеToolStripMenuItem.Text = "Изображение";
            this.изображениеToolStripMenuItem.Click += new System.EventHandler(this.изображениеToolStripMenuItem_Click);
            // 
            // изменитьToolStripMenuItem
            // 
            this.изменитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.жирныйToolStripMenuItem,
            this.курсивToolStripMenuItem,
            this.полужирныйкурсивToolStripMenuItem,
            this.зачеркнутоToolStripMenuItem,
            this.подчерукнутоToolStripMenuItem,
            this.зачеркнутоПодчеркнутоToolStripMenuItem});
            this.изменитьToolStripMenuItem.Name = "изменитьToolStripMenuItem";
            this.изменитьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.изменитьToolStripMenuItem.Text = "Изменить";
            // 
            // жирныйToolStripMenuItem
            // 
            this.жирныйToolStripMenuItem.Name = "жирныйToolStripMenuItem";
            this.жирныйToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.жирныйToolStripMenuItem.Text = "Полужирный";
            // 
            // курсивToolStripMenuItem
            // 
            this.курсивToolStripMenuItem.Name = "курсивToolStripMenuItem";
            this.курсивToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.курсивToolStripMenuItem.Text = "Курсив";
            // 
            // полужирныйкурсивToolStripMenuItem
            // 
            this.полужирныйкурсивToolStripMenuItem.Name = "полужирныйкурсивToolStripMenuItem";
            this.полужирныйкурсивToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.полужирныйкурсивToolStripMenuItem.Text = "Полужирный+курсив";
            // 
            // зачеркнутоToolStripMenuItem
            // 
            this.зачеркнутоToolStripMenuItem.Name = "зачеркнутоToolStripMenuItem";
            this.зачеркнутоToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.зачеркнутоToolStripMenuItem.Text = "Зачеркнуто";
            // 
            // подчерукнутоToolStripMenuItem
            // 
            this.подчерукнутоToolStripMenuItem.Name = "подчерукнутоToolStripMenuItem";
            this.подчерукнутоToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.подчерукнутоToolStripMenuItem.Text = "Подчерукнуто";
            // 
            // зачеркнутоПодчеркнутоToolStripMenuItem
            // 
            this.зачеркнутоПодчеркнутоToolStripMenuItem.Name = "зачеркнутоПодчеркнутоToolStripMenuItem";
            this.зачеркнутоПодчеркнутоToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.зачеркнутоПодчеркнутоToolStripMenuItem.Text = "Зачеркнуто + подчеркнуто";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Location = new System.Drawing.Point(427, 116);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 501);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(933, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
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
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(203, 61);
            this.Name = "Form1";
            this.Text = "Form1";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem редакторToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem превьюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem предпросмотрHtmlToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        public TextBox textBox1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem вставитьToolStripMenuItem;
        private ToolStripMenuItem ссылкуToolStripMenuItem;
        private ToolStripMenuItem изображениеToolStripMenuItem;
        private ToolStripMenuItem изменитьToolStripMenuItem;
        private ToolStripMenuItem жирныйToolStripMenuItem;
        private ToolStripMenuItem курсивToolStripMenuItem;
        private ToolStripMenuItem полужирныйкурсивToolStripMenuItem;
        private ToolStripMenuItem зачеркнутоToolStripMenuItem;
        private ToolStripMenuItem подчерукнутоToolStripMenuItem;
        private ToolStripMenuItem зачеркнутоПодчеркнутоToolStripMenuItem;
        private StatusStrip statusStrip1;
        public ToolStripStatusLabel toolStripStatusLabel1;
    }
}

