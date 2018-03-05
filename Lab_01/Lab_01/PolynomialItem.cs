using System;
using System.Linq;

namespace Lab_01
{
	class PolynomialItem
	{
		private int _power, _rootsCount;
		private double[] _factors, _roots;
		public string StrImage;

		public PolynomialItem(int power, double[] factors, double[] roots, int rootsCount)
		{
			_power = power;
			_factors = factors;
			_roots = roots;
			_rootsCount = rootsCount;

			StrImage = $"{(_factors[0] < 0 ? " -" : " +")} {Math.Abs(_factors[0])}";
			for (int i = 1; i < _power; i++)
			{
				StrImage = $"{(_factors[i] < 0 ? " -" : " +")}" +
					                     $" {Math.Abs(_factors[i])} * X^{i}" +
										 StrImage;
			}
			StrImage = $"{_factors[_power]} * X^{_power}" + StrImage;
			StrImage = StrImage + " = 0; solution: ";

			if (_factors.All(x => x == 0)) { StrImage = StrImage + "RATIONAL SET"; }
			else
			{
				if (_rootsCount < 1 || (factors.Length == 2) & (factors[0] != 0) & (factors[1] == 0))
				  { StrImage = StrImage + "NO ROOTS"; }
				else for (int i = 0; i < _rootsCount; i++)
				     {
				     	StrImage = StrImage + $"X{i + 1} = {_roots[i]}; ";
				     }
			}
		}
	}
}