using System;

namespace ExpressionsCalculation
{
    class Program
    {
        static void Main()
        {
            Console.Write("Enter expression: ");
            string expression = Console.ReadLine();

            var result = ExpressionCalculator.Calculate(expression);
            Console.WriteLine($"Result: {result}");
        }
    }
}
