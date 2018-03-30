using System.Net.Sockets;
using System.Text;
using Lab_01;
using Lab_01.MyInterfaces;

namespace PolynomialSolverClient
{
	public class ClientService
	{
		public Socket TCPSocket { get; private set; }

		private BaseStringPolynomialFormer former = new BaseStringPolynomialFormer();

		public ClientService(Socket socket) => TCPSocket = socket;

		public void SendRequest(IPolynomialReader reader, string userName)
		{
			int power = reader.ReadPower();
			double[] factors = reader.ReadFactors(power);
			byte[] data = Encoding.Unicode.GetBytes(userName + ": " +
				                                    former.Form(new PolynomialItem(power, factors, null, 0)));
			TCPSocket.Send(data);
		}

		public string GetRespond()
		{
			byte[] data = new byte[256];
			StringBuilder builder = new StringBuilder();
			int bytes = 0;

			do
			{
				bytes = TCPSocket.Receive(data, 0);
				builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
			} while (TCPSocket.Available > 0);

			return builder.ToString();
		}

		public void Disconnect()
		{
			TCPSocket.Shutdown(SocketShutdown.Both);
			TCPSocket.Close();
		}
	}
}
