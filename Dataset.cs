using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSGA2_project
{
    class DatasetRow
    {
        public int daysAway { get; set; }
        public double percent { get; set; }
    }
    class Dataset
    {
        public String productName ;
        public List<DatasetRow> data = new List<DatasetRow>();
    }
}
