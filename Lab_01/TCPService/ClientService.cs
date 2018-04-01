using Lab_01.MyInterfaces;
using Lab_01;
using System.Net.Sockets;

namespace PolynomialTCPService
{
	public class ClientService : TCPService, ITcpIO
	{
		private IPolynomialReader reader;
		private BaseStringPolynomialFormer former = new BaseStringPolynomialFormer();

		public ClientService(IPolynomialReader reader, Socket handler) : base(handler) => this.reader = reader;

		public void SendData(string userName)
		{
			int power = reader.ReadPower();
			double[] factors = reader.ReadFactors(power);

			Write(userName + ": " + former.Form(new PolynomialItem(power, factors, null, 0)));
		}

		public int x;

		public string GetData() => Read();
	}
}
