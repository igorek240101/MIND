using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MIND.Library
{
    class SimpleLines : LinesText
    {

        public SimpleLinesControl value;

        public SimpleLines(string s)
        {
            int x = 0, y = 0, maxx = 0;
            List<InLineText> inLines = new List<InLineText>();
            s = ToFormatLine(s);
            string[] array = s.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            List<Formated>[] formateds = new List<Formated>[array.Length];
            for (int i = 0; i < array.Length; i++) { formateds[i] = InLineText.SearchFormat(array[i]); }
            for (int i = 0; i < array.Length; i++)
            {
                if (formateds[i].Count > 1)
                {
                    int[] matrix = new int[formateds[i].Count];
                    matrix[0] = 4; matrix[matrix.Length - 1] = -4;
                    int st = 0;
                    while (st < formateds[i].Count)
                    {
                        int start, end;
                        SearchLink(formateds[i].GetRange(st, formateds[i].Count - st), out start, out end);
                        if (start == -1) break;
                        st = end + 1;

                        matrix[start] = 1;
                        matrix[end] = -1;
                        if (start > 0 && matrix[start - 1] == 0) matrix[start - 1] = -4;
                        if (end + 1 < matrix.Length && matrix[end + 1] == 0) matrix[end + 1] = 4;
                    }
                    st = 0;
                    while (st < formateds[i].Count)
                    {
                        int start, end;
                        SearchImage(formateds[i].GetRange(st, formateds[i].Count - st), out start, out end);
                        if (start == -1) break;
                        st = end + 1;
                        matrix[start] = 2;
                        matrix[end] = -2;
                        if (start > 0 && matrix[start - 1] == 0) matrix[start - 1] = -4;
                        if (end + 1 < matrix.Length && matrix[end + 1] == 0) matrix[end + 1] = 4;
                    }
                    st = 0;
                    while (st < formateds[i].Count)
                    {
                        int start, end;
                        SearchCode(formateds[i].GetRange(st, formateds[i].Count - st), out start, out end);
                        if (start == -1) break;
                        st = end + 1;
                        matrix[start] = 3;
                        matrix[end] = -3;
                        if (start > 0 && matrix[start - 1] == 0) matrix[start - 1] = -4;
                        if (end + 1 < matrix.Length && matrix[end + 1] == 0) matrix[end + 1] = 4;
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
                                    inLines.Add(new Link(formateds[i].GetRange(j, k - j + 1)));
                                    inLines[inLines.Count - 1].startString = y;
                                    inLines[inLines.Count - 1].startX = x;
                                    x += inLines[inLines.Count - 1].value.Width;
                                    break;
                                }
                            case 2:
                                {

                                    break;
                                }
                            case 3:
                                {

                                    break;
                                }
                            case 4:
                                {
                                    inLines.Add(new SimpleInLineText(formateds[i].GetRange(j, k - j + 1)));
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
                    inLines.Add(new SimpleInLineText(formateds[i]));
                    inLines[inLines.Count - 1].startString = y;
                    inLines[inLines.Count - 1].startX = x;
                    x += inLines[inLines.Count - 1].value.Width;
                }
                y += 22;
                if (maxx < x) maxx = x;
                x = 0;
            }

            value = new SimpleLinesControl(inLines, maxx, y);
        }

        private string ToFormatLine(string s)
        {
            s = s.Replace("\\_", Convert.ToString((char)(65535)));
            s = s.Replace("\\*", Convert.ToString((char)(65534)));
            s = s.Replace("\\~", Convert.ToString((char)(65533)));
            string[] array = s.Split(new string[] { "***" }, StringSplitOptions.None);
            for (int i = 1; i < array.Length; i += 2)
            {
                if (i + 1 != array.Length)
                {
                    array[i] = array[i].Replace(System.Environment.NewLine, "***" + System.Environment.NewLine + "***");
                }
            }
            s = array[0];
            for (int i = 1; i < array.Length; i++) s += "***" + array[i];
            s = s.Replace("***", Convert.ToString((char)(65532)));


            array = s.Split(new string[] { "___" }, StringSplitOptions.None);
            for (int i = 1; i < array.Length; i += 2)
            {
                if (i + 1 != array.Length)
                {
                    array[i] = array[i].Replace(System.Environment.NewLine, "___\r\n___");
                }
            }
            s = array[0];
            for (int i = 1; i < array.Length; i++) s += "___" + array[i];
            s = s.Replace("___", Convert.ToString((char)(65531)));


            array = s.Split(new string[] { "~~~" }, StringSplitOptions.None);
            for (int i = 1; i < array.Length; i += 2)
            {
                if (i + 1 != array.Length)
                {
                    array[i] = array[i].Replace(System.Environment.NewLine, "~~~\r\n~~~");
                }
            }
            s = array[0];
            for (int i = 1; i < array.Length; i++) s += "~~~" + array[i];
            s = s.Replace("~~~", Convert.ToString((char)(65530)));

            array = s.Split(new string[] { "**" }, StringSplitOptions.None);
            for (int i = 1; i < array.Length; i += 2)
            {
                if (i + 1 != array.Length)
                {
                    array[i] = array[i].Replace(System.Environment.NewLine, "**\r\n**");
                }
            }
            s = array[0];
            for (int i = 1; i < array.Length; i++) s += "**" + array[i];
            s = s.Replace("**", Convert.ToString((char)(65529)));


            array = s.Split(new string[] { "__" }, StringSplitOptions.None);
            for (int i = 1; i < array.Length; i += 2)
            {
                if (i + 1 != array.Length)
                {
                    array[i] = array[i].Replace(System.Environment.NewLine, "__\r\n__");
                }
            }
            s = array[0];
            for (int i = 1; i < array.Length; i++) s += "__" + array[i];
            s = s.Replace("__", Convert.ToString((char)(65528)));


            array = s.Split(new string[] { "~~" }, StringSplitOptions.None);
            for (int i = 1; i < array.Length; i += 2)
            {
                if (i + 1 != array.Length)
                {
                    array[i] = array[i].Replace(System.Environment.NewLine, "~~\r\n~~");
                }
            }
            s = array[0];
            for (int i = 1; i < array.Length; i++) s += "~~" + array[i];
            s = s.Replace("~~", Convert.ToString((char)(65527)));



            array = s.Split(new string[] { "*" }, StringSplitOptions.None);
            for (int i = 1; i < array.Length; i += 2)
            {
                if (i + 1 != array.Length)
                {
                    array[i] = array[i].Replace(System.Environment.NewLine, "*\r\n*");
                }
            }
            s = array[0];
            for (int i = 1; i < array.Length; i++) s += "*" + array[i];


            array = s.Split(new string[] { "_" }, StringSplitOptions.None);
            for (int i = 1; i < array.Length; i += 2)
            {
                if (i + 1 != array.Length)
                {
                    array[i] = array[i].Replace(System.Environment.NewLine, "_\r\n_");
                }
            }
            s = array[0];
            for (int i = 1; i < array.Length; i++) s += "_" + array[i];


            array = s.Split(new string[] { "~" }, StringSplitOptions.None);
            for (int i = 1; i < array.Length; i += 2)
            {
                if (i + 1 != array.Length)
                {
                    array[i] = array[i].Replace(System.Environment.NewLine, "~\r\n~");
                }
            }
            s = array[0];
            for (int i = 1; i < array.Length; i++) s += "~" + array[i];
            
            s = s.Replace(Convert.ToString((char)(65527)), "~~");
            s = s.Replace(Convert.ToString((char)(65528)), "__");
            s = s.Replace(Convert.ToString((char)(65529)), "**");
            s = s.Replace(Convert.ToString((char)(65530)), "~~~");
            s = s.Replace(Convert.ToString((char)(65531)), "___");
            s = s.Replace(Convert.ToString((char)(65532)), "***");
            
            return s;
        }

        public class SimpleLinesControl : UserControl
        {
            public SimpleLinesControl(List<InLineText> value, int x, int y)
            {
                Size = new Size(x, y);
                for (int i = 0; i < value.Count; i++)
                {
                    Controls.Add(value[i].value);
                    Controls[Controls.Count - 1].Location = new Point(value[i].startX, value[i].startString);
                }
            }
        }
    }
}
