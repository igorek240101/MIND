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
            int x = 0 , y = 5, maxx = 0 , count_of_code = 0;
            List<InLineText> inLines = new List<InLineText>();
            s = ToFormatLine(s);
            string[] array = s.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            List<Formated>[] formateds = new List<Formated>[array.Length];
            List<List<Formated>> codes = new List<List<Formated>>();
            for(int i = 0; i < formateds.Length; i++)
            {
                List<Formated> codes_formated = new List<Formated>();
                for (int j = 0; j < array[i].Length; j++) codes_formated.Add(new Formated(array[i][j]));
                int st = 0;
                while (st < codes_formated.Count)
                {
                    int start, end;
                    SearchCode(codes_formated.GetRange(st, codes_formated.Count - st), out start, out end);
                    if (start == -1) break;
                    codes.Add(new List<Formated>());
                    for(int j = start; j <= end; j++)
                    {
                        codes[codes.Count - 1].Add(new Formated(codes_formated[j].s));
                    }
                    st = end + 1;
                }
            }
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

                        if (start + st == 0 || matrix[start+st-1] != 2)matrix[start+st] = 1;
                        if (end + st + 1 == matrix.Length || matrix[end + st + 1] != -2) matrix[end+st] = -1;
                        if (start+st > 0 && matrix[start+st - 1] == 0) matrix[start+st - 1] = -4;
                        if (end+st + 1 < matrix.Length && matrix[end+st + 1] == 0) matrix[end+st + 1] = 4;
                        st += end + 1;
                    }
                    st = 0;
                    while (st < formateds[i].Count)
                    {
                        int start, end;
                        SearchImage(formateds[i].GetRange(st, formateds[i].Count - st), out start, out end);
                        if (start == -1) break;
                        for(int j = start + st; j >= 0; j--)
                        {
                            if (matrix[j] > 0 && start + st != 0 && (j != 0 || matrix[j] != 4)) break;
                            if (j == 0)
                            {
                                matrix[start + st] = 2; matrix[end + st] = -2;
                                if (start + st > 0 && matrix[start + st - 1] == 0) matrix[start + st - 1] = -4;
                                if (end + st + 1 < matrix.Length && matrix[end + st + 1] == 0) matrix[end + st + 1] = 4;
                            }
                        }
                        st += end + 1;
                    }
                    st = 0;
                    while (st < formateds[i].Count)
                    {
                        int start, end;
                        SearchCode(formateds[i].GetRange(st, formateds[i].Count - st), out start, out end);
                        if (start == -1) break;
                        matrix[start+st] = 3;
                        matrix[end+st] = -3;
                        if (start+st > 0 && matrix[start+st - 1] == 0) matrix[start+st - 1] = -4;
                        if (end+st + 1 < matrix.Length && matrix[end+st + 1] == 0) matrix[end+st + 1] = 4;
                        st += end + 1;
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
                                    inLines.Add(new ImageText(formateds[i].GetRange(j, k - j + 1), null));
                                    y += 11; x = 0;
                                    inLines[inLines.Count - 1].startString = y;
                                    inLines[inLines.Count - 1].startX = x;
                                    if (maxx < inLines[inLines.Count - 1].value.Width) maxx = inLines[inLines.Count - 1].value.Width;
                                    y += inLines[inLines.Count - 1].value.Height;
                                    y -= 11;
                                    break;
                                }
                            case 3:
                                {
                                    inLines.Add(new InLineCode(codes[count_of_code]));
                                    count_of_code++;
                                    inLines[inLines.Count - 1].startString = y;
                                    inLines[inLines.Count - 1].startX = x;
                                    x += inLines[inLines.Count - 1].value.Width;
                                    count_of_code++;
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
                if (i + 1 != array.Length && array[i].Length>0 && array[i] != "\r\n")
                {
                    if (array[i][0] == '\r') { array[i] = array[i].Remove(0, 2); array[i-1] = array[i-1].Insert(array[i-1].Length,"\r\n"); }
                    if (array[i][array[i].Length-1] == '\n') {array[i] = array[i].Remove(array[i].Length-2, 2); array[i+1] = array[i+1].Insert(0, "\r\n"); }
                    array[i] = array[i].Replace(System.Environment.NewLine, "***\r\n***");

                }
            }
            s = array[0];
            for (int i = 1; i < array.Length; i++) s += "***" + array[i];
            s = s.Replace("***", Convert.ToString((char)(65532)));


            array = s.Split(new string[] { "___" }, StringSplitOptions.None);
            for (int i = 1; i < array.Length; i += 2)
            {
                if (i + 1 != array.Length && array[i].Length > 0 && array[i] != "\r\n")
                {
                    if (array[i][0] == '\r') { array[i] = array[i].Remove(0, 2); array[i - 1] = array[i - 1].Insert(array[i - 1].Length, "\r\n"); }
                    if (array[i][array[i].Length - 1] == '\n') { array[i] = array[i].Remove(array[i].Length - 2, 2); array[i + 1] = array[i + 1].Insert(0, "\r\n"); }
                    array[i] = array[i].Replace(System.Environment.NewLine, "___\r\n___");
                }
            }
            s = array[0];
            for (int i = 1; i < array.Length; i++) s += "___" + array[i];
            s = s.Replace("___", Convert.ToString((char)(65531)));


            array = s.Split(new string[] { "~~~" }, StringSplitOptions.None);
            for (int i = 1; i < array.Length; i += 2)
            {
                if (i + 1 != array.Length && array[i].Length > 0 && array[i] != "\r\n")
                {
                    if (array[i][0] == '\r') { array[i] = array[i].Remove(0, 2); array[i - 1] = array[i - 1].Insert(array[i - 1].Length, "\r\n"); }
                    if (array[i][array[i].Length - 1] == '\n') { array[i] = array[i].Remove(array[i].Length - 2, 2); array[i + 1] = array[i + 1].Insert(0, "\r\n"); }
                    array[i] = array[i].Replace(System.Environment.NewLine, "~~~\r\n~~~");
                }
            }
            s = array[0];
            for (int i = 1; i < array.Length; i++) s += "~~~" + array[i];
            s = s.Replace("~~~", Convert.ToString((char)(65530)));

            array = s.Split(new string[] { "**" }, StringSplitOptions.None);
            for (int i = 1; i < array.Length; i += 2)
            {
                if (i + 1 != array.Length && array[i].Length > 0 && array[i] != "\r\n")
                {
                    if (array[i][0] == '\r') { array[i] = array[i].Remove(0, 2); array[i - 1] = array[i - 1].Insert(array[i - 1].Length, "\r\n"); }
                    if (array[i][array[i].Length - 1] == '\n') { array[i] = array[i].Remove(array[i].Length - 2, 2); array[i + 1] = array[i + 1].Insert(0, "\r\n"); }
                    array[i] = array[i].Replace(System.Environment.NewLine, "**\r\n**");
                }
            }
            s = array[0];
            for (int i = 1; i < array.Length; i++) s += "**" + array[i];
            s = s.Replace("**", Convert.ToString((char)(65529)));


            array = s.Split(new string[] { "__" }, StringSplitOptions.None);
            for (int i = 1; i < array.Length; i += 2)
            {
                if (i + 1 != array.Length && array[i].Length > 0 && array[i] != "\r\n")
                {
                    if (array[i][0] == '\r') { array[i] = array[i].Remove(0, 2); array[i - 1] = array[i - 1].Insert(array[i - 1].Length, "\r\n"); }
                    if (array[i][array[i].Length - 1] == '\n') { array[i] = array[i].Remove(array[i].Length - 2, 2); array[i + 1] = array[i + 1].Insert(0, "\r\n"); }
                    array[i] = array[i].Replace(System.Environment.NewLine, "__\r\n__");
                }
            }
            s = array[0];
            for (int i = 1; i < array.Length; i++) s += "__" + array[i];
            s = s.Replace("__", Convert.ToString((char)(65528)));


            array = s.Split(new string[] { "~~" }, StringSplitOptions.None);
            for (int i = 1; i < array.Length; i += 2)
            {
                if (i + 1 != array.Length && array[i].Length > 0 && array[i] != "\r\n")
                {
                    if (array[i][0] == '\r') { array[i] = array[i].Remove(0, 2); array[i - 1] = array[i - 1].Insert(array[i - 1].Length, "\r\n"); }
                    if (array[i][array[i].Length - 1] == '\n') { array[i] = array[i].Remove(array[i].Length - 2, 2); array[i + 1] = array[i + 1].Insert(0, "\r\n"); }
                    array[i] = array[i].Replace(System.Environment.NewLine, "~~\r\n~~");
                }
            }
            s = array[0];
            for (int i = 1; i < array.Length; i++) s += "~~" + array[i];
            s = s.Replace("~~", Convert.ToString((char)(65527)));



            array = s.Split(new string[] { "*" }, StringSplitOptions.None);
            for (int i = 1; i < array.Length; i += 2)
            {
                if (i + 1 != array.Length && array[i].Length > 0 && array[i] != "\r\n")
                {
                    if (array[i][0] == '\r') { array[i] = array[i].Remove(0, 2); array[i - 1] = array[i - 1].Insert(array[i - 1].Length, "\r\n"); }
                    if (array[i][array[i].Length - 1] == '\n') { array[i] = array[i].Remove(array[i].Length - 2, 2); array[i + 1] = array[i + 1].Insert(0, "\r\n"); }
                    array[i] = array[i].Replace(System.Environment.NewLine, "*\r\n*");
                }
            }
            s = array[0];
            for (int i = 1; i < array.Length; i++) s += "*" + array[i];


            array = s.Split(new string[] { "_" }, StringSplitOptions.None);
            for (int i = 1; i < array.Length; i += 2)
            {
                if (i + 1 != array.Length && array[i].Length > 0 && array[i] != "\r\n")
                {
                    if (array[i][0] == '\r') { array[i] = array[i].Remove(0, 2); array[i - 1] = array[i - 1].Insert(array[i - 1].Length, "\r\n"); }
                    if (array[i][array[i].Length - 1] == '\n') { array[i] = array[i].Remove(array[i].Length - 2, 2); array[i + 1] = array[i + 1].Insert(0, "\r\n"); }
                    array[i] = array[i].Replace(System.Environment.NewLine, "_\r\n_");
                }
            }
            s = array[0];
            for (int i = 1; i < array.Length; i++) s += "_" + array[i];


            array = s.Split(new string[] { "~" }, StringSplitOptions.None);
            for (int i = 1; i < array.Length; i += 2)
            {
                if (i + 1 != array.Length && array[i].Length > 0 && array[i] != "\r\n")
                {
                    if (array[i][0] == '\r') { array[i] = array[i].Remove(0, 2); array[i - 1] = array[i - 1].Insert(array[i - 1].Length, "\r\n"); }
                    if (array[i][array[i].Length - 1] == '\n') { array[i] = array[i].Remove(array[i].Length - 2, 2); array[i + 1] = array[i + 1].Insert(0, "\r\n"); }
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
