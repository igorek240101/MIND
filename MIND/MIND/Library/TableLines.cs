using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System;

namespace MIND.Library
{
    class TableLines : LinesText
    {
        public int xc, yc;
        public bool?[] loc;
        public SimpleLines[,] cell;

        public TableLines(string s, int st) : base(st)
        {
            string[] array = s.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
            array[1] = array[1].Replace(" ","");
            string[] a = array[1].Split('|');
            int y = array.Length, x = a.Length - 2;
            loc = new bool?[a.Length - 2];
            for(int i = 1; i < a.Length-1; i++)
            {
                if(a[i][0] == ':' && a[i][a[i].Length-1] == ':')
                {
                    loc[i-1] = null;
                }
                else
                {
                    if(a[i][0] == ':')
                    {
                        loc[i-1] = false;
                    }
                    else
                    {
                        if(a[i][a[i].Length - 1] == ':')
                        {
                            loc[i-1] = true;
                        }
                        else
                        {
                            loc[i-1] = false;
                        }
                    }
                }
            }
            SimpleLines[,] inLine = new SimpleLines[y-1,x];
            for(int i = 0, k = 0; i < y; i++, k++)
            {
                if (i == 1) { k--; continue; }
                string[] s_array = array[i].Split('|');
                for(int j = 1; j < x+1; j++)
                {
                    inLine[k, j-1] = new SimpleLines(s_array[j],st);
                }
            }
            y--;
            cell = inLine;
            value = new TableLinesControl(inLine, loc, x, y);
            xc = x;
            yc = y;
        }

        public class TableLinesControl : UserControl
        {
            int[] maxx, maxy;
            public TableLinesControl(SimpleLines[,] value, bool?[] loc, int x, int y)
            {
                maxx = new int[x]; maxy = new int[y];
                for(int i = 0; i < x; i++)
                {
                    for(int j = 0; j < y; j++)
                    {
                        if (maxx[i] < value[j, i].value.Width+20) maxx[i] = value[j, i].value.Width+20;
                    }
                }
                for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
                        if (maxy[i] < value[i, j].value.Height+20) maxy[i] = value[i, j].value.Height+20;
                    }
                }
                int locy = 0;
                for(int i = 0; i < y; i++)
                {
                    int locx = 0;
                    for(int j = 0; j < x; j++)
                    {
                        if(loc[j] == null)
                        {
                            value[i, j].value.Location = new Point((maxx[j] - value[i, j].value.Width)/2+locx, (maxy[i]-value[i, j].value.Height) / 2 + locy);
                        }
                        else
                        {
                            if(loc[j] == true)
                            {
                                value[i, j].value.Location = new Point(maxx[j] - value[i, j].value.Width + locx-10, (maxy[i] - value[i, j].value.Height) / 2 + locy);
                            }
                            else
                            {
                                value[i, j].value.Location = new Point(locx+10, (maxy[i] - value[i, j].value.Height) / 2 + locy);
                            }
                        }
                        Controls.Add(value[i, j].value);
                        locx += maxx[j];
                    }
                    locy += maxy[i];
                }
                Size = new Size(maxx.Sum(), maxy.Sum());
                Paint += new PaintEventHandler(Paint_T);
            }


            private void Paint_T(object sender, PaintEventArgs e)
            {
                Graphics g = CreateGraphics();
                TableLinesControl table = (sender as TableLinesControl);
                int x = table.maxx.Sum(), y = table.maxy.Sum();
                g.DrawLine(new Pen(Color.Black, 4), 2, 0, 2, y);
                int now = 0;
                for (int i = 0; i < table.maxx.Length; i++)
                {
                    g.DrawLine(new Pen(Color.Black, 4), now+maxx[i], 0, now + maxx[i], y);
                    now += maxx[i];
                }
                g.DrawLine(new Pen(Color.Black, 4), now-2, 0, now-2, y);


                now = 0;
                g.DrawLine(new Pen(Color.Black, 4), 0, 2, x, 2);
                for (int i = 0; i < table.maxy.Length; i++)
                {
                    g.DrawLine(new Pen(Color.Black, 4), 0, now + maxy[i], x, now + maxy[i]);
                    now += maxy[i];
                }
                g.DrawLine(new Pen(Color.Black, 4), 0, now-2, x, now-2);
            }
        }
    }
}
