using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        DrawPolar dp = new DrawPolar();

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonBuild_Click(object sender, EventArgs e)
        {
           
            if (comboType.SelectedIndex == -1)
            {
                MessageBox.Show("Будь ласка, оберіть метод зі списку.", "Фіча!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            double L, R, p;
            int N;

            if (!double.TryParse(textL.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out L))
            {
                MessageBox.Show("Некоректне значення L.", "Помилка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!double.TryParse(textR.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out R))
            {
                MessageBox.Show("Некоректне значення R.", "Помилка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textN.Text, out N))
            {
                MessageBox.Show("Некоректне значення N.", "Помилка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!double.TryParse(textParam.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out p))
            {
                MessageBox.Show("Некоректне значення параметра.", "Помилка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int type = comboType.SelectedIndex;

            dp.Build(pictureBox1, L, R, N, type, p);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboType.SelectedIndex == 3)
            {
                textParam.Visible = false;
                labelParam.Visible = false;
            }
            else
            {
                textParam.Visible = true;
                labelParam.Visible = true;
            }
        }
    }
}