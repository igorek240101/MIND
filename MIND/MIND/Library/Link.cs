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

        public Link(List<Formated> s)
        {
            int count = 1;
            for (int i = s.Count-1; i >= 0; i--) if (s[i].s == ']') { count = i; break;};
            List<LinkLabel> v = new List<LinkLabel>();
            bool isLink = false;
            string link = "", context = "";
            for (int k = s.Count-2; k >= count + 2; k--)
            {
                if (s[k-1].s == ' ' && s[k].s == '\"') { isLink = true; k -= 3; }
                if (isLink) link += s[k].s;
                else context += s[k].s;
            }
            List<ImageText> imageTexts = new List<ImageText>();
            int st = 1;
            while (st < s.Count)
            {
                int start, end;
                LinesText.SearchImage(s.GetRange(st, count - st), out start, out end);
                if (start == -1) break;
                st += end + 1;
            }
            for (int i = 1; i < count; i++)
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
                        v.Add(new LinkLabel());
                        v[v.Count - 1].AutoSize = true;
                        try { v[v.Count - 1].Links.Add(0, v[v.Count - 1].Text.Length, link); } catch { }
                        v[v.Count - 1].ContextMenuStrip = new ContextMenuStrip();
                        try { v[v.Count - 1].ContextMenuStrip.Text = context; } catch { v[v.Count - 1].ContextMenuStrip.Text = ""; }
                        v[v.Count - 1].LinkClicked += new LinkLabelLinkClickedEventHandler(LinkClicked);
                        v[v.Count - 1].MouseHover += new EventHandler(LinkMouseHover);
                        v[v.Count - 1].MouseLeave += new EventHandler(LinkMouseLeave);
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
            value = new LinkControl(v);
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
           System.Diagnostics.Process.Start((sender as LinkLabel).Links[0].LinkData.ToString());
        }


        public class LinkControl : UserControl
        {
            public LinkControl(List<LinkLabel> v)
            {
                int loc = 0, sized = 0;
                int w = 0;
                for (int i = 0; i < v.Count; i++)
                {
                    Graphics g = Graphics.FromHwnd(v[i].Handle);
                    SizeF s = g.MeasureString(v[i].Text, Font);
                    w += v[i].Padding.Horizontal + (int)(s.Width*2);
                    Controls.Add(v[i]);
                    Controls[Controls.Count - 1].Location = new Point(sized + loc, 0);
                    loc = loc + sized;
                    sized = Controls[Controls.Count - 1].Width;
                }
                Size = new Size((int)(w * 0.8), 22);
            }
        }
    } 
}
