namespace PolynomialTCPService
{
	public interface ITcpIO
	{
		void SendData(string tmpString);
		string GetData();
	}
}
