namespace PolynomialLib.MyInterfaces
{
	public interface IPolynomialParser<T>
	{
		int GetPower(T sourse);
		double[] GetFactors(T source, int power);
	}
}
