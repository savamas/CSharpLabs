using PolynomialLib.MyInterfaces;
using System.Net.Sockets;

namespace PolynomialLib.TCPServices
{
	public class ClientService : BaseService
	{
		private IPolynomialReader reader;
		private BaseStringPolynomialFormer former = new BaseStringPolynomialFormer();

		public ClientService(IPolynomialReader reader, Socket handler) : base(handler) => this.reader = reader;

		public override	void SendData(string userName)
		{
			int power = reader.ReadPower();
			double[] factors = reader.ReadFactors(power);

			Write(userName + ": " + former.Form(new PolynomialItem(power, factors, null, 0)));
		}

		public override string GetData() => Read();
	}
}
