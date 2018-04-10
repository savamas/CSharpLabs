using System;
using System.Net;
using System.Net.Sockets;
using PolynomialSolver;
using PolynomialLib.MyReaders;
using PolynomialLib.TCPServices;

namespace PolynomialSolverClient
{
	public class Program
	{
		private const int port = 23; //default telnet port
		private const string remoteHost = "127.0.0.1";

		private static BaseService service;

		static void Main(string[] args)
		{
			Introduction Intro = new Introduction("Polynomial solver (Client Application)");
			Intro.Greeting();

			IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(remoteHost), port);

			ConsoleKeyInfo keyInfo;
			bool next = true;
			bool ServerLaunched = true;

			try
			{
				service = new ClientService(new ConsolePolynomialReader(), new Socket(AddressFamily.InterNetwork,
													  							         SocketType.Stream,
																	                   ProtocolType.Tcp));
				service.Handler.Connect(ipPoint);

				do
				{
					Console.Write("Press any key to solve polinomial(q to quit): ");
					keyInfo = Console.ReadKey(true);
					Console.Clear();
					if (keyInfo.KeyChar == 'q') next = false;
					else
					{
						service.SendData(Intro.UserName);
						Console.WriteLine("\n" + DateTime.Now.ToShortTimeString() + " Server respond: " + 
							              service.GetData() + "\n");
					}
				} while (next);
			}		
			catch (SocketException ex)
			{
				if (ex.SocketErrorCode == SocketError.ConnectionReset)
					Console.WriteLine("\nConnection lost!\nPress any key to exit");
				if (ex.SocketErrorCode == SocketError.ConnectionRefused)
				{
					Console.WriteLine("\nServer's not launched!\nPress any key to exit");
					ServerLaunched = false;
				}
				Console.ReadKey();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				if (ServerLaunched) service.Disconnect();
				Intro.Parting();
			}
		}
	}
}
