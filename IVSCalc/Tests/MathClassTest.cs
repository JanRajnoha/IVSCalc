
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class MathClassTest
    {
        [TestMethod]
        public void AdditionTest()
        {
            Assert.IsTrue(IVSCalc.Math.Addition(10, 10) == 20);
            Assert.IsTrue(IVSCalc.Math.Addition(0, 10) == 10);
            Assert.IsTrue(IVSCalc.Math.Addition(0, 0) == 0);
            Assert.IsTrue(IVSCalc.Math.Addition(-0, -0) == 0);
            Assert.IsTrue(IVSCalc.Math.Addition(0, -0) == 0);
            Assert.IsTrue(IVSCalc.Math.Addition(-10, 10) == 0);
            Assert.IsTrue(IVSCalc.Math.Addition(10, -10) == 0);
            Assert.IsTrue(IVSCalc.Math.Addition(-10, -10) == -20);
            Assert.IsTrue(IVSCalc.Math.Addition(0.5, 0.75) == 1.25);
            Assert.IsTrue(IVSCalc.Math.Addition(1, -0.5) == 0.5);
            Assert.IsTrue(IVSCalc.Math.Addition(2000.5, -1000.5) == 1000);
            Assert.IsFalse(IVSCalc.Math.Addition(0.1, 0.1) == 0);
            Assert.IsFalse(IVSCalc.Math.Addition(-5, -5) == 0);
            Assert.IsFalse(IVSCalc.Math.Addition(100, 100) == 300);
            Assert.IsFalse(IVSCalc.Math.Addition(-100, 0.1) == 0);
            Assert.IsFalse(IVSCalc.Math.Addition(0.5, 0.5) == 0.5);
        }

        [TestMethod]
        public void SubstractionTest()
        {
            Assert.IsTrue(IVSCalc.Math.Substraction(10, 10) == 0);
            Assert.IsTrue(IVSCalc.Math.Substraction(0, 10) == -10);
            Assert.IsTrue(IVSCalc.Math.Substraction(10, 0) == 10);
            Assert.IsTrue(IVSCalc.Math.Substraction(5, -50) == 55);
            Assert.IsTrue(IVSCalc.Math.Substraction(5, 50) == -45);
            Assert.IsTrue(IVSCalc.Math.Substraction(100, 10) == 90);
            Assert.IsTrue(IVSCalc.Math.Substraction(10.652, 5.251) == 5.401);
            Assert.IsTrue(IVSCalc.Math.Substraction(5.0000005, 3.00000003) == 2.00000047);
            Assert.IsTrue(IVSCalc.Math.Substraction(10, -10) == 20);
            Assert.IsTrue(IVSCalc.Math.Substraction(1, 0.3333) == 0.6667);
            Assert.IsTrue(IVSCalc.Math.Substraction(-0.5, -0.5) == 0);
            Assert.IsTrue(IVSCalc.Math.Substraction(150, -999.999999) == 1149.999999);    
        }

        [TestMethod]
        public void MultiplicationTest()
        {
            Assert.IsTrue(IVSCalc.Math.Multiplication(0, 0) == 0);
            Assert.IsTrue(IVSCalc.Math.Multiplication(1, 1) == 1);
            Assert.IsTrue(IVSCalc.Math.Multiplication(3, 3) == 9);
            Assert.IsTrue(IVSCalc.Math.Multiplication(0, 1) == 0);
            Assert.IsTrue(IVSCalc.Math.Multiplication(1, 0) == 0);
            Assert.IsTrue(IVSCalc.Math.Multiplication(-5, 0) == 0);
            Assert.IsTrue(IVSCalc.Math.Multiplication(0, -5) == 0);
            Assert.IsTrue(IVSCalc.Math.Multiplication(-5, -5) == 25);
            Assert.IsTrue(IVSCalc.Math.Multiplication(0.1, 0.1) == 0.01);
            Assert.IsTrue(IVSCalc.Math.Multiplication(0.5, 2) == 1);
            Assert.IsTrue(IVSCalc.Math.Multiplication(10, -0.1) == -1);
            Assert.IsTrue(IVSCalc.Math.Multiplication(0.001, 0.001) == 0.000001);
            Assert.IsFalse(IVSCalc.Math.Multiplication(100, 0) != 0);
            Assert.IsFalse(IVSCalc.Math.Multiplication(100, 100) == 100);
            Assert.IsFalse(IVSCalc.Math.Multiplication(-50, -50) == -2500);
        }

        [TestMethod]
        public void DivisionTest()
        {
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Division(0, 0)));
            Assert.IsTrue(IVSCalc.Math.Division(0, 1) == 0);
            Assert.IsTrue(IVSCalc.Math.Division(5, 1) == 5);
            Assert.IsTrue(IVSCalc.Math.Division(5, 5) == 1);
            Assert.IsTrue(IVSCalc.Math.Division(5, -5) == -1);
            Assert.IsTrue(IVSCalc.Math.Division(-100, -20) == 5);
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Division(10, 0)));
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Division(-10, 0)));
            Assert.IsTrue(IVSCalc.Math.Division(1000, 100) == 10);
            Assert.IsTrue(IVSCalc.Math.Division(50, 70) == 0.71428571); 
            Assert.IsTrue(IVSCalc.Math.Division(-100, 33) == -3.03030303);
            Assert.IsTrue(IVSCalc.Math.Division(-80, -20) == 4);
            Assert.IsTrue(IVSCalc.Math.Division(150, 900) == 0.16666667);
            Assert.IsTrue(IVSCalc.Math.Division(150, 10000) == 0.015);
            Assert.IsTrue(IVSCalc.Math.Division(150, 1000000000) == 0.00000015);
            Assert.IsTrue(IVSCalc.Math.Division(150, 10000000000) == 0.00000001);
        }

        [TestMethod]
        public void RootTest()
        {
            Assert.IsTrue(IVSCalc.Math.Root(0, 2) == 0);
            Assert.IsTrue(IVSCalc.Math.Root(1, 2) == 1);
            Assert.IsTrue(IVSCalc.Math.Root(2, 2) == 1.41421356);
            Assert.IsTrue(IVSCalc.Math.Root(4, 2) == 2);
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Root(-5, 2)));
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Root(-1000, 2)));
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Root(-1, 2)));

            Assert.IsTrue(IVSCalc.Math.Root(10.55, 2) == 3.24807635);
            Assert.IsTrue(IVSCalc.Math.Root(20.4999, 2) == 4.52768153);
            Assert.IsTrue(IVSCalc.Math.Root(50.990099, 2) == 7.14073519);
            Assert.IsTrue(IVSCalc.Math.Root(100.55554444, 2) == 10.02773875);
            Assert.IsTrue(IVSCalc.Math.Root(100.123456789101112, 2) == 10.00617094);
            Assert.IsTrue(IVSCalc.Math.Root(100.123456789, 2) == 10.00617094);

            Assert.IsFalse(IVSCalc.Math.Root(-1, 2) == 1);
            Assert.IsFalse(IVSCalc.Math.Root(-4, 2) == -2);
            Assert.IsFalse(IVSCalc.Math.Root(-16, 2) == -4);
            Assert.IsFalse(double.IsNaN(IVSCalc.Math.Root(0, 2)));
            Assert.IsFalse(IVSCalc.Math.Root(-150, 2) >= 0);
            Assert.IsFalse(IVSCalc.Math.Root(-10000, 2) == 100);

            Assert.IsTrue(IVSCalc.Math.Root(10, 1) == 10);
            Assert.IsTrue(IVSCalc.Math.Root(27, 3) == 3);
            Assert.IsTrue(IVSCalc.Math.Root(100, 3) == 4.64158883);
            Assert.IsTrue(IVSCalc.Math.Root(100, 4) == 3.16227766);
            Assert.IsTrue(IVSCalc.Math.Root(10, 5) == 1.58489319);
            Assert.IsTrue(IVSCalc.Math.Root(256, 8) == 2);
            Assert.IsTrue(IVSCalc.Math.Root(1073741824, 30) == 2);
            Assert.IsTrue(IVSCalc.Math.Root(625, 15) == 1.53600278);

            Assert.IsTrue(IVSCalc.Math.Root(1, 5) == 1);
            Assert.IsTrue(IVSCalc.Math.Root(1, 7) == 1);
            Assert.IsTrue(IVSCalc.Math.Root(1, 9) == 1);
            Assert.IsTrue(IVSCalc.Math.Root(1, 16) == 1);
            Assert.IsTrue(IVSCalc.Math.Root(1, 17) == 1);
            Assert.IsTrue(IVSCalc.Math.Root(1, 25) == 1);

            Assert.IsTrue(IVSCalc.Math.Root(0, 5) == 0);
            Assert.IsTrue(IVSCalc.Math.Root(0, 7) == 0);
            Assert.IsTrue(IVSCalc.Math.Root(0, 9) == 0);
            Assert.IsTrue(IVSCalc.Math.Root(0, 16) == 0);
            Assert.IsTrue(IVSCalc.Math.Root(0, 17) == 0);
            Assert.IsTrue(IVSCalc.Math.Root(0, 25) == 0);

            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Root(-5, 3)));
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Root(-50, 4)));
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Root(-70, 5)));
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Root(-150, 6)));
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Root(-500, 10)));
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Root(-1250, 12)));
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Root(-144, 16)));
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Root(-2, 20)));
        }

        [TestMethod]
        public void PowTest()
        {
            Assert.IsTrue(IVSCalc.Math.Pow(2, 3) == 8);
            Assert.IsTrue(IVSCalc.Math.Pow(1, 1) == 1);
            Assert.IsTrue(IVSCalc.Math.Pow(1, 0) == 1);
            Assert.IsTrue(IVSCalc.Math.Pow(100, 0) == 1);
            Assert.IsTrue(IVSCalc.Math.Pow(-100, 0) == 1);
            Assert.IsTrue(IVSCalc.Math.Pow(1.0201231, 0) == 1);
            Assert.IsTrue(IVSCalc.Math.Pow(15e9, 0) == 1);
            Assert.IsTrue(IVSCalc.Math.Pow(0, 0) == 1);
            Assert.IsTrue(IVSCalc.Math.Pow(0, 5) == 0);
            Assert.IsTrue(IVSCalc.Math.Pow(0, 100) == 0);

            Assert.IsTrue(IVSCalc.Math.Pow(2, 5) == 32);
            Assert.IsTrue(IVSCalc.Math.Pow(2, 15) == 32768);
            Assert.IsTrue(IVSCalc.Math.Pow(1e5, 2) == 1e10);
            Assert.IsTrue(IVSCalc.Math.Pow(-2, 3) == -8);
            Assert.IsTrue(IVSCalc.Math.Pow(-1500, 6) == 1.1390625E+19);
            Assert.IsTrue(IVSCalc.Math.Pow(-99999, 9) == -9.99910003599916E+44);
            Assert.IsTrue(IVSCalc.Math.Pow(1.0005003, 4) == 1.00200270);
            Assert.IsTrue(IVSCalc.Math.Pow(-50, 3) == -125000);
            Assert.IsTrue(IVSCalc.Math.Pow(50, 3) == 125000);
            Assert.IsTrue(IVSCalc.Math.Pow(50, 50) == 8.8817841970012542E+84);
            Assert.IsTrue(IVSCalc.Math.Pow(-500, 50) == 8.8817841970012554E+134);
            Assert.IsTrue(IVSCalc.Math.Pow(9, 5) == 59049);
            Assert.IsTrue(IVSCalc.Math.Pow(5e7, 2) == 2.5e15);

            Assert.IsFalse(IVSCalc.Math.Pow(5e15, 0) != 1);
        }

        [TestMethod]
        public void FactorialTest()
        {
            Assert.IsTrue(IVSCalc.Math.Factorial(0) == 1);
            Assert.IsTrue(IVSCalc.Math.Factorial(1) == 1);
            Assert.IsTrue(IVSCalc.Math.Factorial(5) == 120);
            Assert.IsTrue(IVSCalc.Math.Factorial(10) == 3628800);
            Assert.IsTrue(IVSCalc.Math.Factorial(20) == 2.43290200817664E+18);
            Assert.IsTrue(IVSCalc.Math.Factorial(2) == 2);           
            Assert.IsTrue(IVSCalc.Math.Factorial(80) == 7.1569457046263779E+118);
            Assert.IsTrue(IVSCalc.Math.Factorial(100) == 9.33262154439441E+157);
            Assert.IsTrue(IVSCalc.Math.Factorial(5) == 120);
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Factorial(-1)));
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Factorial(-100)));
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Factorial(-10e5)));
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Factorial(0.5)));
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Factorial(-0.10)));
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Factorial(-1500)));
        }

        [TestMethod]
        public void LogTest()
        {
            Assert.IsTrue(IVSCalc.Math.Log(1) == 0);
            Assert.IsTrue(IVSCalc.Math.Log(2) == 0.30103);
            Assert.IsTrue(IVSCalc.Math.Log(3) == 0.47712125);
            Assert.IsTrue(IVSCalc.Math.Log(10) == 1);
            Assert.IsTrue(IVSCalc.Math.Log(100) == 2);
            Assert.IsTrue(IVSCalc.Math.Log(5.5e5) == 5.74036269);
            Assert.IsTrue(IVSCalc.Math.Log(2426.1131) == 3.38491104);
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Log(0)));
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Log(-10)));
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Log(-0.005)));
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Log(-1)));
            Assert.IsTrue(double.IsNaN(IVSCalc.Math.Log(-5e10)));
        }
    }
}
