using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        DrawMNK dm = new DrawMNK();

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonFill_Click(object sender, EventArgs e)
        {
            int ne = int.Parse(textNe.Text);
            dataGridView1.Rows.Clear();
            for (int i = 0; i < ne; i++)
                dataGridView1.Rows.Add();
        }

        private void buttonApprox_Click(object sender, EventArgs e)
        {
            int ne = int.Parse(textNe.Text);
            int ngr = int.Parse(textNgr.Text);
            int m = int.Parse(textM.Text);

           
            for (int i = 0; i < ne; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value == null ||
                    dataGridView1.Rows[i].Cells[1].Value == null ||
                    dataGridView1.Rows[i].Cells[0].Value.ToString() == "" ||
                    dataGridView1.Rows[i].Cells[1].Value.ToString() == "")
                {
                    MessageBox.Show("Фіча! Не заповнено!",
                        "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            double[] xe = new double[ne];
            double[] ye = new double[ne];

            for (int i = 0; i < ne; i++)
            {
                xe[i] = double.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString().Replace(",", "."),
                    System.Globalization.CultureInfo.InvariantCulture);

                ye[i] = double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString().Replace(",", "."),
                    System.Globalization.CultureInfo.InvariantCulture);
            }

            double[] k = new double[m + 1];
            Method_Aprox(xe, ye, ne, ref k, m);

            double minx = xe[0], maxx = xe[0];
            for (int i = 1; i < ne; i++)
            {
                if (xe[i] < minx) minx = xe[i];
                if (xe[i] > maxx) maxx = xe[i];
            }

            double[] xs = new double[ngr];
            double[] ys = new double[ngr];

            double h = (maxx - minx) / (ngr - 1);
            xs[0] = minx;

            for (int i = 0; i < ngr; i++)
            {
                ys[i] = 0;
                for (int j = 0; j <= m; j++)
                    ys[i] += k[j] * Math.Pow(xs[i], j);

                if (i < ngr - 1) xs[i + 1] = xs[i] + h;
            }

            dm.BuildMNK(pictureBox1, xe, ye, xs, ys, ne, ngr);
        }

        bool Method_Aprox(double[] x, double[] y, int n, ref double[] k, int m)
        {
            double[,] A = new double[m + 1, m + 2];

            for (int i = 0; i <= m; i++)
            {
                for (int j = 0; j <= m; j++)
                {
                    double s = 0;
                    for (int t = 0; t < n; t++)
                        s += Math.Pow(x[t], i + j);
                    A[i, j] = s;
                }

                double s2 = 0;
                for (int t = 0; t < n; t++)
                    s2 += y[t] * Math.Pow(x[t], i);
                A[i, m + 1] = s2;
            }

            for (int i = 0; i <= m; i++)
            {
                double v = A[i, i];
                for (int j = i; j <= m + 1; j++)
                    A[i, j] /= v;

                for (int r = 0; r <= m; r++)
                {
                    if (r == i) continue;
                    double v2 = A[r, i];
                    for (int c = i; c <= m + 1; c++)
                        A[r, c] -= v2 * A[i, c];
                }
            }

            for (int i = 0; i <= m; i++)
                k[i] = A[i, m + 1];

            return true;
        }
    }
}