using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NSGA2_project
{
    public partial class Form1 : Form
    {
        List<Datapoint> dataset;
        Graphics chartGraphics;
        List<List<Datapoint>> paretoFrontierPointsList = new List<List<Datapoint>>();
        Random rand = new Random();

        int panelMaxX;
        int panelMaxY;
       
        //domeniul din care X si Y iau valori
        int XMinDomain;
        int XMaxDomain;
        int YMinDomain;
        int YMaxDomain;

        int populationSize;
        double crossoverRate;
        double mutationRate;
        int iterations;

        public Form1()
        {
            dataset = new List<Datapoint>();
            InitializeComponent();
            chartGraphics = panel1.CreateGraphics();
            panelMaxX = panel1.Width;
            panelMaxY = panel1.Height;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            chartGraphics.Clear(Color.White);
            initializeFields();
            drawXYAxis();
            Color[] colors = new Color[] { Color.Red, Color.Green, Color.Purple, Color.Orange, Color.Blue };
            int color = 0;
            foreach (List<Datapoint> currentFront in paretoFrontierPointsList)
            {
                Datapoint previous = null;
                foreach (Datapoint row in currentFront)
                {
                    int xPos = getXPosForPoint(row.f1);
                    int yPos = getYPosForPoint(row.f2);
                    chartGraphics.DrawRectangle(new Pen(colors[color%colors.Length], 5), new Rectangle(xPos - 1, yPos - 1, 1, 1));
                    if (previous != null)
                    {
                        chartGraphics.DrawLine(new Pen(colors[color%colors.Length], 1),
                            new Point { X = getXPosForPoint(previous.f1), Y = getYPosForPoint(previous.f2) },
                            new Point { X = getXPosForPoint(row.f1), Y = getYPosForPoint(row.f2) });
                    }
                    previous = row;
                }
                color++;
            }
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            initializeFields();
            Random random = new Random();
            for (int i = 0; i < populationSize; i++)
            {
                Datapoint newDatapoint = new Datapoint { X = random.NextDouble() * (XMaxDomain - XMinDomain) + XMinDomain, Y = random.NextDouble() * (YMaxDomain - YMinDomain) + YMinDomain };
                dataset.Add(newDatapoint);
                listView1.Items.Add(new ListViewItem(new string[] { newDatapoint.X + "", newDatapoint.Y + "", newDatapoint.f1 + "", newDatapoint.f2 + "" }));
            }
            updateChart();
            computeParetoFrontierList(dataset);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dataset.Count == 0)
            {
                //initializeFields();
                //button1_Click(sender, e);
            }
            else
            {
                int i = 0;
                List<Datapoint> selected = selectParentsByRank();
                // incrucisare
                List<Datapoint> children = new List<Datapoint>();
                for (i = 0; i < selected.Count - 1; i++)
                    children.Add(crossOver(selected[i], selected[i + 1], crossoverRate));
                children.Add(crossOver(selected[selected.Count - 1], selected[0], crossoverRate));

                // mutatii
                foreach (Datapoint datapoint in children)
                {
                    mutate(datapoint, mutationRate);
                }

                for (i = 0; i < iterations; i++)
                {
                    // union
                    List<Datapoint> union = new List<Datapoint>();
                    union.AddRange(dataset);
                    union.RemoveRange(union.Count / 2, union.Count / 2);
                    union.AddRange(children);
                    computeParetoFrontierList(union); // calculeaza in currentParetoFrontierList; DE SCHIMBAT; de facut elegant
                    List<Datapoint> parents = new List<Datapoint>();
                    int frontL = 0;
                    foreach (List<Datapoint> front in paretoFrontierPointsList)
                    {
                        computeCrowdingDistance(front);
                        if (parents.Count + front.Count > populationSize)
                        {
                            frontL = paretoFrontierPointsList.IndexOf(front);
                            break;
                        }
                        else
                        {
                            parents.RemoveRange(parents.Count / 2, parents.Count / 2);
                            parents.AddRange(front);
                        }
                    }
                    if (parents.Count < populationSize)
                    {
                        paretoFrontierPointsList[frontL].Sort(new DistanceComparer());
                        // mai adauga datapoints pana cand se umple parents
                        foreach (Datapoint datapoint in paretoFrontierPointsList[frontL])
                        {
                            parents.Add(datapoint);
                            if (parents.Count == populationSize)
                                break;
                        }
                    }
                    selected.Clear();
                    // selectez jumatatea superioara
                    selected.AddRange(parents);
                    selected.RemoveRange(selected.Count / 2, selected.Count / 2);

                    dataset.Clear(); //depinde daca prin linia Population <- children din pseudocod se intelege populatie = populatie+children sau populatie = children
                    dataset.AddRange(children);

                    //crossover
                    for (int j = 0; j < selected.Count - 1; j++)
                        children.Add(crossOver(selected[j], selected[j + 1], crossoverRate));
                    children.Add(crossOver(selected[selected.Count - 1], selected[0], crossoverRate));

                    // mutatii
                    foreach (Datapoint datapoint in children)
                        mutate(datapoint, mutationRate);
                }
                // final
                dataset.AddRange(children);
                //dataset = dataset.Distinct().ToList(); // ?
                computeParetoFrontierList(dataset);
                panel1.Invalidate(); // declanseaza desenarea
                updateListViewFromList(dataset.Distinct().OrderBy(x => x.f1).ToList());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataset.RemoveRange(0, dataset.Count);
            chartGraphics.Clear(Color.White);
            drawXYAxis();
            listView1.Items.Clear();
        }

        private void updateListViewFromList(List<Datapoint> ListToBeWritten)
        {
            listView1.Items.Clear();
            //listView1.Items.Add(new ListViewItem(new string[] { "" + "", "" + "", "" + "", "" + "" }));
            for (int i = 0; i < ListToBeWritten.Count; i++)
                listView1.Items.Add(new ListViewItem(new string[] { ListToBeWritten[i].X + "", ListToBeWritten[i].Y + "", ListToBeWritten[i].f1 + "", ListToBeWritten[i].f2 + "" }));
        }

        private void initializeFields()
        {
            Int32.TryParse(tb_minX.Text, out XMinDomain);
            Int32.TryParse(tb_maxX.Text, out XMaxDomain);
            Int32.TryParse(tb_minY.Text, out YMinDomain);
            Int32.TryParse(tb_maxY.Text, out YMaxDomain);

            Int32.TryParse(tb_populationSize.Text, out populationSize);
            Double.TryParse(tb_crossoverRate.Text, out crossoverRate);
            Double.TryParse(tb_mutationRate.Text, out mutationRate);
            Int32.TryParse(tb_noOfIterations.Text, out iterations);
        }
        

        private void updateChart()
        {
            panel1.Invalidate();
        }

        private int getXPosForPoint(double point_X)
        {
            return (int)(((double)(point_X) - (double)(XMinDomain)) * ((panelMaxX+0.0) / (( (XMaxDomain+0.0) - (XMinDomain+0.0)))));
        }

        private int getYPosForPoint(double point_Y)
        {
            return panelMaxY - (int)(((double)(point_Y) - (double)(YMinDomain)) * ((panelMaxY + 0.0) / (((YMaxDomain + 0.0) - (YMinDomain + 0.0)))));
        }

        private void drawXYAxis()
        {
            int xOrigin = 0;
            int yOrigin = panel1.Height - 1;

            int yHighest = panel1.Height;
            int xHighest = panel1.Width;

            chartGraphics.DrawLine(new Pen(Color.Black, 1),
                           new Point { X = xOrigin, Y = yOrigin },
                           new Point { X = xOrigin, Y = 1 });


            chartGraphics.DrawLine(new Pen(Color.Black, 1),
                           new Point { X = xOrigin, Y = yOrigin },
                           new Point { X = xHighest, Y = yOrigin });
        }

        // alege primele n/2 puncte in ordinea sortarii
        private List<Datapoint> selectParentsByRank()
        {
            List<Datapoint> selected = new List<Datapoint>();
            foreach (List<Datapoint> front in paretoFrontierPointsList)
            {
                if (selected.Count + front.Count <= populationSize / 2)
                    selected.AddRange(front);
                else
                {
                    foreach (Datapoint datapoint in front)
                    {
                        selected.Add(datapoint);
                        if (selected.Count == populationSize / 2)
                            break;
                    }
                    break;
                }
            }
            return selected;
        }

        private void computeCrowdingDistance(List<Datapoint> frontier)
        {
            frontier[0].crowdingDistance = double.PositiveInfinity;
            for (int i = 1; i < frontier.Count -1; i++)
            {
                frontier[i].crowdingDistance = frontier[i + 1].f2 - frontier[i - 1].f2 + frontier[i - 1].f1 - frontier[i - 1].f2;
            }
            frontier[frontier.Count - 1].crowdingDistance = double.PositiveInfinity;
        }

        private void computeParetoFrontierList(List<Datapoint> datasetList)
        {
            // 1st step: sort points by X axis
            datasetList.Sort(new XAxisWiseComparer());
            List<Datapoint> auxList = new List<Datapoint>();
            auxList.AddRange(datasetList);

            paretoFrontierPointsList = new List<List<Datapoint>>();

            while (auxList.Count != 0)
            {
                List<Datapoint> currentFrontierList = new List<Datapoint>();
                currentFrontierList.Add(auxList[0]);
                Datapoint previous = auxList[0];

                foreach (Datapoint row in auxList)
                {
                    if (previous.f1 <= row.f1 && previous.f2 >= row.f2)
                    {
                        currentFrontierList.Add(row);
                        previous = row;
                    }
                }
                auxList.RemoveAll(x => currentFrontierList.Contains(x));
                auxList.Sort(new XAxisWiseComparer());
                paretoFrontierPointsList.Add(currentFrontierList);
            }
        }


        private Datapoint crossOver(Datapoint mother, Datapoint father, double rate)
        {
            var randomNumber = rand.NextDouble();

            if (rate < randomNumber)
                if (randomNumber < 0.5)
                    return mother;
                else
                    return father;
            else
            {
                double alpha = rand.NextDouble();
                Datapoint newChromosome = new Datapoint { X = mother.X, Y = mother.Y };

                newChromosome.X = mother.X * alpha + father.X * (1 - randomNumber);
                newChromosome.Y = mother.Y * alpha + father.Y * (1 - randomNumber);


                return newChromosome;
            }
        }

        private void mutate(Datapoint child, double rate)
        {
            double randomNumber = rand.NextDouble();
            if (rand.NextDouble() <= rate)
            {
                // reseteaza!
                child.X = rand.NextDouble() * (XMaxDomain - XMinDomain) + XMinDomain;
            }
            if (rand.NextDouble() <= rate)
            {
                // reseteaza!
                child.Y = rand.NextDouble() * (YMaxDomain - YMinDomain) + YMinDomain;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            initializeFields();
            panel1.Invalidate();
        }
    }

    class XAxisWiseComparer : IComparer<Datapoint>
    {
        public int Compare(Datapoint x, Datapoint y)
        {
            return (int)(x.f1 * 10.0 - y.f1 *10.0);
        }
    }

    class DistanceComparer : IComparer<Datapoint>
    {
        public int Compare(Datapoint x, Datapoint y)
        {
            return (int)(x.crowdingDistance - y.crowdingDistance);
        }
    }

}
