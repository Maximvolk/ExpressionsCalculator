using System;
using System.Collections.Generic;

namespace ExpressionsCalculation
{
    public class ExpressionsCalculator
    {
        private Dictionary<char, int> _operatorToIndexMap = new Dictionary<char, int>
        {
            { '(', 1 },
            { '+', 2 },
            { '-', 3 },
            { '*', 4 },
            { '/', 5 },
            { ')', 6 },
        };
        private int[,] _actionsTable = new int[,]
        {
            { 6, 1, 1, 1, 1, 1, 5 },
            { 5, 1, 1, 1, 1, 1, 3 },
            { 4, 1, 2, 2, 1, 1, 4 },
            { 4, 1, 2, 2, 1, 1, 4 },
            { 4, 1, 4, 4, 2, 2, 4 },
            { 4, 1, 4, 4, 2, 2, 4 }
        };

        // Calculate expression using Bauer - Zamelzon algorithm
        public double Calculate(string expression)
        {
            var expressionTokens = SplitExpression(expression);

            var t = new Stack<char>();
            var e = new Stack<double>();

            foreach (var token in expressionTokens)
            {
                if (token is double)
                {
                    e.Push((double)token);
                    continue;
                }

                var inputOperatorIndex = _operatorToIndexMap[(char)token];
                var stackOperatorIndex = t.IsEmpty() ? 0 : _operatorToIndexMap[t.Peek()];
                var actionIndex = _actionsTable[stackOperatorIndex, inputOperatorIndex];

                ExecuteActionByIndex(actionIndex, token, t, e);
            }

            while (!t.IsEmpty())
            {
                var stackOperatorIndex = t.IsEmpty() ? 0 : _operatorToIndexMap[t.Peek()];
                var actionIndex = _actionsTable[stackOperatorIndex, 0];
                ExecuteActionByIndex(actionIndex, null, t, e);
            }

            return e.Pop();
        }

        private List<object> SplitExpression(string expression)
        {
            var operators = new List<char> { '(', ')', '+', '-', '*', '/' };
            var floatSeparators = new List<char> { ',', '.' };

            var expressionItems = new List<object>();
            object currentNumber = null;
            bool floatingPointOccurred = false;

            // If expression starts with negative number add 0 (e.g. -3-5 becomes 0-3-5)
            if (expression.StartsWith("-"))
                expression = $"0{expression}";

            // Add zero before each negative number in the beginning of subexpression (e.g. 1-(-1) becomes 1-0-1)
            expression = expression.Replace("(-", "(0-");

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
                        currentNumber = double.Parse($"{currentNumber},{symbol}");
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

        // Algorithm has functions table where each function is assigned a number from 1 to 5 (6 is exit)
        private void ExecuteActionByIndex(int index, object token, Stack<char> t, Stack<double> e)
        {
            if (index == 1)
                t.Push((char)token);

            else if (index == 2)
            {
                e.Push(ComputeArithmeticExpression(t, e));
                t.Push((char)token);
            }

            else if (index == 3)
                t.Pop();

            else if (index == 4)
            {
                e.Push(ComputeArithmeticExpression(t, e));

                var stackOperatorIndex = t.IsEmpty() ? 0 : _operatorToIndexMap[t.Peek()];
                var inputOperatorIndex = token == null ? 0 : _operatorToIndexMap[(char)token];
                var actionIndex = _actionsTable[stackOperatorIndex, inputOperatorIndex];
                ExecuteActionByIndex(actionIndex, token, t, e);
            }

            else if (index == 5)
                throw new Exception("Invalid expression");
        }

        private double ComputeArithmeticExpression(Stack<char> t, Stack<double> e)
        {
            var rightOperand = e.Pop();
            var leftOperand = e.Pop();

            return t.Pop() switch
            {
                '+' => leftOperand + rightOperand,
                '-' => leftOperand - rightOperand,
                '*' => leftOperand * rightOperand,
                '/' => leftOperand / rightOperand,
                _ => throw new Exception("Invalid operator")
            };
        }
    }
}
