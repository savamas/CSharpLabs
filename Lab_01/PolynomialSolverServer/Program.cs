using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;
using Lab_01;

namespace PolynomialSolverServer
{
	public class Program
	{
		private const int port = 23; //default telnet port
		private const string localhost = "127.0.0.1";

		static void Main(string[] args)
		{
			Polynomial service = new Polynomial(new FullStringPolynomialFormer(), new PolynomialStringParser());

	    	IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(localhost), port);
			Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			try
			{
				listenSocket.Bind(ipPoint);
				listenSocket.Listen(10);
				Console.WriteLine("Server launched, waiting for connections...");
				while (true)
				{
					Client client = new Client(listenSocket.Accept());
					Thread clientThread = new Thread(new ThreadStart(client.LaunchProcess));
					clientThread.Start();
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
