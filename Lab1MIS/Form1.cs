using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab1MIS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            
            int n = 200;
            String b = (String)comboBox1.SelectedItem;
          double a = double.Parse(b);

            double[] x = new double[200];

            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                x[i] = (-1.0 / a) * Math.Log(rand.NextDouble());
            }
            Array.Sort(x);

           
            listBox1.Items.Clear();
            for (int i = 0; i < n; i++)
            {
                listBox1.Items.Add(x[i]);
            }

            
            double length = x[n - 1] - x[0];
            int N = 11;
            double intervallength = length / N;

            double[] xintervals = new double[N + 1];
            xintervals[0] = x[0];
            for (int i = 1; i < xintervals.Length; i++)
            {
                xintervals[i] = xintervals[i - 1] + intervallength;
            }

            
            int ch = 0;
            int[] chastota = new int[N+1];
            for (int i = 0; i < N; i++)
            {
                ch = 0;
                for (int j = 0; j < n; j++)
                {
                    if (x[j] > xintervals[i] && x[j] <= xintervals[i + 1])
                    {
                        ch++;
                    }
                }
                chastota[i] = ch; ;
            }
            


           
            listBox2.Items.Clear();
            for (int i = 0; i < N; i++)
            {
                listBox2.Items.Add(i + 1 + ")   " + chastota[i]);
                listBox2.Items.Add(xintervals[i]);

            }


           

          
            double[] heights = new double[N];
            for (int i = 0; i < N ; i++)
            {
                heights[i] = chastota[i] / (n * intervallength);
            }

            
            double[] halfintervals = new double[N];
            halfintervals[0] = x[0] + intervallength / 2.0;
            for (int i = 1; i <N; i++)
            {
                halfintervals[i] = halfintervals[i - 1] + intervallength;
            }

            
            this.chart1.Series.Clear();
            Series series1 = this.chart1.Series.Add("Гистограмма частот");
            Series series2 = this.chart1.Series.Add("Полигон частот");
            chart1.Legends.Clear();
            chart1.ChartAreas[0].AxisX.Enabled = AxisEnabled.False;
            series1.ChartType = SeriesChartType.Column;
            series1.Points.DataBindXY(halfintervals, heights);
            series2.ChartType = SeriesChartType.Line;
            series2.Points.DataBindXY(halfintervals, heights);

            double[] Fx = new double[n];
            for (int i = 0; i < n; i++)
            {
                double q = i;
                Fx[i] = q / n;
            }

            this.chart2.Series.Clear();
            Series series3 = this.chart2.Series.Add("Эмпирическая функция");
            series3.ChartType = SeriesChartType.Line;
            chart2.Legends.Clear();
            chart2.ChartAreas[0].AxisX.Enabled = AxisEnabled.False;
            series3.Points.DataBindXY(x, Fx);

            
            double[] chastotateor = new double[N];
            double p1 = 0;
            double p2 = 0;
            for (int i = 0; i < N; i++)
            {
                p1 = 1.0 - Math.Exp(-a * xintervals[i]);
                p2 = 1.0 - Math.Exp(-a * xintervals[i + 1]);
                chastotateor[i] = (p2 - p1) * n;
                p1 = 0;
                p2 = 0;
            }

            listBox3.Items.Clear();
            for (int i = 0; i <N; i++)
            {
                listBox3.Items.Add(i + 1 + ") " + chastotateor[i]);
            }

            
            double xi2 = 0;
            for (int i = 0; i < N; i++)
            {
                xi2 += Math.Pow((double)chastota[i] - chastotateor[i], 2) / chastotateor[i];

            }
            if (xi2 < 23.2)
            { textBox1.Text = "Критерий Пирсона в пределах нормы, равен:"+xi2+". Гипотеза об экспоненциальном распределении принимается."; }
            else { textBox1.Text = "Критерий Пирсона  равен:" + xi2; }
        }

        
    }
}
