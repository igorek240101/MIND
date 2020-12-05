using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace MIND.Library
{
    public class InLineText
    {
        public int startString;
        public int startX;
        public Control value;

        public FontStyle Format(bool isItalic, bool isBolt, bool isStricedOut, bool isUnderLine)
        {
            FontStyle style = FontStyle.Regular;
            if (isItalic) style = style | FontStyle.Italic;
            if (isBolt) style = style | FontStyle.Bold;
            if (isStricedOut) style = style | FontStyle.Strikeout;
            if (isUnderLine) style = (style | FontStyle.Underline);
            return style;
        }


        public static List<Formated> SearchFormat(string s)
        {
            List<Formated> formateds = new List<Formated>();
            for (int i = 0; i < s.Length; i++) formateds.Add(new Formated(s[i]));
            for (int i = 0; i + 6 < formateds.Count; i++)
            {
                if (formateds[i].s == '*' && formateds[i + 1].s == '*' && formateds[i + 2].s == '*' && formateds[i + 3].s != '*')
                {
                    bool tr = true;
                    for (int j = i + 4; j + 2 < formateds.Count; j++)
                    {
                        if (formateds[j].s == '*' && formateds[j + 1].s == '*' && formateds[j + 2].s == '*')
                        {
                            for (int c = i + 3; c < j; c++)
                            {
                                formateds[c].isBolt = true; formateds[c].isItalic = true;
                            }
                            formateds.RemoveRange(j, 3);
                            formateds.RemoveRange(i, 3);
                            i = j - 4;
                            tr = false;
                            break;
                        }
                    }
                    if (tr) { formateds[i].s = ((char)(65534)); formateds[i + 1].s = ((char)(65534)); formateds[i + 2].s = ((char)(65534)); }
                }
            }
            for (int i = 0; i + 6 < formateds.Count; i++)
            {
                if (formateds[i].s == '_' && formateds[i + 1].s == '_' && formateds[i + 2].s == '_' && formateds[i + 3].s != '_')
                {
                    bool tr = true;
                    for (int j = i + 4; j + 2 < formateds.Count; j++)
                    {
                        if (formateds[j].s == '_' && formateds[j + 1].s == '_' && formateds[j + 2].s == '_')
                        {
                            for (int c = i + 3; c < j; c++)
                            {
                                formateds[c].isBolt = true; formateds[c].isItalic = true;
                            }
                            formateds.RemoveRange(j, 3);
                            formateds.RemoveRange(i, 3);
                            i = j - 4;
                            tr = false;
                            break;
                        }
                    }
                    if (tr) { formateds[i].s = ((char)(65535)); formateds[i + 1].s = ((char)(65535)); formateds[i + 2].s = ((char)(65535)); }
                }
            }
            for (int i = 0; i + 6 < formateds.Count; i++)
            {
                if (formateds[i].s == '~' && formateds[i + 1].s == '~' && formateds[i + 2].s == '~' && formateds[i + 3].s != '~')
                {
                    bool tr = true;
                    for (int j = i + 4; j + 2 < formateds.Count; j++)
                    {
                        if (formateds[j].s == '~' && formateds[j + 1].s == '~' && formateds[j + 2].s == '~')
                        {
                            for (int c = i + 3; c < j; c++)
                            {
                                formateds[c].isStricedOut = true; formateds[c].isUnderLine = true;
                            }
                            formateds.RemoveRange(j, 3);
                            formateds.RemoveRange(i, 3);
                            i = j - 4;
                            tr = false;
                            break;
                        }
                    }
                    if (tr) { formateds[i].s = ((char)(65533)); formateds[i + 1].s = ((char)(65533)); formateds[i + 2].s = ((char)(65533)); }
                }
            }
            for (int i = 0; i + 4 < formateds.Count; i++)
            {
                if (formateds[i].s == '*' && formateds[i + 1].s == '*' && formateds[i + 2].s != '*')
                {
                    bool tr = true;
                    for (int j = i + 3; j + 1 < formateds.Count; j++)
                    {
                        if (formateds[j].s == '*' && formateds[j + 1].s == '*')
                        {
                            for (int c = i + 2; c < j; c++)
                            {
                                formateds[c].isBolt = true;
                            }
                            formateds.RemoveRange(j, 2);
                            formateds.RemoveRange(i, 2);
                            i = j - 3;
                            tr = false;
                            break;
                        }
                    }
                    if (tr) { formateds[i].s = ((char)(65534)); formateds[i + 1].s = ((char)(65534)); }
                }
            }
            for (int i = 0; i + 4 < formateds.Count; i++)
            {
                if (formateds[i].s == '_' && formateds[i + 1].s == '_' && formateds[i + 2].s != '_')
                {
                    bool tr = true;
                    for (int j = i + 3; j + 1 < formateds.Count; j++)
                    {
                        if (formateds[j].s == '_' && formateds[j + 1].s == '_')
                        {
                            for (int c = i + 2; c < j; c++)
                            {
                                formateds[c].isBolt = true;
                            }
                            formateds.RemoveRange(j, 2);
                            formateds.RemoveRange(i, 2);
                            i = j - 3;
                            tr = false;
                            break;
                        }
                    }
                    if (tr) { formateds[i].s = ((char)(65535)); formateds[i + 1].s = ((char)(65535)); }
                }
            }
            for (int i = 0; i + 4 < formateds.Count; i++)
            {
                if (formateds[i].s == '~' && formateds[i + 1].s == '~' && formateds[i + 2].s != '~')
                {
                    bool tr = true;
                    for (int j = i + 3; j + 1 < formateds.Count; j++)
                    {
                        if (formateds[j].s == '~' && formateds[j + 1].s == '~')
                        {
                            for (int c = i + 2; c < j; c++)
                            {
                                formateds[c].isStricedOut = true;
                            }
                            formateds.RemoveRange(j, 2);
                            formateds.RemoveRange(i, 2);
                            i = j - 3;
                            tr = false;
                            break;
                        }
                    }
                    if (tr) { formateds[i].s = ((char)(65533)); formateds[i + 1].s = ((char)(65533)); }
                }
            }
            for (int i = 0; i + 2 < formateds.Count; i++)
            {
                if (formateds[i].s == '*' && formateds[i + 1].s != '*')
                {
                    bool tr = true;
                    for (int j = i + 2; j < formateds.Count; j++)
                    {
                        if (formateds[j].s == '*')
                        {
                            for (int c = i + 1; c < j; c++)
                            {
                                formateds[c].isItalic = true;
                            }
                            formateds.RemoveRange(j, 1);
                            formateds.RemoveRange(i, 1);
                            i = j - 2;
                            tr = false;
                            break;
                        }
                    }
                    if (tr) formateds[i].s = ((char)(65534));
                }
            }
            for (int i = 0; i + 2 < formateds.Count; i++)
            {
                if (formateds[i].s == '_' && formateds[i + 1].s != '_')
                {
                    bool tr = true;
                    for (int j = i + 2; j < formateds.Count; j++)
                    {
                        if (formateds[j].s == '_')
                        {
                            for (int c = i + 1; c < j; c++)
                            {
                                formateds[c].isItalic = true;
                            }
                            formateds.RemoveRange(j, 1);
                            formateds.RemoveRange(i, 1);
                            i = j - 2;
                            tr = false;
                            break;
                        }
                    }
                    if (tr) formateds[i].s = ((char)(65535));
                }
            }
            for (int i = 0; i + 2 < formateds.Count; i++)
            {
                if (formateds[i].s == '~' && formateds[i + 1].s != '~')
                {
                    bool tr = true;
                    for (int j = i + 2; j < formateds.Count; j++)
                    {
                        if (formateds[j].s == '~')
                        {
                            for (int c = i + 1; c < j; c++)
                            {
                                formateds[c].isUnderLine = true;
                            }
                            formateds.RemoveRange(j, 1);
                            formateds.RemoveRange(i, 1);
                            i = j - 2;
                            tr = false;
                            break;
                        }
                    }
                    if (tr) formateds[i].s = ((char)(65533));
                }
            }
            for(int i = 0; i < formateds.Count; i++)
            {
                if (formateds[i].s == 65533) formateds[i].s = '~';
                if (formateds[i].s == 65534) formateds[i].s = '*';
                if (formateds[i].s == 65535) formateds[i].s = '_';
            }
            return formateds;

        }

    }


    public class Formated
    {
        public char s;
        public bool isItalic = false, isBolt = false, isStricedOut = false, isUnderLine = false;

        public Formated(char str)
        {
            s = str;
        }
    }
}
