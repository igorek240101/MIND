using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MIND.Library
{
    class QuotationLines : LinesText
    {

        public QuotationLines(string s, int st) : base(st)
        {
            s = s.Substring(1, s.Length - 1);
            s = s.Replace("\r\n>", "\r\n");
            int x = 20, y = 5, maxx = 0, count_of_code = 0;
            List<InLineText> inLines = new List<InLineText>();
            string[] array = s.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            List<Formated>[] formateds = new List<Formated>[array.Length];
            List<List<Formated>> codes = new List<List<Formated>>();
            for (int i = 0; i < formateds.Length; i++)
            {
                List<Formated> codes_formated = new List<Formated>();
                for (int j = 0; j < array[i].Length; j++) codes_formated.Add(new Formated(array[i][j]));
                for (int j = 0; j < codes_formated.Count; j++)
                {
                    if (codes_formated.Count - j > 2 && codes_formated[j].s == '`')
                    {
                        int end;
                        if (isCode(codes_formated.GetRange(j + 1, codes_formated.Count - (j + 1)), out end))
                        {
                            codes.Add(new List<Formated>());
                            for (int k = end + j + 1; k >= j; k--)
                            {
                                codes[codes.Count - 1].Insert(0, new Formated(codes_formated[k].s));
                                if (codes_formated[k].s == '*' || codes_formated[k].s == '~' || codes_formated[k].s == '_') array[i] = array[i].Remove(k, 1);
                            }
                            j += end;
                        }
                    }
                }
            }
            for (int i = 0; i < array.Length; i++) { formateds[i] = InLineText.SearchFormat(array[i]); }
            for (int i = 0; i < array.Length; i++)
            {
                if (formateds[i].Count > 1)
                {
                    int[] matrix = new int[formateds[i].Count];
                    matrix[0] = 4; matrix[matrix.Length - 1] = -4;
                    for (int j = 0; j < formateds[i].Count - 1; j++)
                    {
                        if (formateds[i][j].s == '`')
                        {
                            int last;
                            if (isCode(formateds[i].GetRange(j + 1, formateds[i].Count - (j + 1)), out last))
                            {
                                matrix[j] = 3;
                                matrix[last + j + 1] = -3;
                                if (j > 0 && matrix[j - 1] == 0) matrix[j - 1] = -4;
                                if (last + j + 2 < matrix.Length && matrix[last + j + 2] == 0) matrix[last + j + 2] = 4;
                                j = last + j + 1;
                                continue;
                            }
                        }
                        else
                        {
                            if (formateds[i][j].s == '!')
                            {
                                int last;
                                if (isImage(formateds[i].GetRange(j + 1, formateds[i].Count - (j + 1)), out last))
                                {
                                    matrix[j] = 2;
                                    matrix[last + j + 1] = -2;
                                    if (j > 0 && matrix[j - 1] == 0) matrix[j - 1] = -4;
                                    if (last + j + 2 < matrix.Length && matrix[last + j + 2] == 0) matrix[last + j + 2] = 4;
                                    j = last + j + 1;
                                    continue;
                                }
                            }
                            else
                            {
                                if (formateds[i][j].s == '[')
                                {
                                    int last;
                                    if (isLink(formateds[i].GetRange(j + 1, formateds[i].Count - (j + 1)), out last))
                                    {
                                        matrix[j] = 1;
                                        matrix[last + j + 1] = -1;
                                        if (j > 0 && matrix[j - 1] == 0) matrix[j - 1] = -4;
                                        if (last + j + 2 < matrix.Length && matrix[last + j + 2] == 0) matrix[last + j + 2] = 4;
                                        j = last + j + 1;
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                    for (int j = 0; j < matrix.Length; j++)
                    {
                        int current = matrix[j];
                        int k = j;
                        if (j + 1 != matrix.Length && (matrix[j + 1] == -current || matrix[j + 1] == 0))
                        {
                            while (matrix[k++] != -current) ;
                            k--;
                        }
                        switch (Math.Abs(current))
                        {
                            case 1:
                                {
                                    inLines.Add(new Link(formateds[i].GetRange(j, k - j + 1), Form1.emSize, FontStyle.Italic));
                                    inLines[inLines.Count - 1].startString = y;
                                    inLines[inLines.Count - 1].startX = x;
                                    x += inLines[inLines.Count - 1].value.Width;
                                    if (inLines[inLines.Count - 1].value.Height >   (int)(Form1.emSize * 2))
                                    {
                                        y += inLines[inLines.Count - 1].value.Height + (int)(Form1.emSize * 2);
                                        if (maxx < x) maxx = x;
                                        x = 20;
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    inLines.Add(new ImageText(formateds[i].GetRange(j, k - j + 1), null, Form1.emSize, FontStyle.Italic));
                                    y += (int)(Form1.emSize * 2); x = 20;
                                    inLines[inLines.Count - 1].startString = y;
                                    inLines[inLines.Count - 1].startX = x;
                                    if (maxx < inLines[inLines.Count - 1].value.Width) maxx = inLines[inLines.Count - 1].value.Width;
                                    y += inLines[inLines.Count - 1].value.Height;
                                    break;
                                }
                            case 3:
                                {
                                    inLines.Add(new InLineCode(codes[count_of_code], Form1.emSize, FontStyle.Italic));
                                    count_of_code++;
                                    inLines[inLines.Count - 1].startString = y;
                                    inLines[inLines.Count - 1].startX = x;
                                    x += inLines[inLines.Count - 1].value.Width;
                                    count_of_code++;
                                    break;
                                }
                            case 4:
                                {
                                    inLines.Add(new SimpleInLineText(formateds[i].GetRange(j, k - j + 1), Form1.emSize, FontStyle.Italic));
                                    inLines[inLines.Count - 1].startString = y;
                                    inLines[inLines.Count - 1].startX = x;
                                    x += inLines[inLines.Count - 1].value.Width;
                                    break;
                                }
                        }
                        j = k;

                    }
                }
                else
                {
                    inLines.Add(new SimpleInLineText(formateds[i], Form1.emSize, FontStyle.Italic));
                    inLines[inLines.Count - 1].startString = y;
                    inLines[inLines.Count - 1].startX = x;
                    x += inLines[inLines.Count - 1].value.Width;
                }
                y += (int)(Form1.emSize*2);
                if (maxx < x) maxx = x;
                x = 20;
            }

            value = new QuotationLinesControl(inLines, maxx, y);
        }


        public class QuotationLinesControl : UserControl
        {
            public QuotationLinesControl(List<InLineText> value, int x, int y)
            {
                Size = new Size(x, y);
                for (int i = 0; i < value.Count; i++)
                {
                    Controls.Add(value[i].value);
                    Controls[Controls.Count - 1].Location = new Point(value[i].startX, value[i].startString);
                }
                Paint += new PaintEventHandler(Paint_Q);
                Update();
            }


            private void Paint_Q(object sender, PaintEventArgs e)
            {
                Graphics g = CreateGraphics();
                g.DrawLine(new Pen(Color.Gray, 4), 12, 0, 12, Height);
            }


        }



    }
}
