using System;
using System.Text;
using System.Net.Sockets;
using Lab_01;
using Lab_01.MyInterfaces;

namespace PolynomialSolverServer
{
	public class Client
	{
		private Socket handler;
		private Polynomial service;
		private string clientName; // gonna be extended

		public Client(Socket handler)
		{
			this.handler = handler;
			service = new Polynomial(new FullStringPolynomialFormer(), new PolynomialStringParser());
		}

		public void LaunchProcess()
		{
			bool processIsLaunched = true;
			byte[] data = new byte[256];
			int bytes;

			try
			{
				while (processIsLaunched)
				{
					StringBuilder builder = new StringBuilder();
					bytes = 0;

					do
					{
						bytes = handler.Receive(data, 0);
						builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
					} while (handler.Available > 0);

					if (builder.ToString() == "") break;
					Console.WriteLine(DateTime.Now.ToShortTimeString() + " " + builder.ToString());

					string tmpString = builder.ToString();
					tmpString = tmpString.Substring(tmpString.IndexOf(':') + 1).Trim();

					IPolynomialParser<string> parser = new PolynomialStringParser();
					int power = parser.GetPower(tmpString);
					double[] factors = parser.GetFactors(tmpString, power);

					service.GetRealroots(power, factors, out double[] roots, out int rootsCount);
					string message = service.FullStringFormer.Form(new PolynomialItem(power, factors, roots, rootsCount));

					data = Encoding.Unicode.GetBytes(message);
					handler.Send(data);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				Disconnect();
			}
		}

		public void Disconnect()
		{
			handler.Shutdown(SocketShutdown.Both);
			handler.Close();
		}
	}
}
