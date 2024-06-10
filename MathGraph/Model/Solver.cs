using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NCalc;

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

        public string Function 
        {
            get => m_MathFunction.Function;
            set => m_MathFunction.Function = value;
        }

        public Solver()
        {
            m_MathFunction = new BaseMathFunction();
            Function = "Pw(x, 2)";
            m_DrawArea = new BaseDrawArea();
            AreaRange = new Vector2(-10, 10);
            Accuracy = 0.001d;
            m_Graph = new BaseGraph();
        }

        public void SolveGraph()
        {
            m_Graph.ClearGraph();
            double acc = m_DrawArea.Accuracy;
            for (double x = m_DrawArea.Range.X; x <= m_DrawArea.Range.Y;)
            {
                double value = m_MathFunction.SolveFunction(x);
                string[]? mc = m_MathFunction.GetMathCondition();
                if(mc == null)
                    m_Graph.AddPoint(new Point(x, value));
                x += acc;
            };
        }

        public List<Point> GetGraph() => m_Graph.Points;
    }
}
