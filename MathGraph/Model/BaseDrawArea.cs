using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MathGraph.Model
{
    internal class BaseDrawArea : IDrawArea
    {
        private Vector2 m_Range;
        private double m_Accuracy;

        public Vector2 Range
        {
            get => m_Range;
            set => m_Range = value;
        }

        public double Accuracy
        {
            get => m_Accuracy;
            set => m_Accuracy = value; 
        }
    }
}
