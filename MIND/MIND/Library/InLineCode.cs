using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MIND.Library
{
    class InLineCode:InLineText
    {
        public InLineCode(List<Formated> s, float emSize)
        {
            Label v = new Label();
            string current = "";
            for (int i = 1; i < s.Count-1; i++)
            {
                current += s[i].s;
            }
            current = current.Replace((char)(65533), '~');
            current = current.Replace((char)(65534), '*');
            current = current.Replace((char)(65535), '_');
            v.Text = current;
            v.Font = new Font("Consolas", emSize, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            SizeF textSize = TextRenderer.MeasureText(v.Text, v.Font);
            int w = v.Padding.Horizontal + (int)textSize.Width;
            v.Size = new Size(w, (int)(emSize*2));
            v.BackColor = Color.Moccasin;
            v.ForeColor = Color.Black;
            value = v;
        }
    }
}
