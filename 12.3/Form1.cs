using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        DrawGaussian dg = new DrawGaussian();

        public Form1()
        {
            InitializeComponent();
            GenerateData();
        }

        void GenerateData()
        {
            int ne = int.Parse(textNe.Text);
            dataGridView1.Rows.Clear();

            Random r = new Random();
            for (int i = 0; i < ne; i++)
            { 
                double x = 90 + i * 1.2;
                double y =
                    70 * Math.Exp(-(x - 110) * (x - 110) / (2 * 6 * 6)) +
                    85 * Math.Exp(-(x - 135) * (x - 135) / (2 * 8 * 8)) +
                    (r.NextDouble() * 6 - 3);

                dataGridView1.Rows.Add(x, y);
            }
        }

        private void buttonApprox_Click(object sender, EventArgs e)
        {
            int ne = int.Parse(textNe.Text);
            int ngr = int.Parse(textNgr.Text);

            double[] xe = new double[ne];
            double[] ye = new double[ne];

            for (int i = 0; i < ne; i++)
            {
                xe[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);
                ye[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value);
            }

            double[] p = new double[] { 70, 110, 6, 85, 135, 8 };

            GaussianFit.LevenbergMarquardt(xe, ye, ref p);

            textA1.Text = p[0].ToString("F3");
            textMu1.Text = p[1].ToString("F3");
            textS1.Text = p[2].ToString("F3");

            textA2.Text = p[3].ToString("F3");
            textMu2.Text = p[4].ToString("F3");
            textS2.Text = p[5].ToString("F3");

            double[] xs = new double[ngr];
            double[] ys = new double[ngr];
            double minx = xe[0];
            double maxx = xe[ne - 1];
            double h = (maxx - minx) / (ngr - 1);

            xs[0] = minx;
            for (int i = 0; i < ngr; i++)
            {
                double x = xs[i];

                ys[i] =
                    p[0] * Math.Exp(-(x - p[1]) * (x - p[1]) / (2 * p[2] * p[2])) +
                    p[3] * Math.Exp(-(x - p[4]) * (x - p[4]) / (2 * p[5] * p[5]));

                if (i < ngr - 1) xs[i + 1] = xs[i] + h;
            }

            dg.Draw(pictureBox1, xe, ye, xs, ys);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textNe_TextChanged(object sender, EventArgs e)
        {

        }
    }
}