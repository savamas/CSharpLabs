namespace Lab_01.MyInterfaces
{
	public interface IPolynomialParser<T>
	{
		int GetPower(T sourse);
		double[] GetFactors(T source, int power);
	}
}
