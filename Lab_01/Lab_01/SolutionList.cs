using System;
using System.Collections.Generic;

namespace Lab_01
{
	class SolutionList
	{
		private List<PolynomialItem> Solutions;

		public SolutionList()
		{
			Solutions = new List<PolynomialItem>();
		}

		public OperationState Solve()
		{
			int power, rootsCount = 0;
			double[] factors, roots;

			Console.Write("Please, input polynomials power: ");
			string powerStr = Console.ReadLine();
			if (!int.TryParse(powerStr, out power))
			{
				return OperationState.ERROR_INCORRECT_POWER_INPUT;
			}
			if (power < 1)
			{
				return OperationState.ERROR_INCORRECT_POWER_INPUT;
			}

			factors = new double[power + 1];
			roots = new double[power];

			for (int i = 0; i <= power; i++)
			{
				Console.Write($"Please, input factor of X^{i}: ");
				string factorStr = Console.ReadLine();
				if (!double.TryParse(factorStr, out double res))
				{
					return OperationState.ERROR_INCORRECT_FACTOR_INPUT;
				}
				factors[i] = res;
			}

			Polynomial PolynomialService = new Polynomial();
			PolynomialService.GetRealroots(power, factors, ref roots, ref rootsCount);

			Solutions.Add(new PolynomialItem(power, factors, roots, rootsCount));

			Console.WriteLine("\n" + Solutions[Solutions.Count - 1].StrImage + "\n");

			return OperationState.FuncClearCompleted;
		}

		public OperationState Find()
		{
			if (Solutions.Count == 0)
			{
				Console.WriteLine("No solutions found!\n");
				return OperationState.FuncClearCompleted;
			}
			Console.Write("Please, input index of the solution: ");
			string indexStr = Console.ReadLine();
			if (!int.TryParse(indexStr, out int res))
			{
				return OperationState.ERROR_INDEX_INPUT;
			}
			if (res < 1 || res > Solutions.Count)
			{
				return OperationState.ERROR_INDEX_INPUT;
			}

			Console.WriteLine("\n" + Solutions[res - 1].StrImage + "\n");

			return OperationState.FuncClearCompleted;
		}
	}		
}
