using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class DrawMNK
    {
        double kx, ky, zx, zy;
        int Tx(double x) => (int)(kx * x + zx);
        int Ty(double y) => (int)(ky * y + zy);

        public void BuildMNK(PictureBox pb, double[] xe, double[] ye, double[] xs, double[] ys, int ne, int ngr)
        {
            Graphics g = pb.CreateGraphics();
            g.Clear(Color.White);

            double minx = xs[0], maxx = xs[0];
            double miny = ys[0], maxy = ys[0];

            for (int i = 1; i < ngr; i++)
            {
                if (xs[i] < minx) minx = xs[i];
                if (xs[i] > maxx) maxx = xs[i];
                if (ys[i] < miny) miny = ys[i];
                if (ys[i] > maxy) maxy = ys[i];
            }

            for (int i = 0; i < ne; i++)
            {
                if (xe[i] < minx) minx = xe[i];
                if (xe[i] > maxx) maxx = xe[i];
                if (ye[i] < miny) miny = ye[i];
                if (ye[i] > maxy) maxy = ye[i];
            }

            kx = (pb.Width - 20) / (maxx - minx);
            ky = (pb.Height - 20) / (miny - maxy);
            zx = (pb.Width * minx - 10 * (minx + maxx)) / (minx - maxx);
            zy = (pb.Height * maxy - 10 * (miny + maxy)) / (maxy - miny);





            Pen grid = new Pen(Color.LightGray);
            grid.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            for (int i = 0; i < pb.Width; i += 40)
                g.DrawLine(grid, i, 0, i, pb.Height);

            for (int i = 0; i < pb.Height; i += 40)
                g.DrawLine(grid, 0, i, pb.Width, i);

            Pen ax = new Pen(Color.Black, 2);
            g.DrawLine(ax, Tx(0), 0, Tx(0), pb.Height);
            g.DrawLine(ax, 0, Ty(0), pb.Width, Ty(0));

            Pen pExp = new Pen(Color.Red, 2);
            for (int i = 0; i < ne; i++)
                g.DrawRectangle(pExp, Tx(xe[i]) - 3, Ty(ye[i]) - 3, 6, 6);

            Pen pApr = new Pen(Color.Red, 2);
            for (int i = 1; i < ngr; i++)
                g.DrawLine(pApr, Tx(xs[i - 1]), Ty(ys[i - 1]), Tx(xs[i]), Ty(ys[i]));
        }
    }
}