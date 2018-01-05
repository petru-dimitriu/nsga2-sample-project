using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return (X - 2 * Y * Math.Sin(Y));
            }
        }

        public double f2
        {
            get
            {
                return Y;
            }
        }


    }

}
