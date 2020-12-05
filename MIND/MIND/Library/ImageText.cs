using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace MIND.Library
{
    /// <summary>
    /// Класс Markdown-изображений
    /// </summary>
    class ImageText : InLineText
    {
        public ImageText(List<Formated> s, string l, float emSize, FontStyle style)
        {
            int count = 2;
            for (int i = s.Count-1; i >= 2; i--) if (s[i].s == ']') { count = i; break; };
            Image image = null;
            string link = "", context = "";
            List<Control> v = new List<Control>();
            bool isLink = true;
            for (int k = s.Count - 2; k >= count + 2; k--)
            {
                if (s[k - 1].s == ' ' && s[k].s == '\"') { isLink = true; k -= 3; }
                if (isLink) link = s[k].s + link;
                else context = s[k].s + context;
            }
            if (!isLink) link = context; context = "";


            List<Link> links = new List<Link>();
            List<int> q = new List<int>();
            for(int i = 2; i < s.Count; i++)
            {
                int end;
                if(s[i].s == '[')
                {
                    if(LinesText.isLink(s.GetRange(i+1, count - i), out end))
                    {
                        links.Add(new Link(s.GetRange(i, end+2), emSize, FontStyle.Regular));
                        q.Add(i);
                        s.RemoveRange(i, end+2);
                        count -= end + 2;
                        i--;
                    }
                }
            }
            int st = 0;
            for (int i = 2; i < count; i++)
            {
                string current = "";
                for (int j = i; true; j++)
                {
                    bool tr = false;
                    while (q.Count > st && j == q[st])
                    {
                        if (current != "")
                        {
                            tr = true;
                            v.Add(new Label());
                            v[v.Count - 1].AutoSize = true;
                            current = current.Replace((char)(65534), '*');
                            current = current.Replace((char)(65533), '~');
                            current = current.Replace((char)(65535), '_');
                            v[v.Count - 1].Text = current;
                            current = "";
                            v[v.Count - 1].Font = new Font(Form1.baseFamilyName, emSize, style | Format(s[i].isItalic, s[i].isBolt, s[i].isStricedOut, s[i].isUnderLine), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                            i = j - 1;
                        }
                        v.Add(links[st].value);
                        st++;
                    }
                    if (tr) break;
                    if (j < count && s[i].isBolt == s[j].isBolt && s[i].isItalic == s[j].isItalic && s[i].isStricedOut == s[j].isStricedOut && s[i].isUnderLine == s[j].isUnderLine)
                    {
                        current += s[j].s;
                    }
                    else
                    {
                        v.Add(new Label());
                        v[v.Count - 1].AutoSize = true;
                        current = current.Replace((char)(65534), '*');
                        current = current.Replace((char)(65533), '~');
                        current = current.Replace((char)(65535), '_');
                        v[v.Count - 1].Text = current;
                        current = "";
                        v[v.Count - 1].Font = new Font(Form1.baseFamilyName, emSize, style | Format(s[i].isItalic, s[i].isBolt, s[i].isStricedOut, s[i].isUnderLine), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        i = j - 1;
                        break;
                    }
                }
            }
            try
            {
                image = Image.FromFile(link);
            }
            catch
            {
                try
                {
                    WebClient webClient = new WebClient();
                    byte[] data = webClient.DownloadData(link);
                    MemoryStream mem = new MemoryStream(data);
                    image = Image.FromStream(mem);
                }
                catch
                {
                    image = Image.FromFile(Application.StartupPath + "\\Resourse\\defaultf.jpg");
                    context = "Извините, но я чайник, и я не могу загрузить ваше изображение:(";
                }
            }
            for (; st < links.Count; st++) v.Add(links[st].value);
            value = new ImageTextControl(v, image, context, l);
        }
        /// <summary>
        /// Контроллер Markdown-изображения
        /// </summary>
        public class ImageTextControl : UserControl
        {
            public ImageTextControl(List<Control> value, Image image, string context, string Link)
            {
                PictureBox picture = new PictureBox();
                picture.Size = new Size(image.Width, image.Height);
                picture.Image = image;
                picture.ContextMenuStrip = new ContextMenuStrip();
                picture.ContextMenuStrip.Text = context;
                picture.MouseHover += new EventHandler(ImageMouseHover);
                picture.MouseLeave += new EventHandler(ImageMouseLeave);
                picture.MouseClick += new MouseEventHandler(ImageClicked);
                if (Link != null) picture.Name = Link;
                Controls.Add(picture);
                picture.Location = new Point(0, 0);
                int loc = 0, sized = 0, w = 0;
                for (int i = 0; i < value.Count; i++)
                {
                    w += (int)(value[i].Text.Length * value[i].Font.Size);
                    Controls.Add(value[i]);
                    Controls[Controls.Count - 1].Location = new Point(sized + loc, picture.Height);
                    loc = loc + sized;
                    sized = Controls[Controls.Count - 1].Width;
                }
                Size = new Size(w>picture.Width?w:picture.Width, 22 + picture.Height);
            }
        }

        private static void ImageMouseHover(object sender, EventArgs e)
        {
            try { Form1.main.toolStripStatusLabel1.Text = (sender as PictureBox).ContextMenuStrip.Text; } catch { }
        }

        private static void ImageMouseLeave(object sender, EventArgs e)
        {
            Form1.main.toolStripStatusLabel1.Text = "";
        }

        private static void ImageClicked(object sender, MouseEventArgs e)
        {
            try
            {
                string s = (sender as PictureBox).Name;
                if (s != "") System.Diagnostics.Process.Start(s);
            }
            catch { MessageBox.Show("Не удается перейти по ссылке", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

    }
}
