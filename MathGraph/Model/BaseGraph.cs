using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGraph.Model
{
    internal class BaseGraph : IGraph
    {
        protected List<double> m_Points;

        public List<double> Points
        { 
            get => m_Points;
            set => m_Points = value; 
        }

        public BaseGraph() 
        { 
            Points = new List<double>();
        }

        public void AddPoint(double val) => m_Points.Add(val);

        public void ClearGraph() => m_Points = new List<double>();

        public void RemovePoint(double val) => m_Points.Remove(val);
    }
}
