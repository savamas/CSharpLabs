using System;
using System.IO;
using System.Linq;
using Lab_01.MyInterfaces;

namespace Lab_01.Readers
{
	class TxtPolynomialReader : IPolynomialReader
	{
		private string line;

		public int ReadPower()
		{
			int index;
			string[] allLines;
			string tmpLine;

			do { index = NumberReader.ReadInteger("index of the polynomial");
				allLines = File.ReadAllLines("Polynomial.txt");
				if (index > allLines.Length) Console.WriteLine("No such index found!");
			} while (index > allLines.Length);
			line = File.ReadAllLines("Polynomial.txt").Skip(index - 1).Take(1).First();

			tmpLine = line.Substring(line.IndexOf('X') + 2, line.Length - line.IndexOf('X') - 2);

			return int.Parse(tmpLine.Substring(0, tmpLine.IndexOf(' ')));
		}

		public double[] ReadFactors(int power)
		{
			double[] factors = new double[power + 1];
			line = line.Substring(0, line.Length - 5);

			for (int i = power; i > 0; --i)
			{
				string strCoef = line.Substring(0, line.IndexOf('X') - 3);
				strCoef = strCoef.Replace(" ", string.Empty);
				strCoef = strCoef.Replace("+", string.Empty);
				factors[i] = double.Parse(strCoef);
				line = line.Substring(line.IndexOf('X') + 2 + i.ToString().Length,
					                  line.Length - line.IndexOf('X') - 2 - i.ToString().Length);
			}
			line = line.Replace(" ", string.Empty);
			line = line.Replace("+", string.Empty);
			factors[0] = double.Parse(line);

			return factors;
		}
	}
}
