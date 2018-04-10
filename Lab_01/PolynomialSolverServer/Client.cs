using System;
using PolynomialLib.TCPServices;

namespace PolynomialSolverServer
{
	public class Client
	{
		private BaseService service;
		private string clientName;

		private bool clientFirstRequest;

		public delegate void ConnectionEventHandler(string clientName);

		public event ConnectionEventHandler ClientConnected;
		public event ConnectionEventHandler ClientDisconnected;

		public Client(BaseService service)
		{
			this.service = service;
			clientFirstRequest = true;
		}

		public void LaunchProcess()
		{
			bool processIsLaunched = true;

			try
			{
				while (processIsLaunched)
				{
					string clientRequest = service.GetData();

					if (String.IsNullOrEmpty(clientRequest)) break;

					if (clientFirstRequest)
					{
						clientName = clientRequest.Substring(0, clientRequest.IndexOf(':'));
						ClientConnected?.Invoke(clientName);
						clientFirstRequest = false;
					}

					Console.WriteLine(DateTime.Now.ToShortTimeString() + " " + clientRequest);

					clientRequest = clientRequest.Substring(clientRequest.IndexOf(':') + 1).Trim();

					service.SendData(clientRequest);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				ClientDisconnected(clientName);
				service.Disconnect();
			}
		}
	}
}
