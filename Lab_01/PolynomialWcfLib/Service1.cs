using PolynomialLib;
using System;

namespace PolynomialWcfLib
{
	public class PolynomialWcfService : IPolynomial
	{
		private Polynomial solver = new Polynomial(new FullStringPolynomialFormer(),
													new PolynomialStringParser());

		public string GetPolynomialSolution(string equation)
		{
			Console.WriteLine("Received equation: " + equation);

			int power = solver.StringParser.GetPower(equation);
			double[] factors = solver.StringParser.GetFactors(equation, power);

			solver.GetRealroots(power, factors, out double[] roots, out int rootsCount);
			string respond = solver.FullStringFormer.Form(new PolynomialItem(power, factors, roots, rootsCount));

			return respond;
		}
	}
}
