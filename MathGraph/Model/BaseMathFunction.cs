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

        public string Function
        { 
            get => m_RawFunction;
            set
            {
                m_Function = Parse(value);
                m_RawFunction = value;
            } 
        }

        public double SolveFunction(double x)
        {
            NCalc.Expression expression = new NCalc.Expression(m_Function);
            expression.Parameters["x"] = x;
            return (double)Convert.ToDecimal(expression.Evaluate());
        }

        private string Parse(string input)
        {
            return input;
        }
    }
}
