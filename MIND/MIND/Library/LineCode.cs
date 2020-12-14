using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace MIND.Library
{
    class LineCode : LinesText
    {
        public LineCode(string s, int st) : base(st)
        {
            int y = 0 , maxx = 0;
            List<InLineCode> inLineCodes = new List<InLineCode>();
            s = s.Substring(5, s.Length - 10);
            string[] array = s.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            List<Formated>[] formateds = new List<Formated>[array.Length];
            for (int i = 0; i < formateds.Length; i++)
            {
                formateds[i] = new List<Formated>();
                for (int j = 0; j < array[i].Length; j++) formateds[i].Add(new Formated(array[i][j]));
                formateds[i].Insert(0, new Formated('`'));
                formateds[i].Add(new Formated('`'));
            }
            for (int i = 0; i < array.Length; i++)
            {
                inLineCodes.Add(new InLineCode(formateds[i], Form1.emSize));
                inLineCodes[inLineCodes.Count - 1].startString = y;
                inLineCodes[inLineCodes.Count - 1].startX = 50;
                y += (int)(Form1.emSize * 2);
                if (inLineCodes[inLineCodes.Count - 1].value.Width > maxx) maxx = inLineCodes[inLineCodes.Count - 1].value.Width;
            }
            value = new LineCodeControl(inLineCodes,maxx, y);
        }


        public class LineCodeControl : UserControl
        {
            public LineCodeControl(List<InLineCode> value,int x, int y)
            {
                BackColor = Color.Moccasin;
                BorderStyle = BorderStyle.Fixed3D;
                Size = new Size(x+50, y);
                for (int i = 0; i < value.Count; i++)
                {
                    Controls.Add(value[i].value);
                    Controls[Controls.Count - 1].Location = new Point(value[i].startX, value[i].startString);
                    Label label = new Label();
                    label.Text = i<999? Convert.ToString(i + 1): "###";
                    label.Font = new Font(Form1.baseFamilyName, Form1.emSize, FontStyle.Regular, GraphicsUnit.Point);
                    label.Width = 40;
                    Controls.Add(label);
                    Controls[Controls.Count - 1].Location = new Point(5, value[i].startString);
                }
                Paint += new PaintEventHandler(Paint_C);
            }

            private void Paint_C(object sender, PaintEventArgs e)
            {
                Graphics g = CreateGraphics();
                g.DrawLine(new Pen(Color.Black, 2), 48, 0, 48, Height);
            }
        }

    }
}
