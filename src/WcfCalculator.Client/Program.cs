using System;
using System.ServiceModel;
using WcfCalculator.Client.ServiceReferences;
using static System.Console;

namespace WcfCalculator.Client
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                WriteLine("Press any key to invoke WCF service...");
                ReadKey(true);

                var random = new Random();
                var n1 = random.Next(100);
                var n2 = random.Next(100 - n1);

                //var result = AddWithCalculatorClient(n1, n2);
                //var result = AddWithChannelFactory(n1, n2);
                var result = AddWithCustomChannelFactory(n1, n2);

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

        private static double AddWithChannelFactory(double n1, double n2)
        {
            using (var factory = new ChannelFactory<ICalculator>("BasicHttpBinding_ICalculator"))
            {
                var channel = factory.CreateChannel();
                return channel.Add(n1, n2);
            }
        }

        private static double AddWithCustomChannelFactory(double n1, double n2)
        {
            using (var factory = new CustomChannelFactory<ICalculator>("BasicHttpBinding_ICalculator"))
            {
                var channel = factory.CreateChannel();
                return channel.Add(n1, n2);
            }
        }
    }
}
