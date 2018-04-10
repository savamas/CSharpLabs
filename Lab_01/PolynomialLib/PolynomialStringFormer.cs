using PolynomialLib.MyInterfaces;
using System;
using System.Linq;

namespace PolynomialLib
{
	public class BaseStringPolynomialFormer : IPolynomialFormer<string>
	{
		public virtual string Form(PolynomialItem item)
		{
			string strImage = $"{(item._factors[0] < 0 ? " -" : " +")} {Math.Abs(item._factors[0])}";
			for (int i = 1; i < item._power; ++i)
			{
				strImage = $"{(item._factors[i] < 0 ? " -" : " +")}" +
										 $" {Math.Abs(item._factors[i])} * X^{i}" +
										 strImage;
			}
			strImage = $"{item._factors[item._power]} * X^{item._power}" + strImage;
			strImage = strImage + " = 0;";

			return strImage;
		}
	}

	public class FullStringPolynomialFormer : BaseStringPolynomialFormer
	{
		public override string Form(PolynomialItem item)
		{
			string strImage = base.Form(item);
			strImage = strImage + " solution: ";

			if (item._factors.All(x => x == 0)) { strImage = strImage + "RATIONAL SET"; }
			else
			{
				if (item._rootsCount < 1 || (item._factors.Length == 2) & (item._factors[0] != 0) & (item._factors[1] == 0))
				{ strImage = strImage + "NO ROOTS"; }
				else for (int i = 0; i < item._rootsCount; ++i)
					{
						strImage = strImage + $"X{i + 1} = {item._roots[i]}; ";
					}
			}

			return strImage;
		}
	}
}
