using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIND.Library
{
    class LinesText
    {
        protected int startString;


        public static void SearchLink(List<Formated> value, out int startPos, out int lastpos)
        {
            for(int i = 0; i+5 < value.Count; i++)
            {
                if(value[i].s == '[' && value[i+1].s != ']' && (i == 0 || value[i-1].s != '!'))
                {
                    for(int j = i+2; j+3 < value.Count; j++)
                    {
                        if(value[j-1].s == '!' && value[j].s == '[')
                        {
                            int start = 0, last = 0;
                            SearchImage(value.GetRange(j - 1, value.Count - (j - 1)), out start, out last);
                            if (start == 0) j += last - 1;
                        }
                        if(value[j].s == ']')
                        {
                            if(value[j+1].s != '(' || value[j+2].s == ')')
                            {
                                i = j;
                                goto mark1;
                            }
                            for(int k = j+2; k < value.Count; k++)
                            {
                                if(value[k].s == ')')
                                {
                                    startPos = i; lastpos = k;
                                    return;
                                }
                            }
                        }
                    }
                }
            mark1:;
            }
            startPos = -1; lastpos = -1;
        }

        public static void SearchImage(List<Formated> value, out int startPos, out int lastPos)
        {
            for (int i = 0; i + 5 < value.Count; i++)
            {
                if (value[i].s == '!' && value[i + 1].s == '[')
                {
                    for (int j = i + 2; j + 3 < value.Count; j++)
                    {
                        if (value[j - 1].s != '!' && value[j].s == '[')
                        {
                            int start = 0, last = 0;
                            SearchLink(value.GetRange(j, value.Count - j), out start, out last);
                            if (start == 0) j = last;
                        }
                        if (value[j].s == ']')
                        {
                            if (value[j + 1].s != '(' || value[j + 2].s == ')')
                            {
                                i = j;
                                goto mark1;
                            }
                            for (int k = j + 2; k < value.Count; k++)
                            {
                                if (value[k].s == ')')
                                {
                                    startPos = i; lastPos = k;
                                    return;
                                }
                            }
                        }
                    }
                }
            mark1:;
            }
            startPos = -1; lastPos = -1;
        }

        public static void SearchCode(List<Formated> value, out int startPos, out int lastPos)
        {
            for(int i = 0; i+2 < value.Count; i++)
            {
                if(value[i].s == '`' && value[i+1].s != '`')
                {
                    for(int j = i+2; j < value.Count; j++)
                    {
                        if(value[j].s == '`')
                        {
                            startPos = i; lastPos = j;
                            return;
                        }
                    }
                    startPos = -1; lastPos = -1;
                    return;
                }
            }
            startPos = -1; lastPos = -1;
        }


    }
}
