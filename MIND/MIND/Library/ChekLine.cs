using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MIND.Library
{
    class ChekLine : LinesText
    {
        public ChekLine(string s, int st) : base(st)
        {
            bool tr = s[1] == 'X';
            s = s.Substring(4, s.Length - 4);

            int x = 0, count_of_code = 0;
            List<InLineText> inLines = new List<InLineText>();
            List<Formated> formateds = new List<Formated>();
            List<List<Formated>> codes = new List<List<Formated>>();
            List<Formated> codes_formated = new List<Formated>();
            for (int j = 0; j < s.Length; j++) codes_formated.Add(new Formated(s[j]));
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
                            if (codes_formated[k].s == '*' || codes_formated[k].s == '~' || codes_formated[k].s == '_') s = s.Remove(k, 1);
                        }
                        j += end;
                    }
                }
            }
            for (int i = 0; i < s.Length; i++) { formateds = InLineText.SearchFormat(s); }
            if (formateds.Count > 1)
            {
                int[] matrix = new int[formateds.Count];
                matrix[0] = 4; matrix[matrix.Length - 1] = -4;
                for (int j = 0; j < formateds.Count - 1; j++)
                {
                    if (formateds[j].s == '`')
                    {
                        int last;
                        if (isCode(formateds.GetRange(j + 1, formateds.Count - (j + 1)), out last))
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
                        if (formateds[j].s == '!')
                        {
                            int last;
                            if (isImage(formateds.GetRange(j + 1, formateds.Count - (j + 1)), out last))
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
                            if (formateds[j].s == '[')
                            {
                                int last;
                                if (isLink(formateds.GetRange(j + 1, formateds.Count - (j + 1)), out last))
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
                                inLines.Add(new Link(formateds.GetRange(j, k - j + 1), Form1.emSize, FontStyle.Regular));
                                inLines[inLines.Count - 1].startString = 0;
                                inLines[inLines.Count - 1].startX = x;
                                x += inLines[inLines.Count - 1].value.Width;
                                if (inLines[inLines.Count - 1].value.Height > Form1.emSize * 2)
                                {
                                    x = 0;
                                }
                                break;
                            }
                        case 2:
                            {
                                inLines.Add(new ImageText(formateds.GetRange(j, k - j + 1), null, Form1.emSize, FontStyle.Regular));
                                x = 0;
                                inLines[inLines.Count - 1].startString = 0;
                                inLines[inLines.Count - 1].startX = x;
                                break;
                            }
                        case 3:
                            {
                                inLines.Add(new InLineCode(codes[count_of_code], Form1.emSize, FontStyle.Regular));
                                count_of_code++;
                                inLines[inLines.Count - 1].startString = 0;
                                inLines[inLines.Count - 1].startX = x;
                                x += inLines[inLines.Count - 1].value.Width;
                                count_of_code++;
                                break;
                            }
                        case 4:
                            {
                                inLines.Add(new SimpleInLineText(formateds.GetRange(j, k - j + 1), Form1.emSize, FontStyle.Regular));
                                inLines[inLines.Count - 1].startString = 0;
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
                inLines.Add(new SimpleInLineText(formateds, Form1.emSize, FontStyle.Regular));
                inLines[inLines.Count - 1].startString = 0;
                inLines[inLines.Count - 1].startX = x;
                x += inLines[inLines.Count - 1].value.Width;
            }


            value = new ChekLineControl(inLines, tr, x);
        }

        public class ChekLineControl : UserControl
        {
            public ChekLineControl(List<InLineText> value, bool tr, int x)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Checked = tr;
                checkBox.Enabled = false;
                checkBox.Size = new Size(30, 30);
                checkBox.CheckAlign = ContentAlignment.TopCenter;
                Controls.Add(checkBox);
                Controls[Controls.Count - 1].Location = new Point(0, 0);
                Size = new Size(x + 30, (int)Form1.emSize*2);
                for (int i = 0; i < value.Count; i++)
                {
                    Controls.Add(value[i].value);
                    Controls[Controls.Count - 1].Location = new Point(value[i].startX+30, value[i].startString);
                }
            }
        }
    }
}
