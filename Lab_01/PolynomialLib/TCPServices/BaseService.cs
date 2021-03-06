﻿using System.Text;
using System.Net.Sockets;

namespace PolynomialLib.TCPServices
{
	public abstract class BaseService
	{
		public Socket Handler { get; private set; }

		public BaseService(Socket handler) => Handler = handler;

		protected void Write(string message)
		{
			byte[] data = Encoding.Unicode.GetBytes(message);
			Handler.Send(data);
		}

		protected string Read()
		{
			byte[] data = new byte[256];
			StringBuilder builder = new StringBuilder();
			int bytes = 0;

			do
			{
				bytes = Handler.Receive(data, 0);
				builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
			} while (Handler.Available > 0);

			return builder.ToString();
		}

		public abstract void SendData(string tmpString);
		public abstract string GetData();

		public void Disconnect()
		{
			Handler.Shutdown(SocketShutdown.Both);
			Handler.Close();
		}
	}
}
