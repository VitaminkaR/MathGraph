using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGraph.Model
{
    internal class BaseGraph : IGraph
    {
        protected List<double>? m_Points;

        public List<double>? Values
        { 
            get => m_Points;
            set => m_Points = value; 
        }

        public void AddPoint(double val)
        {
            throw new NotImplementedException();
        }

        public void RemovePoint(double val)
        {
            throw new NotImplementedException();
        }
    }
}
