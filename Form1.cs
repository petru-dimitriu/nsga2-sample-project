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
        List<Dataset> datasetList;
        Graphics chartGraphics;
        long maxDaysAway;
        
        public Form1()
        {
            datasetList = new List<Dataset>();
            InitializeComponent();
            chartGraphics = panel1.CreateGraphics();
        }

        private void reloadListbox()
        {
            listBox1.Items.Clear();
            foreach (Dataset d in datasetList)
            {
                listBox1.Items.Add(d.productName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(dialog.FileName);
                Dataset dataset = new Dataset();
                dataset.productName = sr.ReadLine(); // numele produsului
                String firstRow = sr.ReadLine();
                String[] numbers = firstRow.Split(' ');

                DateTime initialDate = new DateTime(int.Parse(numbers[2]), int.Parse(numbers[1]), int.Parse(numbers[0]));
                double initialPrice = double.Parse(numbers[3]);

                DatasetRow datasetRow = new DatasetRow { daysAway = 0, percent = 1 };
                datasetRow.daysAway = 0;
                datasetRow.percent = 1;
                dataset.data.Add(datasetRow);

                while (!sr.EndOfStream)
                {
                    String s = sr.ReadLine();
                    numbers = s.Split(' ');

                    DateTime date = new DateTime(int.Parse(numbers[2]), int.Parse(numbers[1]), int.Parse(numbers[0]));
                    double currentPrice = double.Parse(numbers[3]);

                    DatasetRow newRow = new DatasetRow();
                    newRow.daysAway = (int)(date - initialDate).TotalDays;
                    newRow.percent = currentPrice / initialPrice;
                    dataset.data.Add(newRow);

                    if (newRow.daysAway > maxDaysAway)
                    {
                        maxDaysAway = newRow.daysAway;
                    }
                }
                datasetList.Add(dataset);
                reloadListbox();
                drawChart();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            loadDataset(index);
        }

        private void loadDataset(int index)
        {
            foreach (DatasetRow row in datasetList[index].data)
            {
                ListViewItem lvi = new ListViewItem(row.daysAway.ToString());
                lvi.SubItems.Add(row.percent.ToString());
                listView1.Items.Add(lvi);
            }
            listView1.Refresh();
        }

        private void drawChart()
        {
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            chartGraphics.Clear(Color.White);
            foreach (DatasetRow row in datasetList[0].data)
            {
                int yPos = (int)((1 - row.percent) * (panel1.Height - 10));
                int xPos = (int)(((row.daysAway + 0.0) / maxDaysAway) * (panel1.Width - 10));
                MessageBox.Show(xPos + " " + yPos);
                chartGraphics.DrawEllipse(new Pen(Color.Blue,5), new Rectangle(xPos - 3, yPos - 3, 5, 5));
            }
        }
    }
    }
