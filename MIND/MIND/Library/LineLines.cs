using System.Drawing;
using System.Windows.Forms;

namespace MIND.Library
{
    class LineLines : LinesText
    {
        public int w;
        public LineLines(string s, int st) : base(st)
        {
            if (s[0] == '*') { value = new LineLinesControl(2); w = 2; }
            else if (s[0] == '-') { value = new LineLinesControl(4); w = 4; }
            else if (s[0] == '_') {value = new LineLinesControl(8); w = 8; }
        }
    }


    public class LineLinesControl : UserControl
    {
        int we;
        public LineLinesControl(int w)
        {
            we = w;
            Paint += new PaintEventHandler(Paint_L);
        }

        private void Paint_L(object sender, PaintEventArgs e)
        {
            Graphics g = CreateGraphics();
            g.DrawLine(new Pen(Color.Black, (sender as LineLinesControl).we), 0, 3, (sender as LineLinesControl).Width, 3);
        }
    }
}
