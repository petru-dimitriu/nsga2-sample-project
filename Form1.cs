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
    public partial class Form1 : Form
    {
        List<Datapoint> dataset;
        Graphics chartGraphics;
        List<List<Datapoint>> paretoFrontierPointsList;
        long maxDaysAway;

        public Form1()
        {
            dataset = new List<Datapoint>();
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

                Datapoint datasetRow = new Datapoint { productName = name, daysAfter = 0, percent = 1 };
                dataset.Add(datasetRow);
                listView1.Items.Add(new ListViewItem(new string[] { name, "0", "1" }));

                while (!sr.EndOfStream)
                {
                    String s = sr.ReadLine();
                    numbers = s.Split(' ');

                    DateTime date = new DateTime(int.Parse(numbers[2]), int.Parse(numbers[1]), int.Parse(numbers[0]));
                    double currentPrice = double.Parse(numbers[3]);

                    Datapoint newRow = new Datapoint { productName = name, daysAfter = (int)(date - initialDate).TotalDays, percent = currentPrice / initialPrice };
                    dataset.Add(newRow);

                    if (newRow.daysAfter > maxDaysAway)
                        maxDaysAway = newRow.daysAfter;
                }
                updateChart();
                computeParetoFrontier();
                reloadListView();
            }
        }

        private void reloadListView()
        {
            listView1.Items.Clear();
            foreach (Datapoint row in dataset)
            {
                listView1.Items.Add((new ListViewItem(new string[] { row.productName, row.daysAfter.ToString(), row.percent.ToString() })));
            }
        }

        private void updateChart()
        {
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

            Color[] colors = new Color[] { Color.Red, Color.Green, Color.Purple, Color.Orange, Color.Blue };
            int color = 0;
            foreach (List<Datapoint> currentFront in paretoFrontierPointsList)
            {
                Datapoint previous = null;
                foreach (Datapoint row in currentFront)
                {
                    int xPos = getXPosForPoint(row.daysAfter);
                    int yPos = getYPosForPoint(row.percent);
                    chartGraphics.DrawRectangle(new Pen(colors[color%colors.Length], 5), new Rectangle(xPos - 3, yPos - 3, 5, 5));
                    if (previous != null)
                    {
                        chartGraphics.DrawLine(new Pen(colors[color%colors.Length], 2),
                            new Point { X = getXPosForPoint(previous.daysAfter), Y = getYPosForPoint(previous.percent) },
                            new Point { X = getXPosForPoint(row.daysAfter), Y = getYPosForPoint(row.percent) });
                    }
                    previous = row;
                }
                color++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listviewItem in listView1.SelectedItems)
            {
                dataset.RemoveAt(listviewItem.Index);
                listView1.Items.Remove(listviewItem);
            }
            computeParetoFrontier();
            updateChart();
        }

        private void computeParetoFrontier()
        {
            
            // 1st step: sort points by X axis
            dataset.Sort(new DatasetRowComparer());
            List<Datapoint> auxList = new List<Datapoint>();
            auxList.AddRange(dataset);

            paretoFrontierPointsList = new List<List<Datapoint>>();

            while (auxList.Count != 0)
            {
                List<Datapoint> currentFrontierList = new List<Datapoint>();
                currentFrontierList.Add(auxList[0]);
                Datapoint previous = auxList[0];

                foreach (Datapoint row in auxList)
                {
                    if (previous.daysAfter <= row.daysAfter && previous.percent >= row.percent)
                    {
                        currentFrontierList.Add(row);
                        previous = row;
                    }
                }
                auxList.RemoveAll(x => currentFrontierList.Contains(x));
                paretoFrontierPointsList.Add(currentFrontierList);
            }
        }
    }

    class DatasetRowComparer : IComparer<Datapoint>
    {
        public int Compare(Datapoint x, Datapoint y)
        {
            return x.daysAfter - y.daysAfter;
        }
    }
}