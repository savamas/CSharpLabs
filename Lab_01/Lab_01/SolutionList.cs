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
			int Power, RootsCount = 0;
			double[] factors, roots;

			Console.Write("Please, input polynomials power: ");
			string PowerStr = Console.ReadLine();
			if (!int.TryParse(PowerStr, out Power))
			{
				return OperationState.ERROR_INCORRECT_POWER_INPUT;
			}
			if (Power < 1)
			{
				return OperationState.ERROR_INCORRECT_POWER_INPUT;
			}

			factors = new double[Power + 1];
			roots = new double[Power];

			for (int i = 0; i <= Power; i++)
			{
				Console.Write($"Please, input factor of X^{i}: ");
				string FactorStr = Console.ReadLine();
				if (!double.TryParse(FactorStr, out double res))
				{
					return OperationState.ERROR_INCORRECT_FACTOR_INPUT;
				}
				factors[i] = res;
			}

			Polynomial PolynomialService = new Polynomial();
			PolynomialService.GetRealRoots(Power, factors, ref roots, ref RootsCount);

			Solutions.Add(new PolynomialItem(Power, factors, roots, RootsCount));

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
			string IndexStr = Console.ReadLine();
			if (!int.TryParse(IndexStr, out int res))
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
