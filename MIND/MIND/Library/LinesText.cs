using System.Collections.Generic;

namespace MIND.Library
{
    class LinesText
    {
        protected int startString;


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


    }
}
