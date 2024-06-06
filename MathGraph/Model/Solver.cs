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
        private IMathFunction m_MathFunction;
        private IDrawArea m_DrawArea;
        private IGraph m_Graph;

        public double Accuracy 
        {
            get => m_DrawArea.Accuracy;
            set => m_DrawArea.Accuracy = value;
        }

        public Vector2 AreaRange
        {
            get => m_DrawArea.Range;
            set => m_DrawArea.Range = value;
        }

        public Solver()
        {
            m_MathFunction = new BaseMathFunction();
            m_DrawArea = new BaseDrawArea();
            m_DrawArea.Range = new Vector2(-10, 10);
            m_DrawArea.Accuracy = 0.001d;
            m_Graph = new BaseGraph();
        }

        public List<double> GetGraph() => m_Graph.Points;

        public void SetFunction(string f) => m_MathFunction.SetFunction = f;
    }
}
