using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MathGraph.Model
{
    internal interface ISolver
    {
        public void SetFunction(string f);
        public List<double> GetGraph();
        public Vector2 AreaRange { get; set; }
        public double Accuracy { get; set; }
    }
}
