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
		private string clientName;
		private bool clientFirstRequest;
		private ConsoleColor clientColor;

		public Client(Socket handler)
		{
			this.handler = handler;
			clientColor = GetARandomColorForClient();
			clientFirstRequest = true;
		}

		private string GetRequest()
		{
			byte[] data = new byte[256];
			StringBuilder builder = new StringBuilder();
			int bytes = 0;

			do
			{
				bytes = handler.Receive(data, 0);
				builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
			} while (handler.Available > 0);

			return builder.ToString();
		}

		private void SendRespond(string request)
		{
			byte[] data = new byte[256];
			Polynomial service = new Polynomial(new FullStringPolynomialFormer(), new PolynomialStringParser());

			IPolynomialParser<string> parser = new PolynomialStringParser();
			int power = parser.GetPower(request);
			double[] factors = parser.GetFactors(request, power);

			service.GetRealroots(power, factors, out double[] roots, out int rootsCount);
			string message = service.FullStringFormer.Form(new PolynomialItem(power, factors, roots, rootsCount));

			data = Encoding.Unicode.GetBytes(message);
			handler.Send(data);
		}

		public void LaunchProcess()
		{
			bool processIsLaunched = true;

			try
			{
				while (processIsLaunched)
				{
					string clientRequest = GetRequest();

					if (String.IsNullOrEmpty(clientRequest)) break;

					if (clientFirstRequest)
					{
						clientName = clientRequest.Substring(0, clientRequest.IndexOf(':'));
						Console.ForegroundColor = clientColor;
						Console.WriteLine("\n" + clientName + " connected\n");
						clientFirstRequest = false;
					}

					Console.ForegroundColor = clientColor;
					Console.WriteLine(DateTime.Now.ToShortTimeString() + " " + clientRequest);

					clientRequest = clientRequest.Substring(clientRequest.IndexOf(':') + 1).Trim();

					SendRespond(clientRequest);
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

		private void Disconnect()
		{
			Console.ForegroundColor = clientColor;
			Console.WriteLine(clientName + " disconnected");
			handler.Shutdown(SocketShutdown.Both);
			handler.Close();
		}

		private ConsoleColor GetARandomColorForClient()
		{
			Random random = new Random();
			Array consoleColors = Enum.GetValues(typeof(ConsoleColor));
			return (ConsoleColor)consoleColors.GetValue(random.Next(consoleColors.Length));
		}
	}
}
