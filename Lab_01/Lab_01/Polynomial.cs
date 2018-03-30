using System.Linq;
using System.Collections.Generic;
using Lab_01.MyInterfaces;

namespace Lab_01
{
	public partial class Polynomial
	{
		public List<PolynomialItem> Solutions { get; private set; }
		public IPolynomialFormer<string> FullStringFormer { get; private set; }
		public IPolynomialParser<string> StringParser { get; private set; }

		public Polynomial(IPolynomialFormer<string> fullStringFormer,
			              IPolynomialParser<string> stringParser)
		{
			Solutions = new List<PolynomialItem>();
			FullStringFormer = fullStringFormer;
			StringParser = stringParser;
		}

		partial void Polinom(int n, double x, double[] k, ref double s);
		partial void Dihot(int degree, double edgeNegativ, double edgePositiv, double[] kf, ref double x);
		partial void StepUp(int level, double[][] A, double[][] B, int[] currentrootsCount);

		public void GetRealroots(int n, double[] factors, out double[] roots, out int rootsCount)
		{
			/*
			  используются вспомогательные массивы A и B, имеющие следующее содержание
		      A это коэффициенты а B корни производных полиномов
			  все A-полиномы нормируются так,
			  чтобы коэффициент при старшей степени был равен единице
			  A[n] - это массив нормированных коэффициентов исходного полинома
			  B[n] - это массив корней исходного полинома
			  A[n-1] - это массив нормированных коэффициентов производного полинома
			  B[n-1] - это массив корней производного полинома
			  аналогичным образом
			  A[n-2] и B[n-2] - это коэффициенты и корни дважды производного полинома
			  наконец A[1] - это массив коэффициентов последнего полинома
			  в цепочке производных полиномов
			  это линейный полином и B[1] - это массив его корней,
			  представленный единственным значимым элементом
			*/

			roots = new double[n];

			double[][] A = new double[n + 1][];
			double[][] B = new double[n + 1][];
			int[] currentrootsCount = new int[n + 1];

			for (int i = 1; i <= n; ++i)
			{
				A[i] = new double[i];
				B[i] = new double[i];
			}

			//нормировка исходного полинома

			for (int i = 0; i < n; ++i) A[n][i] = factors[i] / factors[n];

			//расчёт производных A-полиномов

			for (int i1 = n, i = n - 1; i > 0; i1 = i, i--)
			{
				for (int j1 = i, j = i - 1; j >= 0; j1 = j, j--)
				{
					A[i][j] = A[i1][j1] * j1 / i1;
				}
			}

			//формирование исходного корня последнего производного полинома

			currentrootsCount[1] = 1;
			B[1][0] = -A[1][0];

			//подъём по лестнице производных полиномов

			for (int i = 2; i <= n; ++i) StepUp(i, A, B, currentrootsCount);

			//формирование результата

			rootsCount = currentrootsCount[n];
			for (int i = 0; i < rootsCount; ++i) roots[i] = B[n][i];

			if (factors[0] != 0)
			{
				roots = roots.Where(x => x != 0).ToArray();
				rootsCount = roots.Length;
			}
			if (rootsCount != 0)
			{
				roots = roots.Distinct().ToArray();
				rootsCount = roots.Length;
			}
		}

	}
}