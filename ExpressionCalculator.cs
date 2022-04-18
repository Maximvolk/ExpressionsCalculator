using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionsCalculation
{
    class ExpressionCalculator
    {
        // Calculate expression using Bauer - Zamelson algorithm and Inverse Polish notation
        static double Calculate(string expression)
        {
            var expressionList = new List<object>();


            ConvertToInversePolishNotation(expressionList);

            return ComputeInversePolishExpression(expressionList);
        }

        private static List<object> ConvertToInversePolishNotation(List<object> expression)
        {

        }

        private static double ComputeInversePolishExpression(List<object> expression)
        {

        }
    }
}
