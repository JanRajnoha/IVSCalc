using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVSCalc
{
    public class Math
    {
        /// <summary>
        /// Funkce pro součet dvou čísel
        /// </summary>
        /// <param name="x"> První Sčítanec</param>
        /// <param name="y"> Druhý Sčítanec</param>
        /// <returns> Součet x a y </returns>
        public static double Addition(double x, double y)
        {
            int RoundInt = DecimalParser(x) + DecimalParser(y) > 15 ? 15 : DecimalParser(x) + DecimalParser(y);
            return System.Math.Round(x + y, RoundInt);
        }

        /// <summary>
        /// Funkce pro výpočet rozdílu dvou čísel
        /// </summary>
        /// <param name="x"> Menšenec</param>
        /// <param name="y"> Menšitel</param>
        /// <returns> Rozdíl čísel</returns>
        public static double Substraction(double x, double y)
        {
            int RoundInt = DecimalParser(x) + DecimalParser(y) > 15 ? 15 : DecimalParser(x) + DecimalParser(y);
            return System.Math.Round(x - y, RoundInt);
        }

        /// <summary>
        /// Funkce pro výpočet součinu dvou čísel
        /// </summary>
        /// <param name="x">První činitel</param>
        /// <param name="y">Druhý činitel</param>
        /// <returns>Součin činitělů</returns>
        public static double Multiplication(double x, double y)
        {
            int RoundInt = DecimalParser(x) + DecimalParser(y) > 15 ? 15 : DecimalParser(x) + DecimalParser(y);
            return System.Math.Round(x * y, RoundInt);
        }

        /// <summary>
        /// Funkce pro výpočet podílu dvou čísel
        /// </summary>
        /// <param name="x">Dělenec</param>
        /// <param name="y">Dělitel</param>
        /// <returns>Podíl Dělence a dělitele</returns>
        public static double Division(double x, double y)
        {
            if (y == 0.0)
            {
                return double.NaN;
            }

            return System.Math.Round(x / y, 8);
        }

        /// <summary>
        /// Funkce pro výpočet faktoriálu daného čísla
        /// </summary>
        /// Faktoriál je součin všech kladných celých čísel menší nebo rovných x
        /// <param name="x"></param>
        /// <returns>Faktoriál čísla</returns>
        public static double Factorial(double x)
        {
            if (x < 0.0)
            {
                return double.NaN;
            }

            if (x - (int)x == 0.0)
                return System.Math.Round(FuncFactorial(x), 8);
            else
                return double.NaN;
        }

        /// <summary>
        /// Pomocná funkce pro funkci Factorial(double x)
        /// </summary>
        /// <param name="x"></param>
        /// <returns>Faktoriál čísla</returns>
        private static double FuncFactorial(double x)
        {
            if (x == 0.0)
            {
                return 1.0;
            }
            return x * FuncFactorial(x - 1);
        }

        /// <summary>
        /// Volající funkce pro výpočet n-té mocniny
        /// </summary>
        /// <param name="x">Umocňované číslo</param>
        /// <param name="n">Mocnina</param>
        /// <returns>N-tá mocnina čísla</returns>
        public static double Pow(double x, int n)
        {
            if (x == 1.0)
                return x;

            return System.Math.Round(FuncPow(x, n), 8);
        }

        /// <summary>
        /// Volající funkce pro výpočet n-té odmocniny
        /// </summary>
        /// <param name="x">Odmocňované číslo</param>
        /// <param name="n">Odmocina</param>
        /// <returns>N-tá odmocnina čísla</returns>
        public static double Root(double x, int n)
        {
            if (x < 0.0)
                return double.NaN;

            if (x == 0.0 || x == 1.0)
                return x;

            return System.Math.Round(FuncPow(x, 1.0 / n), 8);
        }

        /// <summary>
        /// Pomocná funkce pro výpočet odmocniny
        /// </summary>
        /// <param name="x">Odmocňované číslo</param>
        /// <param name="n">Odmocnina</param>
        /// <returns>N-tá odmocnina z x</returns>
        private static double NthRoot(double x, int n)
        {
            double pom;     //pomocna premenna optimalizacie poctu iteracii
            int digit_num_x = DigitCount(x);
            int digit_num_n = DigitCount(n);

            //optimalizacia presnosti/poctu iteracii
            pom = Pow(digit_num_x, 3)*(Pow(n, 2)*(1/Pow(10, digit_num_n))) + 20;
            
            //n-th root algoritmus
            int K = (int)pom;
            double[] temp = new double[K];
            temp[0] = 1;
            for (int k = 0; k < K - 1; k++)
                temp[k + 1] = (1.0 / n) * ((n - 1) * temp[k] + x / FuncPow(temp[k], n - 1));
            return temp[K - 1];
        }

        /// <summary>
        /// Funkce pro výpočet mocniny/odmocniny
        /// </summary>
        /// <param name="x">Umocňované číslo</param>
        /// <param name="n">Mocnina</param>
        /// <returns>N-tá mocnina čísla x</returns>
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

        /// <summary>
        /// Funkce pro výpočet přirozeného logaritmu
        /// </summary>
        /// <param name="x">Logaritmované číslo</param>
        /// <returns>Přirozený logaritmus čísla x</returns>
        private static double Ln(double x)
        {
            double mocnina = 1.0;   // pomocne premenne pri vypocte podla vzorca
            double temp_a = 0.0;   
            double temp_b = 0.0;
            double cf = 1.0;        // premenna pre zretazenie zlomkov
            int n = 0;              //pocet iteracii
            int rounded_x = (int)x;

            //optimalizacia presnosti/poctu iteracii
            if (x > 1.0)
            {
                int digit_num = DigitCount(x);
                while (digit_num != 0)
                {
                    n += (int) (15 * Pow(digit_num, digit_num - 2));
                    digit_num--;
                }
            }
            else if (x > 0.5)
                n = 10;
            else if (x > 0.1)
                n = 15;
            else
            {
                int zero_num = ZeroCount(x);
                if (zero_num < 4)
                    while (zero_num != 0)
                    {
                        n += 50 * zero_num;
                        zero_num--;
                    }
                else if (zero_num < 5)
                    n = (int)Pow(zero_num + 2, zero_num);
                else if (zero_num < 6)
                    n = (int)Pow(zero_num + 1, zero_num);
                else if (zero_num < 7)
                    n = (int)Pow(zero_num - 1, zero_num);
                else
                    n = (int)Pow(zero_num - 1, zero_num - 1);
            }
            
            //pocitanie zretazenych zlomkov
            //ln((1+z)/(1-z)) =  2z/(1-z^2)/(3-4z^2)/(5-9z^2)/...

            x = (1 - x) / (-1 - x);

            while (n > 0)
            {
                mocnina = n * n;
                temp_b = mocnina * (x * x);
                temp_a = ((n * 2) - 1);
                cf = temp_a - (temp_b / cf);
                n--;
            }

            return ((2 * x) / cf);
        }

        /// <summary>
        /// Funkce pro výpočet logaritmu o základu 10
        /// </summary>
        /// <param name="x">Logaritmované číslo</param>
        /// <returns>Logaritmus čísla x</returns>
        public static double Log(double x)
        {
            if (x == 1.0)
                return 0.0;

            if (x <= 0.0)
                return double.NaN;

            double temp = 10.0;

            return System.Math.Round(Ln(x) / Ln(temp), 8);
        }

        /// <summary>
        /// Funkce pro výpočet směrodatnej odchylky
        /// </summary>
        /// <param name="numbers">Pole hodnot</param>
        /// <returns>Směrodatna odchylka</returns>
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

        /// <summary>
        /// Parser for decimal numbers and their count.
        /// Return 0, if numbers are whole.
        /// </summary>
        /// <example> 
        /// Example of number rounding with Parser precision
        /// <code>
        ///     System.Math.Round(1.123 * 5.21, Parser(1.123)+Parser(5.21)))
        /// </code>
        /// </example>
        /// <param name="Num">Number</param>
        /// <returns>Count of decimal places of numbers</returns>
        private static int DecimalParser(double Num)
        {
            var ParsedNumber = Num.ToString().Split('.');

            if (ParsedNumber.Length > 1)
            {
                return ParsedNumber[1].Length > 15 ? 15 : ParsedNumber[1].Length;
            }
            return 0;
        }

        
        /// <summary>
        /// Funkce pro vypocet poctu nul za desetinou carkou
        /// </summary>
        /// <param name="Num"></param>
        /// <returns>Pocet nul za desetinou carkou</returns>
        private static int ZeroCount(double Num)
        {
            var ParsedNumber = Num.ToString().Split('.');
            int count = 0;

            if (ParsedNumber.Length > 1)
            {
                for (int i = 0; i < ParsedNumber[1].Length; i++)
                {
                    if (ParsedNumber[1][i] == '0')
                        count++;
                    else
                        break;
                }
            }
            return count;
        }

        
        /// <summary>
        /// Funkce pro výpočet počtu číslic před desetinnou čárkou
        /// </summary>
        /// <param name="Num"></param>
        /// <returns>Počet číslic před desetinnou čárkou</returns>
        private static int DigitCount(double Num)
        {
            var ParsedNumber = Num.ToString().Split('.');
            return ParsedNumber[0].Length;
        }  
    }
}

