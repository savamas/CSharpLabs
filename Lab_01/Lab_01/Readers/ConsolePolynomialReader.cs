using static System.Console;
using Lab_01.MyInterfaces;

namespace Lab_01.Readers
{
	class ConsolePolynomialReader : IPolynomialReader
	{
		public int ReadPower()
		{
			int power;

			do
			{
				power = NumberReader.ReadInteger("polynomial power");
				if (power < 1) WriteLine("Power can't be lower than 1!");
			} while (power < 1);

			return power;
		}

		public double[] ReadFactors(int power)
		{
			double res;
			double[] factors = new double[power + 1];

			for (int i = 0; i <= power; ++i)
			{
				res = NumberReader.ReadDouble($"factor of X^{i}");
				factors[i] = res;
			}

			return factors;
		}
	}
}