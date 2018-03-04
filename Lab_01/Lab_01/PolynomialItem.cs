using System;
using System.Linq;

namespace Lab_01
{
	class PolynomialItem
	{
		private int _power, _rootsCount;
		private double[] factors, roots;
		public string StrImage;

		public PolynomialItem(int Power, double[] Factors, double[] Roots, int RootsCount)
		{
			_power = Power;
			factors = Factors;
			roots = Roots;
			_rootsCount = RootsCount;

			StrImage = $"{(factors[0] < 0 ? " -" : " +")} {Math.Abs(factors[0])}";
			for (int i = 1; i < _power; i++)
			{
				StrImage = $"{(factors[i] < 0 ? " -" : " +")}" +
					                     $" {Math.Abs(factors[i])} * X^{i}" +
										 StrImage;
			}
			StrImage = $"{factors[_power]} * X^{_power}" + StrImage;
			StrImage = StrImage + " = 0; solution: ";

			if (factors.All(x => x == 0)) { StrImage = StrImage + "RATIONAL SET"; }
			else
			{
				if (_rootsCount < 1 || (Factors.Length == 2) & (Factors[0] != 0) & (Factors[1] == 0))
				  { StrImage = StrImage + "NO ROOTS"; }
				else for (int i = 0; i < _rootsCount; i++)
				     {
				     	StrImage = StrImage + $"X{i + 1} = {roots[i]}; ";
				     }
			}
		}
	}
}