using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MIND.Library
{

    class Link : InLineText
    {

        public Link(List<Formated> s, float emSize, FontStyle style)
        {
            int count = 1;
            for (int i = s.Count-1; i >= 0; i--) if (s[i].s == ']') { count = i; break;};
            List<Control> v = new List<Control>();
            bool isLink = false;
            string link = "", context = "";
            for (int k = s.Count-2; k >= count + 2; k--)
            {
                if (s[k-1].s == ' ' && s[k].s == '\"') { isLink = true; k -= 2; }
                if (isLink) link = s[k].s + link;
                else context = s[k].s + context;
            }
            if (!isLink) { link = context; context = ""; }
            else try { context = context.Remove(context.Length - 1, 1); } catch { }
            List<ImageText> imageTexts = new List<ImageText>();
            List<int> q = new List<int>();
            for (int i = 1; i < s.Count; i++)
            {
                int end;
                if (s[i].s == '!')
                {
                    if (LinesText.isImage(s.GetRange(i + 1, count - i), out end))
                    {
                        imageTexts.Add(new ImageText(s.GetRange(i, end + 2), link, Form1.emSize, style));
                        q.Add(i);
                        s.RemoveRange(i, end + 2);
                        count -= end + 2;
                        i--;
                    }
                }
            }
            int st = 0;
            for (int i = 1; i < count; i++)
            {
                string current = "";
                for (int j = i; true; j++)
                {
                    bool tr = false;
                    while (q.Count > st && j == q[st])
                    {
                        if(current != "")
                        {
                            tr = true;
                            v.Add(new LinkLabel());
                            v[v.Count - 1].AutoSize = true;
                            try { (v[v.Count - 1] as LinkLabel).Links.Add(0, v[v.Count - 1].Text.Length, link); } catch { }
                            v[v.Count - 1].ContextMenuStrip = new ContextMenuStrip();
                            try { v[v.Count - 1].ContextMenuStrip.Text = context; } catch { v[v.Count - 1].ContextMenuStrip.Text = ""; }
                            (v[v.Count - 1] as LinkLabel).LinkClicked += new LinkLabelLinkClickedEventHandler(LinkClicked);
                            v[v.Count - 1].MouseHover += new EventHandler(LinkMouseHover);
                            v[v.Count - 1].MouseLeave += new EventHandler(LinkMouseLeave);
                            current = current.Replace((char)(65534), '*');
                            current = current.Replace((char)(65533), '~');
                            current = current.Replace((char)(65535), '_');
                            v[v.Count - 1].Text = current;
                            current = "";
                            v[v.Count - 1].Font = new Font(Form1.baseFamilyName, emSize, style | Format(s[i].isItalic, s[i].isBolt, s[i].isStricedOut, s[i].isUnderLine), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                            i = j - 1;
                        }
                        v.Add(imageTexts[st].value);
                        st++;
                    }
                    if (tr) break;
                    if (j < count && s[i].isBolt == s[j].isBolt && s[i].isItalic == s[j].isItalic && s[i].isStricedOut == s[j].isStricedOut && s[i].isUnderLine == s[j].isUnderLine)
                    {
                        current += s[j].s;
                    }
                    else
                    {
                        v.Add(new LinkLabel());
                        v[v.Count - 1].AutoSize = true;
                        try { (v[v.Count - 1] as LinkLabel).Links.Add(0, v[v.Count - 1].Text.Length, link); } catch { }
                        v[v.Count - 1].ContextMenuStrip = new ContextMenuStrip();
                        try { v[v.Count - 1].ContextMenuStrip.Text = context; } catch { v[v.Count - 1].ContextMenuStrip.Text = ""; }
                        (v[v.Count - 1] as LinkLabel).LinkClicked += new LinkLabelLinkClickedEventHandler(LinkClicked);
                        v[v.Count - 1].MouseHover += new EventHandler(LinkMouseHover);
                        v[v.Count - 1].MouseLeave += new EventHandler(LinkMouseLeave);
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
            for (; st < imageTexts.Count; st++) v.Add(imageTexts[st].value);
            value = new LinkControl(v, (int)(emSize));
        }

        private void LinkMouseHover(object sender, EventArgs e)
        {
            try { Form1.main.toolStripStatusLabel1.Text = (sender as LinkLabel).ContextMenuStrip.Text; } catch { }
        }

        private void LinkMouseLeave(object sender, EventArgs e)
        {
            Form1.main.toolStripStatusLabel1.Text = "";
        }

        private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start((sender as LinkLabel).Links[0].LinkData.ToString());
            }
            catch
            {
                MessageBox.Show("Не удается перейти по ссылке", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public class LinkControl : UserControl
        {
            public LinkControl(List<Control> v, int emsized)
            {
                int y = 0;
                int loc = 0, sized = 0;
                int w = 0, max = 0;
                for (int i = 0; i < v.Count; i++)
                {
                    if (v[i].GetType() == typeof(LinkLabel))
                    {
                        Graphics g = Graphics.FromHwnd(v[i].Handle);
                        SizeF s = g.MeasureString(v[i].Text, Font);
                        w += v[i].Padding.Horizontal + (int)(s.Width * 2);
                        Controls.Add(v[i]);
                        Controls[Controls.Count - 1].Location = new Point(sized + loc, y);
                        loc = loc + sized;
                        sized = Controls[Controls.Count - 1].Width;
                        if (w > max) max = w;
                    }
                    else
                    {
                        y += emsized*2;
                        loc = 0;
                        sized = 0;
                        v[i].Cursor = Cursors.Hand;
                        Controls.Add(v[i]);
                        Controls[Controls.Count - 1].Location = new Point(0, y);
                        y += emsized*2 + Controls[Controls.Count - 1].Height;
                        if (Controls[Controls.Count - 1].Width > max) max = Controls[Controls.Count - 1].Width;
                        w = 0;
                    }
                }
                Size = new Size((int)(max), y + emsized * 2);
            }
        }
    } 
}
