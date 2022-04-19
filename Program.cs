using System;

namespace ExpressionsCalculation
{
    class Program
    {
        static void Main()
        {
            Console.Write("Enter expression: ");
            string expression = Console.ReadLine();

            var result = new ExpressionCalculator().Calculate(expression);
            Console.WriteLine($"Result: {result}");
        }
    }
}
