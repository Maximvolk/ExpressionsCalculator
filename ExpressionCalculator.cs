using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionsCalculation
{
    public class ExpressionCalculator
    {
        // Calculate expression using Bauer - Zamelson algorithm and Inverse Polish notation
        public static double Calculate(string expression)
        {
            var expressionItems = SplitExpression(expression);

            
            return 0;
        }

        private static List<object> SplitExpression(string expression)
        {
            var operators = new List<char> { '(', ')', '+', '-', '*', '/' };
            var floatSeparators = new List<char> { ',', '.' };

            var expressionItems = new List<object>();
            object currentNumber = null;
            bool floatingPointOccurred = false;

            foreach (var symbol in expression)
            {
                if (operators.Contains(symbol))
                {
                    if (currentNumber != null)
                        expressionItems.Add(currentNumber);
                    
                    expressionItems.Add(symbol);

                    floatingPointOccurred = false;
                    currentNumber = null;
                }
                else if (char.IsDigit(symbol))
                {
                    // Case when symbol is probably a part of floating point number
                    if (floatingPointOccurred)
                        currentNumber = double.Parse($"{currentNumber}.{symbol}");
                    else
                        currentNumber = double.Parse($"{currentNumber}{symbol}");
                    
                    floatingPointOccurred = false;
                }
                else if (floatSeparators.Contains(symbol))
                    floatingPointOccurred = true;

                else if (symbol != ' ')
                    throw new Exception($"Invalid symbol occurred: {symbol}");
            }

            // Do not forget last item if it exists
            if (currentNumber != null)
                expressionItems.Add(double.Parse(currentNumber.ToString()));

            return expressionItems;
        }
    }
}
