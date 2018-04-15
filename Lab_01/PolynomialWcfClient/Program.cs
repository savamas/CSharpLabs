using System;
using PolynomialSolver;
using PolynomialLib.MyInterfaces;
using PolynomialLib.MyReaders;
using PolynomialLib;
using PolynomialWcfClient.PolynomialServiceReference;

namespace PolynomialWcfClient
{
	class Program
	{
		static void Main(string[] args)
		{
			Introduction Intro = new Introduction("PolynomialWcf solver (Client Application)");
			Intro.Greeting();

			ConsoleKeyInfo keyInfo;
			bool next = true;

			IPolynomialReader reader = new ConsolePolynomialReader();
			BaseStringPolynomialFormer former = new BaseStringPolynomialFormer();

			PolynomialClient client = new PolynomialClient();

			do
			{
				Console.Write("Press any key to solve polinomial(q to quit): ");
				keyInfo = Console.ReadKey(true);
				Console.Clear();
				if (keyInfo.KeyChar == 'q') next = false;
				else
				{
					int power = reader.ReadPower();
					double[] factors = reader.ReadFactors(power);

					try
					{
						Console.WriteLine(
						$"Solution: {client.GetPolynomialSolution(former.Form(new PolynomialItem(power, factors, null, 0)))}");
					}
					catch (Exception)
					{
						Console.WriteLine("Host is not launched!");
						next = false;
						Console.ReadLine();
					}

				}
			} while (next);

			Intro.Parting();
		}
	}
}
