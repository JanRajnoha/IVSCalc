using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Deviation
{
    class Program
    {
        static void Main(string[] args)
        {
            switch (args.Length)
            {
                case 1:
                    if (int.TryParse(args[0], out int DevNumb))
                    {
                        double[] Numbs = new double[DevNumb];
                        Random Rand = new Random();

                        for (int i = 0; i < DevNumb; i++)
                        {
                            Numbs[i] = Rand.Next();
                        }

                        WriteLine(Deviation(Numbs));
                    }
                    else
                    {
                        WriteLine("Bad argument. Insert number");
                        GetHelp();
                    }

                    break;

                case 2:
                    if (args[1].ToUpper() == "-F" || args[1].ToUpper() == "F" || args[1].ToUpper() == "FILE")
                    {
                        var SourceFile = File.ReadAllLines(@args[0]);
                        double[] Numbs = new double[1];

                        foreach (var Numb in File.ReadAllLines(args[0]))
                        {
                            if (double.TryParse(Numb, out Numbs[Numbs.Length - 1]))
                                Array.Resize(ref Numbs, Numbs.Length + 1);
                            else
                            {
                                WriteLine("Bad number");
                            }
                        }

                        WriteLine(Deviation(Numbs));
                    }
                    else
                    {
                        WriteLine("Bad parameters");
                        GetHelp();
                    }
                    break;

                default:
                    GetHelp();
                    break;
            }
        }

        private static void GetHelp()
        {
            WriteLine("Help:");
            WriteLine(" Get random numbers:");
            WriteLine("   Deviation.exe [Count of random numbers]");
            WriteLine("----------------------------------------------");
            WriteLine(" Read numbers from file:");
            WriteLine("   F - read from file");
            WriteLine("   Deviation.exe [File address] -f");

            ReadLine();
        }

        public static double Deviation(double[] numbers)
        {
            #region Deviation
            double sum = 0.0;
            double standardDeviation = 0.0;

            for (int i = 0; i < numbers.Length; ++i)
                sum += numbers[i];

            double average = sum / numbers.Length;

            for (int i = 0; i < numbers.Length; ++i)
                standardDeviation += Pow(numbers[i] - average, 2);

            return Root(standardDeviation / numbers.Length - 1, 2);
            #endregion
        }

        public static double Pow(double x, int n)
        {
            if (x == 1.0)
                return x;

            return System.Math.Round(FuncPow(x, n), 8);
        }

        private static double Root(double x, int n)
        {
            if (x < 0.0)
                return double.NaN;

            if (x == 0.0 || x == 1.0)
                return x;

            return System.Math.Round(FuncPow(x, 1.0 / n), 8);
        }

        private static double FuncPow(double x, double n)
        {
            if (n == 0.0)
                return 1.0;

            else if (n < 0)
                return 1 / FuncPow(x, -n);              // desatinny exponent

            else if (n > 0 && n < 1)
            {
                int temp = (int)(1 / n);
                return NthRoot(x, temp);
            }

            else if ((int)n % 2 == 0)
            {
                double half_pow = FuncPow(x, n / 2);    //integer exponent
                return half_pow * half_pow;
            }

            else
                return x * FuncPow(x, n - 1);
        }

        public static double NthRoot(double x, int n)
        {
            const int K = 20;
            double[] temp = new double[K];
            temp[0] = 1;
            for (int k = 0; k < K - 1; k++)
                temp[k + 1] = (1.0 / n) * ((n - 1) * temp[k] + x / FuncPow(temp[k], n - 1));
            return temp[K - 1];
        }
    }
}
