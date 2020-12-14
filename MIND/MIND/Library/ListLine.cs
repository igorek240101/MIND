using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MIND.Library
{
    class ListLine : LinesText
    {

        public int[] space;

        public ListLine(string s, int st) : base(st)
        {
            List<int> inside = new List<int>();
            inside.Add(0);
            string[] array = s.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            int count_of_space = array[0].Length - array[0].TrimStart(' ').Length;
            int[] mark = new int[array.Length];
            space = new int[array.Length];
            List<SimpleLines> simpleLines = new List<SimpleLines>();
            for (int i = 0; i < array.Length; i++)
            {
                if ((array[i].Length - array[i].TrimStart(' ').Length + 1) < count_of_space)
                {
                    int j = 1;
                    while ((array[i].Length - array[i].TrimStart(' ').Length + 1) < count_of_space)
                    { int bufer = inside[inside.Count - 1]; inside.RemoveAt(inside.Count - 1); count_of_space = space[i - j - bufer]; j += bufer; }
                    inside[inside.Count - 1]++;
                }
                else
                {
                    if ((array[i].Length - array[i].TrimStart(' ').Length - 1) > count_of_space)
                    {
                        inside.Add(1);
                    }
                    else { inside[inside.Count - 1]++; }
                }
                count_of_space = array[i].Length - array[i].TrimStart(' ').Length;
                array[i] = array[i].TrimStart(' ');
                if (array[i][0] != '-') { mark[i] = inside[inside.Count - 1]; array[i] = array[i].Substring(2, array[i].Length - 2); }
                else { mark[i] = 0; array[i] = array[i].Substring(1, array[i].Length - 1); }
                space[i] = inside.Count;
                simpleLines.Add(new SimpleLines(array[i], st));
            }

            value = new ListLineControl(simpleLines, mark, space);

        }

        public class ListLineControl : UserControl
        {
            public ListLineControl(List<SimpleLines> value, int[] mark, int[] space)
            {
                int y = 0, x = 0;
                for (int i = 0; i < mark.Length; i++)
                {
                    Label label = new Label();
                    if (mark[i] == 0)
                    {
                        label.Text = "•";
                    }
                    else
                    {
                        label.Text = Convert.ToString(mark[i] + ")");
                    }
                    label.Font = new Font(Form1.baseFamilyName, Form1.emSize);
                    Controls.Add(value[i].value);
                    Controls.Add(label);
                    value[i].value.Location = new Point(40 + (space[i] - 1) * 40, y);
                    label.Location = new Point((space[i] - 1) * 40, (y * 2 + value[i].value.Height) / 2 - label.Height / 2);
                    y += value[i].value.Height;
                    if (value[i].value.Width + (space[i] - 1) * 40 > x) x = value[i].value.Width + (space[i] - 1) * 40;
                }
                Size = new Size(x + 40, y);
            }
        }
    }
}
