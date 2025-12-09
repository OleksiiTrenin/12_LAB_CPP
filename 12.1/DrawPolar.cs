using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class DrawPolar
    {
        double kx, ky, zx, zy;

        double Cardi(double phi, double a)
        {
            return a * (1 + Math.Cos(phi));
        }

        double Rose(double phi, double k)
        {
            return Math.Cos(k * phi);
        }

        double Hyp(double phi, double a)
        {
            return a / phi;
        }

        double Arch(double phi, double a)
        {
            return a * phi;
        }

        int Tx(double x) => (int)(kx * x + zx);
        int Ty(double y) => (int)(ky * y + zy);

        public void Build(PictureBox pb, double L, double R, int N, int type, double par)
        {
            Graphics g = pb.CreateGraphics();
            g.Clear(Color.White);

            double[] x = new double[N];
            double[] y = new double[N];

            double h = (R - L) / (N - 1);

            for (int i = 0; i < N; i++)
            {
                double phi = L + i * h;
                double r = 0;

                if (type == 0) r = Cardi(phi, par);
                if (type == 1) r = Rose(phi, par);
                if (type == 2) r = Hyp(phi, par);
                if (type == 3) r = Arch(phi, par);

                x[i] = r * Math.Cos(phi);
                y[i] = r * Math.Sin(phi);
            }

            double minx = x[0], maxx = x[0];
            double miny = y[0], maxy = y[0];

            for (int i = 1; i < N; i++)
            {
                if (x[i] < minx) minx = x[i];
                if (x[i] > maxx) maxx = x[i];
                if (y[i] < miny) miny = y[i];
                if (y[i] > maxy) maxy = y[i];
            }

            kx = (pb.Width - 20) / (maxx - minx);
            ky = (pb.Height - 20) / (miny - maxy);
            zx = (pb.Width * minx - 10 * (minx + maxx)) / (minx - maxx);
            zy = (pb.Height * maxy - 10 * (miny + maxy)) / (maxy - miny);



            Pen grid = new Pen(Color.LightGreen);
            grid.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            for (int i = 0; i < pb.Width; i += 30)
                g.DrawLine(grid, i, 0, i, pb.Height);

            for (int i = 0; i < pb.Height; i += 30)
                g.DrawLine(grid, 0, i, pb.Width, i);

            Pen ax = new Pen(Color.Red, 2);
            g.DrawLine(ax, Tx(0), 0, Tx(0), pb.Height);
            g.DrawLine(ax, 0, Ty(0), pb.Width, Ty(0));

            Pen p = new Pen(Color.Blue, 2);

            for (int i = 1; i < N; i++)
                g.DrawLine(p, Tx(x[i - 1]), Ty(y[i - 1]), Tx(x[i]), Ty(y[i]));
        }
    }
}