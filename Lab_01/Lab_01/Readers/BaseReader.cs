using System;
using Lab_01.MyInterfaces;

namespace Lab_01
{
	public abstract class BaseReader : IPolynomialReader
	{
		public abstract int ReadPower();
		public abstract double[] ReadFactors(int power);

		public int ReadInteger(string message) => ReadNum<int>(message, int.TryParse);

		public double ReadDouble(string message) => ReadNum<double>(message, double.TryParse);

		private delegate bool TryParse<T>(string str, out T value);

		private T ReadNum<T>(string message, TryParse<T> parseFunc)
		{

			Console.Write("Please, input " + message + ": ");

			T res;
			string strRes = null;

			do
			{
				if (strRes != null)
				{
					Console.WriteLine("Can't parse number. Try again");
				}
				strRes = Console.ReadLine();
			} while (!parseFunc(strRes, out res));

			return res;
		}
	}
}
