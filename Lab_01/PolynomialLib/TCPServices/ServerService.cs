using System.Net.Sockets;

namespace PolynomialLib.TCPServices
{
	public class ServerService : BaseService
	{
		private Polynomial service;

		public ServerService(Polynomial service, Socket handler) : base(handler) => this.service = service;

		public override void SendData(string request)
		{
			int power = service.StringParser.GetPower(request);
			double[] factors = service.StringParser.GetFactors(request, power);

			service.GetRealroots(power, factors, out double[] roots, out int rootsCount);
			string respond = service.FullStringFormer.Form(new PolynomialItem(power, factors, roots, rootsCount));

			Write(respond);
		}

		public override string GetData() => Read();
	}
}
