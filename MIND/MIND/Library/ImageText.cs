using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace MIND.Library
{
    class ImageText : InLineText
    {
        public ImageText(List<Formated> s)
        {
            int count = 2;
            for (int i = 2; i < s.Count; i++) if (s[i].s == ']') { count = i; break; };
            Image image = null;
            string link = "", context = "";
            List<Label> v = new List<Label>();
            bool isLink = true;
            for (int k = count + 2; k + 1 < s.Count; k++)
            {
                if (s[k].s == ' ' && s[k + 1].s == '\"') { isLink = false; k += 3; }
                if (isLink) link += s[k].s;
                else context += s[k - 1].s;
            }
            for (int i = 2; i < count; i++)
            {
                string current = "";
                for (int j = i; true; j++)
                {
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
                        v[v.Count - 1].Font = new Font(Form1.baseFamilyName, Form1.emSize, Format(s[i].isItalic, s[i].isBolt, s[i].isStricedOut, s[i].isUnderLine), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
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
                    context = "Извените, но я чайник, и я не могу загрузить ваше изображение";
                }
            }
            value = new ImageTextControl(v, image, context);
        }

        public class ImageTextControl : UserControl
        {
            public ImageTextControl(List<Label> value, Image image, string context)
            {
                PictureBox picture = new PictureBox();
                picture.Size = new Size(image.Width, image.Height);
                picture.Image = image;
                picture.ContextMenuStrip = new ContextMenuStrip();
                picture.ContextMenuStrip.Text = context;
                picture.MouseHover += new EventHandler(ImageMouseHover);
                picture.MouseLeave += new EventHandler(ImageMouseLeave);
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

    }
}
