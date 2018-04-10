namespace PolynomialLib.MyInterfaces
{
	public interface IPolynomialReader
	{
		int ReadPower();
		double[] ReadFactors(int power);
		int ReadInteger(string message);
		double ReadDouble(string message);
	}
}
