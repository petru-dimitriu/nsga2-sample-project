using System;

namespace NSGA2_project
{
    class Datapoint
    {  

        public double X { get; set ; }

        public double Y { get; set; }

        public double crowdingDistance { get; set; }

        public double f1
        {
            get
            {
                return (X);
            }
        }

        public double f2
        {
            get
            {
                return (1+Y)/X;
            }
        }


    }

}
