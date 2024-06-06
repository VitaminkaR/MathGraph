using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MathGraph.Model
{
    internal interface IDrawArea
    {
        public Vector2 Range { get; set; }
        public double Accuracy { get; set; }
    }
}
