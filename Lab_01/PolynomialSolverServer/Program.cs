using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;

namespace PolynomialSolverServer
{
	public class Program
	{
		private const int port = 23; //default telnet port
		private const string localhost = "127.0.0.1";
		private const bool serverIsLaunched = true;

		static void Main(string[] args)
		{
			IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(localhost), port);
			Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			try
			{
				listenSocket.Bind(ipPoint);
				listenSocket.Listen(10);
				Console.WriteLine("Server launched, waiting for connections...");
				while (serverIsLaunched)
				{
					Client client = new Client(listenSocket.Accept());
					Thread clientThread = new Thread(new ThreadStart(client.LaunchProcess));
					clientThread.Start();
				}
			}
			catch (SocketException ex)
			{
				if (ex.SocketErrorCode == SocketError.AddressAlreadyInUse)
				Console.WriteLine("Server's already launched!");
				Console.ReadKey();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
