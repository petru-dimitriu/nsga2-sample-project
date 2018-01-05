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
        List<List<Datapoint>> paretoFrontierPointsList = new List<List<Datapoint>>();
        int populationSize = 30;
        Random rand = new Random();
        double maxX = 0, maxY = 0;
        double crossoverRate = 0.2;
        double mutationRate = 0.1;
        int iterations = 50;

        public Form1()
        {
            dataset = new List<Datapoint>();
            InitializeComponent();
            chartGraphics = panel1.CreateGraphics();
        }

      

        private void updateChart()
        {
            panel1.Invalidate();
        }

        private int getXPosForPoint (double daysAfter)
        {
            return (int)(((daysAfter + 0.0) / maxX) * (panel1.Width - 10));
        }

        private int getYPosForPoint (double percent)
        {
            return (int)((percent / maxY) * (panel1.Height - 10));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            chartGraphics.Clear(Color.White);

            Color[] colors = new Color[] { Color.Red, Color.Green, Color.Purple, Color.Orange, Color.Blue };
            int color = 0;
            foreach (List<Datapoint> currentFront in paretoFrontierPointsList)
            {
                Datapoint previous = null;
                foreach (Datapoint row in currentFront)
                {
                    int xPos = getXPosForPoint(row.f1);
                    int yPos = getYPosForPoint(row.f2);
                    chartGraphics.DrawRectangle(new Pen(colors[color%colors.Length], 3), new Rectangle(xPos - 1, yPos - 1, 2, 2));
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

        // alege primele n/2 puncte in ordinea sortarii
        private List<Datapoint> selectParentsByRank()
        {
            List<Datapoint> selected = new List<Datapoint>();
            foreach (List<Datapoint> front in paretoFrontierPointsList)
            {
                if (selected.Count + front.Count <= populationSize / 2)
                    selected.AddRange(front);
                else {
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

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listviewItem in listView1.SelectedItems)
            {
                dataset.RemoveAt(listviewItem.Index);
                listView1.Items.Remove(listviewItem);
            }
            computeParetoFrontierList();
            updateChart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            for (int i = 0; i < populationSize; i++)
            {
                Datapoint newDatapoint = new Datapoint { X = random.NextDouble() * 50 + 100, Y = random.NextDouble() * 50 };
                if (newDatapoint.f1 > maxX)
                    maxX = newDatapoint.f1;
                if (newDatapoint.f2 > maxY)
                    maxY = newDatapoint.f2;
                dataset.Add(newDatapoint);
                listView1.Items.Add(new ListViewItem(new string[] { newDatapoint.X + "", newDatapoint.Y + "", newDatapoint.f1 + "", newDatapoint.f2 + "" }));
            }
            updateChart();
            computeParetoFrontierList();
        }

        private Datapoint crossOver(Datapoint mother, Datapoint father, double rate)
        {
            // dam cu banul daca facem incrucisarea sau copiem un parinte
            double randomNumber = rand.NextDouble();
            if (randomNumber <= rate)
            {
                // incrucisare
                randomNumber = rand.NextDouble();
                Datapoint newChromosome = new Datapoint { X = mother.X, Y = mother.Y } ;
                // cele doua gene sunt X si Y
                newChromosome.X = mother.X * randomNumber + father.X * (1 - randomNumber);
                newChromosome.Y = mother.Y * randomNumber + father.Y * (1 - randomNumber);
                return newChromosome;
            }
            else
                return father;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            int i = 0;
            List<Datapoint> selected = selectParentsByRank();
            // incrucisare
            List<Datapoint> children = new List<Datapoint>();
            for (i = 0; i < selected.Count - 1; i++)
                children.Add(crossOver(selected[i], selected[i + 1], crossoverRate));
            children.Add(crossOver(selected[selected.Count - 1], selected[0], crossoverRate));

            // mutatii
            foreach(Datapoint datapoint in children)
            {
                mutate(datapoint, mutationRate);
            }

            for (i = 0; i < iterations; i++)
            {
                // union
                dataset.RemoveRange(populationSize/2, populationSize / 2);
                dataset.AddRange(children);
                computeParetoFrontierList(); // calculeaza in currentParetoFrontierList; DE SCHIMBAT; de facut elegant
                List<Datapoint> parents = new List<Datapoint>();
                int frontL = 0;
                foreach (List<Datapoint> front in paretoFrontierPointsList)
                {
                    computeCrowdingDistance(front);
                    if (parents.Count + front.Count > populationSize)
                        break;
                    else
                        parents.AddRange(front);
                    frontL++;
                }
                if (parents.Count < populationSize)
                {
                    paretoFrontierPointsList[frontL].Sort(new DistanceComparer());
                    // mai adauga datapoints pana cand se umple parents
                    foreach(Datapoint datapoint in paretoFrontierPointsList[frontL])
                    {
                        parents.Add(datapoint);
                        if (parents.Count == populationSize)
                            break;
                    }
                }
                selected.Clear();
                // selectez jumatatea superioara
                selected.AddRange(parents);
                selected.RemoveRange(populationSize / 2, populationSize / 2);
                dataset = children;

                //crossover
                for (i = 0; i < selected.Count - 1; i++)
                    children.Add(crossOver(selected[i], selected[i + 1], crossoverRate));
                children.Add(crossOver(selected[selected.Count - 1], selected[0], crossoverRate));

                // mutatii
                foreach (Datapoint datapoint in children)
                {
                    mutate(datapoint, mutationRate);
                }
            }
            // final
            dataset = children;
            updateChart();
        }

        private void mutate(Datapoint child, double rate)
        {
            double randomNumber = rand.NextDouble();
            if (rand.NextDouble() <= rate)
            {
                // reseteaza!
                child.X = rand.NextDouble() * 50 + 100;
            }
            if (rand.NextDouble() <= rate)
            {
                // reseteaza!
                child.Y = rand.NextDouble() * 50 + 100;
            }
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



        private void computeParetoFrontierList()
        {
            // 1st step: sort points by X axis
            dataset.Sort(new XAxisWiseComparer());
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
                    if (previous.f1 < row.f1 && previous.f2 < row.f2)
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
    }

    class XAxisWiseComparer : IComparer<Datapoint>
    {
        public int Compare(Datapoint x, Datapoint y)
        {
            return (int)(x.f1 - y.f1);
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
