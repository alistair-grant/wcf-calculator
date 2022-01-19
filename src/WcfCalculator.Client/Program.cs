using System;
using WcfCalculator.Client.ServiceReferences;
using static System.Console;

namespace WcfCalculator.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                WriteLine("Press any key to invoke WCF service...");
                ReadKey(true);

                var random = new Random();
                var n1 = random.Next(50);
                var n2 = random.Next(50);

                var result = AddWithCalculatorClient(n1, n2);

                WriteLine($"{n1} + {n2} = {result}");
            }
            catch (Exception exception)
            {
                var originalColor = ForegroundColor;
                ForegroundColor = ConsoleColor.Red;

                Error.WriteLine(exception.Message);
                Error.WriteLine(exception.StackTrace);

                ForegroundColor = originalColor;
            }

            WriteLine();
            WriteLine("Press any key to exit...");
            ReadKey(true);
        }

        private static double AddWithCalculatorClient(double n1, double n2)
        {
            using (var client = new CalculatorClient())
            {
                return client.Add(n1, n2);
            }
        }
    }
}
