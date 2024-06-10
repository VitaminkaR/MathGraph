using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MathGraph.Model
{
    internal interface IGraph
    {
        public List<Point>? Points { get; set; }
        public void AddPoint(Point val);
        public void RemovePoint(Point val);
        public void ClearGraph();
    }
}
