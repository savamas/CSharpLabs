namespace PolynomialLib
{
	public class PolynomialItem
	{
		public int _power { get; private set; }
		public int _rootsCount { get; private set; }
		public double[] _factors { get; private set; }
		public double[] _roots { get; private set; }

		public PolynomialItem(int power, double[] factors, double[] roots, int rootsCount)
		{
			_power = power;
			_factors = factors;
			_roots = roots;
			_rootsCount = rootsCount;
		}
	}
}