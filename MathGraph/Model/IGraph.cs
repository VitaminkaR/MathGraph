using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGraph.Model
{
    internal interface IGraph
    {
        public List<double>? Points { get; set; }
        public void AddPoint(double val);
        public void RemovePoint(double val);
    }
}
