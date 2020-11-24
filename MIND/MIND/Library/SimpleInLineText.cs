using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MIND.Library
{
    class SimpleInLineText:InLineText
    {
        public List<Label> value;

        public SimpleInLineText(string s)
        {
            s = s.Replace("\\_", Convert.ToString((char)(65535)));
            s = s.Replace("\\*", Convert.ToString((char)(65534)));
            List<Formated> formateds = new List<Formated>();
            for (int i = 0; i < s.Length; i++) formateds.Add(new Formated(s[i]));
            for (int i = 0; i+6 < formateds.Count; i++)
            {
                if(formateds[i].s == '*' && formateds[i+1].s == '*' && formateds[i+2].s == '*' && formateds[i+3].s != '*')
                {
                    for(int j = i + 4; j+2 < formateds.Count; j++)
                    {
                        if(formateds[j].s == '*' && formateds[j + 1].s == '*' && formateds[j + 2].s == '*')
                        {
                            for(int c = i+3; c < j; c++)
                            {
                                formateds[c].isBolt = true; formateds[c].isItalic = true;
                            }
                            formateds.RemoveRange(j, 3);
                            formateds.RemoveRange(i, 3);
                            i = j - 4;
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i + 6 < formateds.Count; i++)
            {
                if (formateds[i].s == '_' && formateds[i + 1].s == '_' && formateds[i + 2].s == '_' && formateds[i + 3].s != '_')
                {
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
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i + 6 < formateds.Count; i++)
            {
                if (formateds[i].s == '~' && formateds[i + 1].s == '~' && formateds[i + 2].s == '~' && formateds[i + 3].s != '~')
                {
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
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i + 4 < formateds.Count; i++)
            {
                if (formateds[i].s == '*' && formateds[i + 1].s == '*' && formateds[i + 2].s != '*')
                {
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
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i + 4 < formateds.Count; i++)
            {
                if (formateds[i].s == '_' && formateds[i + 1].s == '_' && formateds[i + 2].s != '_')
                {
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
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i + 4 < formateds.Count; i++)
            {
                if (formateds[i].s == '~' && formateds[i + 1].s == '~' && formateds[i + 2].s != '~')
                {
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
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i + 2 < formateds.Count; i++)
            {
                if (formateds[i].s == '*' && formateds[i + 1].s != '*')
                {
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
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i + 2 < formateds.Count; i++)
            {
                if (formateds[i].s == '_' && formateds[i + 1].s != '_')
                {
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
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i + 2 < formateds.Count; i++)
            {
                if (formateds[i].s == '~' && formateds[i + 1].s != '~')
                {
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
                            break;
                        }
                    }
                }
            }

            value = new List<Label>();
            for (int i = 0; i < formateds.Count; i++)
            {
                string current = "";
                for(int j = i; true; j++)
                {
                    if(j < formateds.Count && formateds[i].isBolt == formateds[j].isBolt && formateds[i].isItalic == formateds[j].isItalic && formateds[i].isStricedOut == formateds[j].isStricedOut && formateds[i].isUnderLine == formateds[j].isUnderLine)
                    {
                        current += formateds[j].s;
                    }
                    else
                    {
                        value.Add(new Label());
                        value[value.Count - 1].AutoSize = true;
                        value[value.Count - 1].Text = current;
                        current = "";
                        value[value.Count - 1].ForeColor = Color.White;
                        value[value.Count - 1].Font = new Font("Times New Roman", 14.25F, Format(formateds[i].isItalic, formateds[i].isBolt, formateds[i].isStricedOut, formateds[i].isUnderLine), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        i = j-1;
                        break;
                    }    
                }
            }

        }



        class Formated
        {
            public char s;
            public bool isItalic = false, isBolt = false, isStricedOut = false, isUnderLine = false;

            public Formated(char str)
            {
                s = str;
            }
        }
    }
}
