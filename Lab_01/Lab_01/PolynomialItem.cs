using System;
using System.Linq;

namespace Lab_01
{
	class PolynomialItem
	{
		private int _power, _rootsCount;
		private double[] _factors, _roots;
		private string strImage;

		public string StrImage => strImage;

		public PolynomialItem(int power, double[] factors, double[] roots, int rootsCount)
		{
			_power = power;
			_factors = factors;
			_roots = roots;
			_rootsCount = rootsCount;

			strImage = $"{(_factors[0] < 0 ? " -" : " +")} {Math.Abs(_factors[0])}";
			for (int i = 1; i < _power; i++)
			{
				strImage = $"{(_factors[i] < 0 ? " -" : " +")}" +
					                     $" {Math.Abs(_factors[i])} * X^{i}" +
										 strImage;
			}
			strImage = $"{_factors[_power]} * X^{_power}" + strImage;
			strImage = strImage + " = 0; solution: ";

			if (_factors.All(x => x == 0)) { strImage = strImage + "RATIONAL SET"; }
			else
			{
				if (_rootsCount < 1 || (factors.Length == 2) & (factors[0] != 0) & (factors[1] == 0))
				  { strImage = strImage + "NO ROOTS"; }
				else for (int i = 0; i < _rootsCount; i++)
				     {
				     	strImage = strImage + $"X{i + 1} = {_roots[i]}; ";
				     }
			}
		}
	}
}