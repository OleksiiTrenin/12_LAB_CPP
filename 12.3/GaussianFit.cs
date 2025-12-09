using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    static class GaussianFit
    {
        static double f(double x, double[] p)
        {
            return
                p[0] * Math.Exp(-(x - p[1]) * (x - p[1]) / (2 * p[2] * p[2])) +
                p[3] * Math.Exp(-(x - p[4]) * (x - p[4]) / (2 * p[5] * p[5]));
        }

        public static void LevenbergMarquardt(double[] x, double[] y, ref double[] p)
        {
            int n = x.Length;
            int m = 6;

            double lambda = 0.001;

            for (int iter = 0; iter < 100; iter++)
            {
                double[,] JtJ = new double[m, m];
                double[] Jtr = new double[m];

                for (int i = 0; i < n; i++)
                {
                    double xi = x[i];
                    double yi = y[i];
                    double fi = f(xi, p);
                    double r = yi - fi;

                    double[] d = new double[m];

                    d[0] = Math.Exp(-(xi - p[1]) * (xi - p[1]) / (2 * p[2] * p[2]));
                    d[1] = p[0] * d[0] * (xi - p[1]) / (p[2] * p[2]);
                    d[2] = p[0] * d[0] * (xi - p[1]) * (xi - p[1]) / (p[2] * p[2] * p[2]);

                    d[3] = Math.Exp(-(xi - p[4]) * (xi - p[4]) / (2 * p[5] * p[5]));
                    d[4] = p[3] * d[3] * (xi - p[4]) / (p[5] * p[5]);
                    d[5] = p[3] * d[3] * (xi - p[4]) * (xi - p[4]) / (p[5] * p[5] * p[5]);

                    for (int a = 0; a < m; a++)
                    {
                        Jtr[a] += d[a] * r;

                        for (int b = 0; b < m; b++)
                            JtJ[a, b] += d[a] * d[b];
                    }
                }

                for (int i = 0; i < m; i++)
                    JtJ[i, i] *= (1 + lambda);

                double[] dp = Solve(JtJ, Jtr);

                for (int i = 0; i < m; i++)
                    p[i] += dp[i];

                if (Norm(dp) < 1e-6)
                    break;
            }
        }

        static double[] Solve(double[,] A, double[] b)
        {
            int n = b.Length;

            for (int i = 0; i < n; i++)
            {
                double v = A[i, i];
                for (int j = i; j < n; j++)
                    A[i, j] /= v;
                b[i] /= v;

                for (int r = 0; r < n; r++)
                {
                    if (r == i) continue;
                    double v2 = A[r, i];
                    for (int c = i; c < n; c++)
                        A[r, c] -= v2 * A[i, c];
                    b[r] -= v2 * b[i];
                }
            }
            return b;
        }

        static double Norm(double[] v)
        {
            double s = 0;
            for (int i = 0; i < v.Length; i++) s += v[i] * v[i];
            return Math.Sqrt(s);
        }
    }
}