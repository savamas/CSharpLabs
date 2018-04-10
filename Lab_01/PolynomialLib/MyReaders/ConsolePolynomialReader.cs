using static System.Console;

namespace PolynomialLib.MyReaders
{
    public class ConsolePolynomialReader : BaseReader
	{
		public override int ReadPower()
		{
			int power;

			do
			{
				power = ReadInteger("polynomial power");
				if (power < 1) WriteLine("Power can't be lower than 1!");
			} while (power < 1);

			return power;
		}

		public override double[] ReadFactors(int power)
		{
			double res;
			double[] factors = new double[power + 1];

			for (int i = 0; i <= power; ++i)
			{
				res = ReadDouble($"factor of X^{i}");
				factors[i] = res;
			}

			return factors;
		}
	}
}