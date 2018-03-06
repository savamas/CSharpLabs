using System;

namespace Lab_01
{
	partial class Polynomial
	{
		//полином вида x^n+k[n-1]*x^(n-1)+k[n-2]*x^(n-2)+...+k[1]*x+k[0]
		//со старшим коэффициентом, равным единице

		partial void Polinom(int n, double x, double[] k, ref double s)
		{
			for (int i = n - 1; i >= 0; i--)
				s = s * x + k[i];
		}

		//расчёт корня полинома методом деления пополам отрезка, содержащего этот корень

		partial void Dihot(int degree, double edgeNegativ, double edgePositiv, double[] kf, ref double x)
		{
			for (; ;)
			{//цикл деления отрезка пополам
				double s = 1;
				x = 0.5 * (edgeNegativ + edgePositiv);
				if (x == edgeNegativ || x == edgePositiv) return;
				Polinom(degree, x, kf, ref s);
				if (s < 0) edgeNegativ = x;
				else edgePositiv = x;
			}//цикл деления отрезка пополам
		}

		partial void StepUp(int level, double[][] A, double[][] B, int[] currentrootsCount)
		{
			double major = 0;
			for (int i = 0; i < level; i++)
			{//формирование major
				double s = Math.Abs(A[level][i]);
				if (s > major) major = s;
			}//формирование major
			major += 1.0;
			currentrootsCount[level] = 0;

			for (int i = 0; i <= currentrootsCount[level - 1]; i++)
			{//очередной интервал монотонности
			 //signLeft signRight - знаки текущего A-полинома на левой и правой границе интервала монотонности
				int signLeft, signRight;

				//предварительная левая и правая границы интервала поиска
				double edgeLeft, edgeRight;

				//границы интервала монотонности, несущие информацию о знаке полинома на них
				double edgeNegativ, edgePositiv;

				//формирование левой границы поиска
				if (i == 0) edgeLeft = -major;
				else edgeLeft = B[level - 1][i - 1];

				//значение текущего A-полинома на левой границе
				double rb = 1;
				Polinom(level, edgeLeft, A[level], ref rb);

				if (rb == 0)
				{//маловероятный случай попадания в корень
					B[level][currentrootsCount[level]] = edgeLeft;
					currentrootsCount[level]++;
					continue;
				}//маловероятный случай попадания в корень

				//запомнить знак текущего A-полинома на левой границе
				if (rb > 0) signLeft = 1; else signLeft = -1;

				//формирование правой границы поиска
				if (i == currentrootsCount[level - 1]) edgeRight = major;
				else edgeRight = B[level - 1][i];

				//значение текущего A-полинома на правой границе
				Polinom(level, edgeRight, A[level], ref rb);

				if (rb == 0)
				{//маловероятный случай попадания в корень
					B[level][currentrootsCount[level]] = edgeRight;
					currentrootsCount[level]++;
					continue;
				}//маловероятный случай попадания в корень

				//запомнить знак текущего A-полинома на правой границе
				if (rb > 0) signRight = 1; else signRight = -1;

				//если знаки полинома на границах интервала монотонности совпадают,
				//то корня нет
				if (signLeft == signRight) continue;

				//теперь можно определить плюс границу и минус границу поиска корня
				if (signLeft < 0) { edgeNegativ = edgeLeft; edgePositiv = edgeRight; }
				else { edgeNegativ = edgeRight; edgePositiv = edgeLeft; }

				//всё готово для локализации корня методом деления пополам интервала поиска

				double x = 1;

				Dihot(level, edgeNegativ, edgePositiv, A[level], ref x);

				B[level][currentrootsCount[level]] = x;
				currentrootsCount[level]++;
			}//очередной интервал монотонности
			return;
		}
	}
}
