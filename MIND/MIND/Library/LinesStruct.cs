using System.Collections.Generic;
using System.Windows.Forms;
using System;

namespace MIND.Library
{
    static class LinesStruct
    {
        /// <summary>
        /// Метод проверяет является ли входная строка заголовком
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool isHeader(string s)
        {
            for (int i = 0; i < s.Length && i < 7; i++)
            {
                if (s[i] != '#')
                {
                    if (i == 0) return false;
                    if (s[i] == ' ') return true;
                    else return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Метод проверяет является ли входная строка цитатой
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool isQuotation(string s)
        {
            return s.Length>2 && s[0] == '>';
        }

        /// <summary>
        /// Метод проверяет является ли входная строка элементом списка
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool isList(string s)
        {
            s = s.TrimStart(' ');
            if(s.Length > 2)
            {
                if (s[0] == '-' && s[1] == ' ') return true;
                else if(s.Length > 3)
                {
                    for(int i = 0; i < 10; i++)
                    {
                        if (s[0] == Convert.ToString(i)[0] && s[1] == '.' && s[2] == ' ') return true;
                    }
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// Метод проверяет является ли входная строка горизонтальной линией
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool isLine(string s)
        {
            return s.Length >=3 && (s.Replace("*", "").Length == 0 || s.Replace("_", "").Length == 0 || s.Replace("-", "").Length == 0);
        }

        /// <summary>
        /// Метод проверяет является ли входная строка делом (из списка дел)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool isChek(string s)
        {
            return s.Length > 3 && s[0] == '[' && (s[1] == ' ' || s[1] == 'X') && s[2] == ']' && s[3] == ' ';
        }

        /// <summary>
        /// Метод проверяет является ли входная строка таблицей
        /// </summary>
        /// <param name="s">string со стоящих из двух строк, являющихся перввыми двумя строками Markdown-таблицы</param>
        /// <returns></returns>
        public static bool isTable(string s)
        {
            string[] str = s.Split('\r');
            int box_count = str[0].Split('|').Length-2;
            str = str[1].Split('|');
            if (str.Length-2 == box_count && box_count != -1)
            {
                for(int i = 1; i < str.Length-1; i++)
                {
                    if (str[i].Length == 0) return false;
                    for(int j = 0; j < str[i].Length; j++)
                    {
                        if (str[i][j] != '-')
                        {
                            if (j == 0 || j == str[i].Length - 1)
                            {
                                if (str[i][j] != ':') return false;
                            }
                            else return false;
                        }
                    }
                }
                return true;
            }
            else return false;
        }


        public static void MainSearch(string s, int startY, List<LinesText> linesTexts, Panel panel, int indexOfInsert, Form1 f)
        {
            int tableCount = -1;
            string[] array = s.Split(new string[] { "\r\n" }, System.StringSplitOptions.None);
            int lastline = 0, lastconst = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == "" && i > lastline)
                {
                    if (i != 0 && i + 1 < array.Length)
                    {
                        LastCreate(array, lastline, lastconst, i, panel, ref indexOfInsert, linesTexts, startY);
                        lastconst = 0;
                        lastline = i;
                    }
                    else
                    {
                        if (lastconst != 0)
                        {
                            LastCreate(array, lastline, lastconst, i, panel, ref indexOfInsert, linesTexts, startY);
                            lastconst = 0;
                            lastline = i;
                        }
                    }
                }
                else
                {
                    if (isHeader(array[i]))
                    {
                        if (i > lastline)
                        {
                            LastCreate(array, lastline, lastconst, i, panel, ref indexOfInsert, linesTexts, startY);
                        }
                        try
                        {
                            linesTexts.Insert(indexOfInsert, new HeaderLines(array[i], i + startY));
                            panel.Controls.Add(linesTexts[indexOfInsert].value);
                        }
                        catch { linesTexts.Add(new HeaderLines(array[i], i + startY)); panel.Controls.Add(linesTexts[indexOfInsert - 1].value); }
                        indexOfInsert++;
                        lastconst = 0;
                        lastline = i + 1;
                    }
                    else
                    {
                        if (isQuotation(array[i]))
                        {
                            if (i > lastline && lastconst != 1)
                            {
                                LastCreate(array, lastline, lastconst, i, panel, ref indexOfInsert, linesTexts, startY);
                            }
                            if (lastconst != 1) { lastconst = 1; lastline = i; }
                        }
                        else
                        {
                            if (isList(array[i]))
                            {
                                if (i > lastline && lastconst != 2)
                                {
                                    LastCreate(array, lastline, lastconst, i, panel, ref indexOfInsert, linesTexts, startY);
                                }
                                if (lastconst != 2) { lastconst = 2; lastline = i; }
                            }
                            else
                            {
                                if (isLine(array[i]))
                                {
                                    if (i > lastline)
                                    {
                                        LastCreate(array, lastline, lastconst, i, panel, ref indexOfInsert, linesTexts, startY);
                                    }
                                    try
                                    {
                                        linesTexts.Insert(indexOfInsert, new LineLines(array[i], i + startY));
                                        panel.Controls.Add(linesTexts[indexOfInsert].value);
                                    }
                                    catch { linesTexts.Add(new LineLines(array[i], i + startY)); panel.Controls.Add(linesTexts[indexOfInsert - 1].value); }
                                    linesTexts[indexOfInsert].value.Size = new System.Drawing.Size(500, 10);
                                    indexOfInsert++;
                                    lastconst = 0;
                                    lastline = i + 1;
                                }
                                else
                                {
                                    if (isChek(array[i]))
                                    {
                                        if (i > lastline)
                                        {
                                            LastCreate(array, lastline, lastconst, i, panel, ref indexOfInsert, linesTexts, startY);
                                        }
                                        try
                                        {
                                            linesTexts.Insert(indexOfInsert, new ChekLine(array[i], i + startY, f));
                                            panel.Controls.Add(linesTexts[indexOfInsert].value);
                                        }
                                        catch { linesTexts.Add(new ChekLine(array[i], i + startY, f)); panel.Controls.Add(linesTexts[indexOfInsert - 1].value); }
                                        indexOfInsert++;
                                        lastconst = 0;
                                        lastline = i + 1;
                                    }
                                    else
                                    {
                                        if (lastconst == 3 && array[i].Split('|').Length >= tableCount) continue;
                                        else
                                        {
                                            if (array[i] == "```")
                                            {
                                                bool tr = false;
                                                for (int j = i + 2; j < array.Length; j++)
                                                {
                                                    if (array[j] == "```")
                                                    {
                                                        tr = true;
                                                        string code = "";
                                                        for (int k = i; k <= j; k++)
                                                        {
                                                            code += array[k] + "\r\n";
                                                        }
                                                        code = code.Substring(0, code.Length - 2);
                                                        if (i > lastline)
                                                        {
                                                            LastCreate(array, lastline, lastconst, i, panel, ref indexOfInsert, linesTexts, startY);
                                                        }
                                                        try
                                                        {
                                                            linesTexts.Insert(indexOfInsert, new LineCode(code, i + startY));
                                                            panel.Controls.Add(linesTexts[indexOfInsert].value);
                                                        }
                                                        catch { linesTexts.Add(new LineCode(code, i + startY)); panel.Controls.Add(linesTexts[indexOfInsert - 1].value); }
                                                        indexOfInsert++;
                                                        i = j;
                                                        lastconst = 0;
                                                        lastline = i + 1;
                                                        break;
                                                    }
                                                }
                                                if (!tr)
                                                {
                                                    if (i > lastline && lastconst != 0)
                                                    {
                                                        LastCreate(array, lastline, lastconst, i, panel, ref indexOfInsert, linesTexts, startY);
                                                    }
                                                    lastconst = 0;
                                                    lastline = i;

                                                }
                                            }
                                            else
                                            {
                                                if (i + 1 < array.Length && isTable(array[i] + '\r' + array[i + 1]))
                                                {
                                                    if (i > lastline && lastconst != 3)
                                                    {
                                                        LastCreate(array, lastline, lastconst, i, panel, ref indexOfInsert, linesTexts, startY);
                                                    }
                                                    lastconst = 3; lastline = i; i++; tableCount = array[i].Split('|').Length;
                                                }
                                                else
                                                {
                                                    if (i > lastline && lastconst != 0)
                                                    {
                                                        LastCreate(array, lastline, lastconst, i, panel, ref indexOfInsert, linesTexts, startY);
                                                    }
                                                    if (lastconst != 0) { lastconst = 0; lastline = i; }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (lastline != array.Length)
            {
                LastCreate(array, lastline, lastconst, array.Length, panel, ref indexOfInsert, linesTexts, startY);
            }
        }

        public static void LastCreate(string[] array, int lastline, int lastconst, int now, Panel panel, ref int indexOfInsert, List<LinesText> linesTexts, int startY)
        {
            string s = "";
            for (int i = lastline; i < now; i++)
            {
                s += "\r\n" + array[i];
            }
            s = s.Substring(2);
            switch (lastconst)
            {
                case 0:
                    {
                        try
                        {
                            linesTexts.Insert(indexOfInsert, new SimpleLines(s, lastline + startY));
                            panel.Controls.Add(linesTexts[indexOfInsert].value);
                        }
                        catch { linesTexts.Add(new SimpleLines(s, lastline + startY)); panel.Controls.Add(linesTexts[indexOfInsert - 1].value); }
                        indexOfInsert++;
                        break;
                    }
                case 1:
                    {
                        try
                        {
                            linesTexts.Insert(indexOfInsert, new QuotationLines(s, lastline + startY));
                            panel.Controls.Add(linesTexts[indexOfInsert].value);
                        }
                        catch { linesTexts.Add(new QuotationLines(s, lastline + startY)); panel.Controls.Add(linesTexts[indexOfInsert - 1].value); }
                        indexOfInsert++;
                        break;
                    }
                case 2:
                    {
                        try
                        {
                            linesTexts.Insert(indexOfInsert, new ListLine(s, lastline + startY));
                            panel.Controls.Add(linesTexts[indexOfInsert].value);
                        }
                        catch { linesTexts.Add(new ListLine(s, lastline + startY)); panel.Controls.Add(linesTexts[indexOfInsert - 1].value); }
                        indexOfInsert++;
                        break;
                    }
                case 3:
                    {
                        try
                        {
                            linesTexts.Insert(indexOfInsert, new TableLines(s, lastline + startY));
                            panel.Controls.Add(linesTexts[indexOfInsert].value);
                        }
                        catch { linesTexts.Add(new TableLines(s, lastline + startY)); panel.Controls.Add(linesTexts[indexOfInsert - 1].value); }
                        indexOfInsert++;
                        break;
                    }

            }
        }
    }
}
