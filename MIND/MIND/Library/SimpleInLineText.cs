using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MIND.Library
{
    class SimpleInLineText:InLineText
    {

        public SimpleInLineText(List<Formated> s)
        {
            List<Label> v = new List<Label>();
            for (int i = 0; i < s.Count; i++)
            {
                string current = "";
                for(int j = i; true; j++)
                {
                    if(j < s.Count && s[i].isBolt == s[j].isBolt && s[i].isItalic == s[j].isItalic && s[i].isStricedOut == s[j].isStricedOut && s[i].isUnderLine == s[j].isUnderLine)
                    {
                        current += s[j].s;
                    }
                    else
                    {
                        v.Add(new Label());
                        v[v.Count - 1].AutoSize = true;
                        current = current.Replace((char)(65533), '~');
                        current = current.Replace((char)(65534), '*');
                        current = current.Replace((char)(65535), '_');
                        v[v.Count - 1].Text = current;
                        current = "";
                        v[v.Count - 1].Font = new Font(Form1.baseFamilyName, Form1.emSize, Format(s[i].isItalic, s[i].isBolt, s[i].isStricedOut, s[i].isUnderLine), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        i = j-1;
                        break;
                    }    
                }
            }
            value = new SimpleInLineTextControl(v);

        }



        public class SimpleInLineTextControl : UserControl
        {
            public SimpleInLineTextControl(List<Label> v)
            {
                int loc = 0, sized = 0;
                int w = 0;
                for (int i = 0; i < v.Count; i++)
                {
                    w += (int)(v[i].Text.Length*v[i].Font.Size);
                    Controls.Add(v[i]);
                    Controls[Controls.Count - 1].Location = new Point(sized + loc, 0);
                    loc = loc + sized;
                    sized = Controls[Controls.Count - 1].Width;
                }
                Size = new Size(w, 22);
            }
        }


    }
}
