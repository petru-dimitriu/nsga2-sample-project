using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace NSGA2_project
{
    class DatasetRowComparer : IComparer<DatasetRow>
    {
        public int Compare(DatasetRow x, DatasetRow y)
        {
            return x.daysAfter - y.daysAfter;
        }
    }
    public partial class Form1 : Form
    {
        List<DatasetRow> dataset;
        Graphics chartGraphics;
        List<DatasetRow> paretoFrontierPointsList;
        long maxDaysAway;

        public Form1()
        {
            dataset = new List<DatasetRow>();
            InitializeComponent();
            chartGraphics = panel1.CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(dialog.FileName);
                String name = sr.ReadLine(); // numele produsului
                String firstRow = sr.ReadLine();
                String[] numbers = firstRow.Split(' ');

                DateTime initialDate = new DateTime(int.Parse(numbers[2]), int.Parse(numbers[1]), int.Parse(numbers[0]));
                double initialPrice = double.Parse(numbers[3]);

                DatasetRow datasetRow = new DatasetRow { productName = name, daysAfter = 0, percent = 1 };
                dataset.Add(datasetRow);
                listView1.Items.Add(new ListViewItem(new string[] { name, "0", "1" }));

                while (!sr.EndOfStream)
                {
                    String s = sr.ReadLine();
                    numbers = s.Split(' ');

                    DateTime date = new DateTime(int.Parse(numbers[2]), int.Parse(numbers[1]), int.Parse(numbers[0]));
                    double currentPrice = double.Parse(numbers[3]);

                    DatasetRow newRow = new DatasetRow { productName = name, daysAfter = (int)(date - initialDate).TotalDays, percent = currentPrice / initialPrice };
                    dataset.Add(newRow);

                    listView1.Items.Add(new ListViewItem(new string[] { name, newRow.daysAfter.ToString(), newRow.percent.ToString() }));

                    if (newRow.daysAfter > maxDaysAway)
                        maxDaysAway = newRow.daysAfter;
                }
                updateChart();
            }
        }

        private void updateChart()
        {
            computeParetoFrontier();
            panel1.Invalidate();
        }

        private int getXPosForPoint (int daysAfter)
        {
            return (int)(((daysAfter + 0.0) / maxDaysAway) * (panel1.Width - 10));
        }

        private int getYPosForPoint (double percent)
        {
            return (int)((1 - percent) * (panel1.Height - 10));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Color pointColor;
            chartGraphics.Clear(Color.White);
            foreach (DatasetRow row in dataset)
            {
                int xPos = getXPosForPoint(row.daysAfter);
                int yPos = getYPosForPoint(row.percent);

                pointColor = Color.Blue;

                chartGraphics.DrawEllipse(new Pen(pointColor,5), new Rectangle(xPos - 3, yPos - 3, 5, 5));
            }
            DatasetRow previous = null;
            foreach (DatasetRow row in paretoFrontierPointsList)
            {
                if (previous != null)
                {
                    chartGraphics.DrawLine(new Pen(Color.Red, 2),
                        new Point { X = getXPosForPoint(previous.daysAfter), Y = getYPosForPoint(previous.percent) },
                        new Point { X = getXPosForPoint(row.daysAfter), Y = getYPosForPoint(row.percent) });
                }
                previous = row;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listviewItem in listView1.SelectedItems)
            {
                dataset.RemoveAt(listviewItem.Index);
                listView1.Items.Remove(listviewItem);
            }
            updateChart();
        }

        private void computeParetoFrontier()
        {
            // 1st step: sort points by X axis
            dataset.Sort(new DatasetRowComparer());
            paretoFrontierPointsList = new List<DatasetRow>();
            paretoFrontierPointsList.Add(dataset[0]);
            DatasetRow previous = dataset[0];
            foreach (DatasetRow row in dataset)
            {
                if (previous.daysAfter <= row.daysAfter && previous.percent >= row.percent)
                {
                    paretoFrontierPointsList.Add(row);
                    previous = row;
                }
            }
        }
    }
}