using NCalc;
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
                m_Expression.Parameters["pi"] = Math.PI;
                m_Expression.Parameters["e"] = Math.E;
                m_Expression.EvaluateFunction += delegate (string name, FunctionArgs args)
                {
                    // возведение в степень (встроенная функция возведения вызывает ошибки)
                    if (name == "Pw")
                    {
                        double arg1 = Convert.ToDouble(args.Parameters[0].Evaluate());
                        double arg2 = Convert.ToDouble(args.Parameters[1].Evaluate());
                        double res = Math.Pow(arg1, arg2);
                        if (double.IsNaN(res) || res > 9999999999999999 || res < -9999999999999999)
                        {
                            res = 0;
                            m_MathCondition = new string[1] { "pwfuncerr" };
                        }
                        args.Result = res;
                    }  
                };

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
