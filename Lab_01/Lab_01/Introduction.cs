using System;
using System.Threading;

namespace Lab_01
{
	public class Introduction
	{
		private const int secondInMilliseconds = 1000;
		private const int amountOfSeconds = 3;

		public string UserName { get; private set; }
		private readonly string _programTitle;

		public Introduction(string programTitle) => _programTitle = programTitle;

		public void Greeting()
		{
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.Title = _programTitle;
			Console.Write("Please, introduce yourself: ");
			UserName = Console.ReadLine();
			Console.Write($"Hello, {UserName}!\n\nPress any key to start: ");
			Console.ReadKey(true);
			Console.Clear();
		}

		public void Parting()
		{
			Console.Clear();
			Console.WriteLine($"Goodbuy, {UserName}!\n\n" +
				               "*****************************************\n\n" +
				              $"Work Completion in ...\n");
			for (int i = amountOfSeconds; i > 0; --i)
			{
				Console.WriteLine(i);
				Thread.Sleep(secondInMilliseconds);
			}
		} 
	}
}