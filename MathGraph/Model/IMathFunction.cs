using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGraph.Model
{
    internal interface IMathFunction
    {
        public string Function { get; set; }
        public double SolveFunction(double x);
        // возвращает мат условие (например если при подсчете функции было подсчитано комплексное число (должно быть возвращена информация об ошибке))
        public string[]? GetMathCondition();
    }
}
