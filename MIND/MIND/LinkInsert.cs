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
    /// <summary>
    /// Форма для вставки ссылки
    /// </summary>
    public partial class LinkInsert : Form
    {
        /// <summary>
        /// Родительская форма
        /// </summary>
        Form1 parent;

        /// <summary>
        /// Создание формы для вставки ссылки
        /// </summary>
        /// <param name="sender">родительская форма</param>
        public LinkInsert(Form1 sender)
        {
            parent = sender;
            InitializeComponent();
        }

        /// <summary>
        /// Выполняет вставку или предупреждает пользователя, что не все обязательные поля заполнены
        /// </summary>
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

        /// <summary>
        /// Закрывает форму
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Возвращает активность родительской форме
        /// </summary>
        private void LinkInsert_FormClosed(object sender, FormClosedEventArgs e)
        {
            parent.Enabled = true;
        }
    }
}
