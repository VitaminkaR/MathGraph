using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGraph.Model
{
    internal interface IMathFunction
    {
        public int SetFunction { get; set; }
        public double SolveFunction();
    }
}
