using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;
using PolynomialLib;
using PolynomialLib.TCPServices;

namespace PolynomialSolverServer
{
	public class Program
	{
		private const int port = 23; //default telnet port
		private const string localhost = "127.0.0.1";
		private const bool serverIsLaunched = true;

		static void Main(string[] args)
		{
			Console.Title = "Polynomial solver (Server)";

			IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(localhost), port);
			Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			try
			{
				listenSocket.Bind(ipPoint);
				listenSocket.Listen(10);
				Console.WriteLine("Server launched, waiting for connections...");
				while (serverIsLaunched)
				{
					Client client = new Client(new ServerService(new Polynomial(new FullStringPolynomialFormer(),
																				new PolynomialStringParser()),
																	 listenSocket.Accept()));
					client.ClientConnected += clientConnected;
					client.ClientDisconnected += clientDisconnected;
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

		private static void clientConnected(string clientName)
		{
			Console.WriteLine("\n" + clientName + " connected\n");
		}

		private static void clientDisconnected(string clientName)
		{
			Console.WriteLine("\n" + clientName + " disconnected\n");
		}
	}
}
