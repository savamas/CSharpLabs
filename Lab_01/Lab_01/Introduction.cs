using System;
using System.Threading;

namespace Lab_01
{
	class Introduction
	{
		private const int SecondInMilliseconds = 1000;
		private const int AmountOfSeconds = 3;

		private string _userName;
		private string _programTitle;

		public Introduction(string ProgramTitle)
		{
			_programTitle = ProgramTitle;
		}

		public void Greeting()
		{
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.Title = _programTitle;
			Console.Write("Please, introduce yourself: ");
			_userName = Console.ReadLine();
			Console.Write($"Hello, {_userName}!\n\nPress any key to start: ");
			Console.ReadKey();
			Console.Clear();
		}

		public void Parting(OperationState ErrorState)
		{
			Console.Clear();
			Console.WriteLine($"Program state: {ErrorState}\n\nGoodbuy, {_userName}!\n\n" +
				               "*****************************************\n\n" +
				              $"Work Completion in ...\n");
			for (int i = AmountOfSeconds; i > 0; i--)
			{
				Console.WriteLine(i);
				Thread.Sleep(SecondInMilliseconds);
			}
		} 
	}
}
