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
    public partial class ImageInsert : Form
    {
        Form1 parent;
        public ImageInsert(Form1 sender)
        {
            parent = sender;
            InitializeComponent();
            button3.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resourse\\Folder.jpg");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                if (textBox3.Text != "") parent.textBox1.Text = parent.textBox1.Text.Insert(parent.textBox1.SelectionStart, "![" + textBox1.Text + "](" + textBox2.Text + " \"" + textBox3.Text + "\")");
                else parent.textBox1.Text = parent.textBox1.Text.Insert(parent.textBox1.SelectionStart, "![" + textBox1.Text + "](" + textBox2.Text + ")");
                Close();
            }
            else MessageBox.Show("Заполните поле \"Адресс изображения\".", "Адресс изображения не заполнен", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ImageInsert_FormClosed(object sender, FormClosedEventArgs e)
        {
            parent.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = Application.StartupPath;
            fileDialog.Title = "Выбрать изображение";
            fileDialog.CheckPathExists = true;
            fileDialog.CheckFileExists = true;
            fileDialog.ShowDialog();
            try
            {
                textBox2.Text = fileDialog.FileName;
            }
            catch { }
        }
    }
}
