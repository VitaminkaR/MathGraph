using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MathGraph.Model
{
    internal class Solver : ISolver
    {
        private IMathFunction? m_MathFunction;
        private IDrawArea? m_DrawArea;
        private IGraph? m_Graph;

        public double GetAccuracy()
        {
            throw new NotImplementedException();
        }

        public List<Vector2> GetGraph()
        {
            throw new NotImplementedException();
        }

        public Vector2 GetRange()
        {
            throw new NotImplementedException();
        }

        public void SetAccuracy(double val)
        {
            throw new NotImplementedException();
        }

        public void SetFunction(string f)
        {
            throw new NotImplementedException();
        }

        public void SetRange(Vector2 vec)
        {
            throw new NotImplementedException();
        }
    }
}
