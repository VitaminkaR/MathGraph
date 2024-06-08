using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MathGraph.Model
{
    internal class BaseMathFunction : IMathFunction
    {
        // представляет прямой ввод функции от пользователя
        private string m_RawFunction = "";
        // представляет обработанное для вычислителя представление (строка) функции 
        private string m_Function = "";

        private string[]? m_MathCondition;

        private NCalc.Expression m_Expression;

        public string Function
        { 
            get => m_RawFunction;
            set
            {
                m_Function = Parse(value);
                m_Expression = new NCalc.Expression(m_Function);
                m_RawFunction = value;
            } 
        }

        public string[]? GetMathCondition()
        {
            string[]? mc = m_MathCondition;
            m_MathCondition = null;
            return mc;
        }

        public double SolveFunction(double x)
        {
            m_Expression.Parameters["x"] = x;
            return (double)Convert.ToDecimal(m_Expression.Evaluate());
        }

        private string Parse(string input)
        {
            return input;
        }
    }
}
