using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MathGraph.Model
{
    internal class BaseGraph : IGraph
    {
        protected List<Point> m_Points;

        public List<Point> Points
        { 
            get => m_Points;
            set => m_Points = value; 
        }

        public BaseGraph() 
        { 
            Points = new List<Point>();
        }

        public void AddPoint(Point val) => m_Points.Add(val);

        public void ClearGraph() => m_Points = new List<Point>();

        public void RemovePoint(Point val) => m_Points.Remove(val);
    }
}
