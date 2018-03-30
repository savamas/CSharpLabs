using Lab_01.MyInterfaces;

namespace Lab_01
{
	public class PolynomialStringParser : IPolynomialParser<string>
	{
		public int GetPower(string source)
		{
			string strToken = source.Substring(source.IndexOf('X') + 2, source.Length - source.IndexOf('X') - 2);

			return int.Parse(strToken.Substring(0, strToken.IndexOf(' ')));
		}

		public double[] GetFactors(string source, int power)
		{
			double[] factors = new double[power + 1];
			source = source.Substring(0, source.Length - 5);

			for (int i = power; i > 0; --i)
			{
				string strCoef = source.Substring(0, source.IndexOf('X') - 3);
				strCoef = strCoef.Replace(" ", string.Empty);
				strCoef = strCoef.Replace("+", string.Empty);
				factors[i] = double.Parse(strCoef);
				source = source.Substring(source.IndexOf('X') + 2 + i.ToString().Length,
									  source.Length - source.IndexOf('X') - 2 - i.ToString().Length);
			}
			source = source.Replace(" ", string.Empty);
			source = source.Replace("+", string.Empty);
			factors[0] = double.Parse(source);

			return factors;
		}
	}
}
