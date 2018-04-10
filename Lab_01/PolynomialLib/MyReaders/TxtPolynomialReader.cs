using System;
using System.IO;
using System.Linq;
using PolynomialLib.MyInterfaces;

namespace PolynomialLib.MyReaders
{
	public class TxtPolynomialReader : BaseReader
	{
		private string line;
		private IPolynomialParser<string> parser = new PolynomialStringParser();

		public override int ReadPower()
		{
			int index;
			string[] allLines;

			do { index = ReadInteger("index of the polynomial");
				allLines = File.ReadAllLines("Polynomial.txt");
				if (index > allLines.Length) Console.WriteLine("No such index found!");
			} while (index > allLines.Length);
			line = File.ReadAllLines("Polynomial.txt").Skip(index - 1).Take(1).First();

			return parser.GetPower(line);
		}

		public override double[] ReadFactors(int power) => parser.GetFactors(line, power);
	}
}