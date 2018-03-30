using System;
using System.Net;
using System.Net.Sockets;
using Lab_01;
using Lab_01.Readers;

namespace PolynomialSolverClient
{
	public class Program
	{
		private const int port = 23; //default telnet port
		private const string remoteHost = "127.0.0.1";

		static void Main(string[] args)
		{
			Introduction Intro = new Introduction("Polynomial solver (Client Application)");
			Intro.Greeting();

			IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(remoteHost), port);

			ClientService clientService = new ClientService(new Socket(AddressFamily.InterNetwork,
																	   SocketType.Stream,
																	   ProtocolType.Tcp));
			ConsoleKeyInfo keyInfo;
			bool next = true;

			try
			{
				clientService.TCPSocket.Connect(ipPoint);

				do
				{
					Console.Write("Press any key to solve polinomial(q to quit): ");
					keyInfo = Console.ReadKey(true);
					Console.Clear();
					if (keyInfo.KeyChar == 'q') next = false;
					else
					{
						clientService.SendRequest(new ConsolePolynomialReader(), Intro.UserName);
						Console.WriteLine("\n" + DateTime.Now.ToShortTimeString() + " Server respond: " + 
							              clientService.GetRespond() + "\n");
					}
				} while (next);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				clientService.Disconnect();
				Intro.Parting();
			}
		}
	}
}
