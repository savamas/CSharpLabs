using System;
using System.Threading;

namespace Lab_01
{
	class Introduction
	{
		private const int secondInMilliseconds = 1000;
		private const int amountOfSeconds = 3;

		private string userName;
		private string _programTitle;

		public Introduction(string programTitle)
		{
			_programTitle = programTitle;
		}

		public void Greeting()
		{
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.Title = _programTitle;
			Console.Write("Please, introduce yourself: ");
			userName = Console.ReadLine();
			Console.Write($"Hello, {userName}!\n\nPress any key to start: ");
			Console.ReadKey();
			Console.Clear();
		}

		public void Parting(OperationState ErrorState)
		{
			Console.Clear();
			Console.WriteLine($"Program state: {ErrorState}\n\nGoodbuy, {userName}!\n\n" +
				               "*****************************************\n\n" +
				              $"Work Completion in ...\n");
			for (int i = amountOfSeconds; i > 0; i--)
			{
				Console.WriteLine(i);
				Thread.Sleep(secondInMilliseconds);
			}
		} 
	}
}