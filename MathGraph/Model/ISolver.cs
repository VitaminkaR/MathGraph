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
        public List<Vector2> GetGraph();
        public void SetRange(Vector2 vec);
        public Vector2 GetRange();
        public void SetAccuracy(double val);
        public double GetAccuracy();
    }
}
