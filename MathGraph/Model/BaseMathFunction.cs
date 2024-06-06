using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGraph.Model
{
    internal class BaseMathFunction : IMathFunction
    {
        public string? m_Function;

        public int SetFunction
            { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }

        public double SolveFunction()
        {
            throw new NotImplementedException();
        }
    }
}
