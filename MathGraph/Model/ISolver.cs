using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MathGraph.Model
{
    internal interface ISolver
    {
        public List<Point> GetGraph();
        public Vector2 AreaRange { get; set; }
        public double Accuracy { get; set; }
        public string Function { get; set; }
        public void SolveGraph();
    }
}
