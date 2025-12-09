using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class DrawGaussian
    {
        double kx, ky, zx, zy;

        int Tx(double x) => (int)(kx * x + zx);
        int Ty(double y) => (int)(ky * y + zy);

        public void Draw(PictureBox pb, double[] xe, double[] ye, double[] xs, double[] ys)
        {
            Graphics g = pb.CreateGraphics();
            g.Clear(Color.White);

            double minx = xs[0], maxx = xs[0];
            double miny = ys[0], maxy = ys[0];

            for (int i = 1; i < xs.Length; i++)
            {
                if (xs[i] < minx) minx = xs[i];
                if (xs[i] > maxx) maxx = xs[i];
                if (ys[i] < miny) miny = ys[i];
                if (ys[i] > maxy) maxy = ys[i];
            }

            for (int i = 0; i < xe.Length; i++)
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

            Pen pExp = new Pen(Color.Blue, 2);
            for (int i = 0; i < xe.Length; i++)
                g.DrawRectangle(pExp, Tx(xe[i]) - 3, Ty(ye[i]) - 3, 6, 6);

            Pen p = new Pen(Color.Red, 2);
            for (int i = 1; i < xs.Length; i++)
                g.DrawLine(p, Tx(xs[i - 1]), Ty(ys[i - 1]), Tx(xs[i]), Ty(ys[i]));
        }
    }
}
