using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MIND.Library
{
    class InLineText
    {
        int stratString;

        protected FontStyle Format(bool isItalic, bool isBolt, bool isStricedOut, bool isUnderLine)
        {
            FontStyle style = FontStyle.Regular;
            if (isItalic) style = style | FontStyle.Italic;
            if (isBolt) style = style | FontStyle.Bold;
            if (isStricedOut) style = style | FontStyle.Strikeout;
            if (isUnderLine) style = (style | FontStyle.Underline);
            return style;
        }

    }
}
