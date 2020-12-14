using System.Collections.Generic;
using System.Windows.Forms;
using System;

namespace MIND.Library
{
    class LinesText
    {
        public int startString;

        public Control value;

        public LinesText(int st)
        {
            startString = st;
        }


        public static bool isLink(List<Formated> value, out int lastpos)
        {
            if (value[0].s != ']')
            {
                for (int j = 1; j + 3 < value.Count; j++)
                {
                    if (value[j - 1].s == '!' && value[j].s == '[')
                    {
                        int last = 0;
                        if (isImage(value.GetRange(j, value.Count - j), out last)) j += last;
                    }
                    if (value[j].s == ']')
                    {
                        if (value[j + 1].s != '(' || value[j + 2].s == ')')
                        {
                            break;
                        }
                        for (int k = j + 2; k < value.Count; k++)
                        {
                            if (value[k].s == ')')
                            {
                                lastpos = k;
                                return true;
                            }
                        }
                    }
                }
            }
            lastpos = -1;
            return false;
        }

        public static bool isImage(List<Formated> value, out int lastPos)
        {
            if (value[0].s == '[')
            {
                for (int j = 1; j + 3 < value.Count; j++)
                {
                    if (value[j - 1].s != '!' && value[j].s == '[')
                    {
                        int last = 0;
                        if (isLink(value.GetRange(j, value.Count - j), out last)) j += last;
                    }
                    if (value[j].s == ']')
                    {
                        if (value[j + 1].s != '(' || value[j + 2].s == ')')
                        {
                            break;
                        }
                        for (int k = j + 2; k < value.Count; k++)
                        {
                            if (value[k].s == ')')
                            {
                                lastPos = k;
                                return true;
                            }
                        }
                    }
                }
            }
            lastPos = -1;
            return false;
        }


        public static bool isCode(List<Formated> value, out int lastPos)
        {
            if (value[0].s != '`')
            {
                for (int j = 1; j < value.Count; j++)
                {
                    if (value[j].s == '`')
                    {
                        lastPos = j;
                        return true;
                    }
                }
            }
            lastPos = -1;
            return false;
        }

        /// <summary>
        /// Нормализация вида для многострочного форматирования
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        protected static string ToFormatLine(string s)
        {
            s = s.Replace("\\_", Convert.ToString((char)(65535)));
            s = s.Replace("\\*", Convert.ToString((char)(65534)));
            s = s.Replace("\\~", Convert.ToString((char)(65533)));
            string[] array = s.Split(new string[] { "***" }, StringSplitOptions.None);
            for (int i = 1; i < array.Length; i += 2)
            {
                if (i + 1 != array.Length && array[i].Length > 0 && array[i] != "\r\n")
                {
                    if (array[i][0] == '\r') { array[i] = array[i].Remove(0, 2); array[i - 1] = array[i - 1].Insert(array[i - 1].Length, "\r\n"); }
                    if (array[i][array[i].Length - 1] == '\n') { array[i] = array[i].Remove(array[i].Length - 2, 2); array[i + 1] = array[i + 1].Insert(0, "\r\n"); }
                    array[i] = array[i].Replace(System.Environment.NewLine, "***" + ((char)(65526)) + "\r\n" + ((char)(65526)) + "***");

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
                    array[i] = array[i].Replace(System.Environment.NewLine, "___" + ((char)(65526)) + "\r\n" + ((char)(65526)) + "___");
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
                    array[i] = array[i].Replace(System.Environment.NewLine, "~~~" + ((char)(65526)) + "\r\n" + ((char)(65526)) + "~~~");
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
                    array[i] = array[i].Replace(System.Environment.NewLine, "**" + ((char)(65526)) + "\r\n" + ((char)(65526)) + "**");
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
                    array[i] = array[i].Replace(System.Environment.NewLine, "__" + ((char)(65526)) + "\r\n" + ((char)(65526)) + "__");
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
                    array[i] = array[i].Replace(System.Environment.NewLine, "~~" + ((char)(65526)) + "\r\n" + ((char)(65526)) + "~~");
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
                    array[i] = array[i].Replace(System.Environment.NewLine, "*" + ((char)(65526)) + "\r\n" + ((char)(65526)) + "*");
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
                    array[i] = array[i].Replace(System.Environment.NewLine, "_" + ((char)(65526)) + "\r\n" + ((char)(65526)) + "_");
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
                    array[i] = array[i].Replace(System.Environment.NewLine, "~" + ((char)(65526)) + "\r\n" + ((char)(65526)) + "~");
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
    }
}
