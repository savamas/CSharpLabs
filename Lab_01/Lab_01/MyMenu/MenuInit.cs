using System;
using System.IO;
using Lab_01.MyInterfaces;
using Lab_01.Readers;

namespace Lab_01.MyMenu
{
	class MenuTasks : Menu
	{
		private Polynomial service = Polynomial.getService(new TxtFullPolynomialFormer());

		public MenuTasks(string menuLabel) : base(menuLabel) { }

		protected void Solve()
		{
			Console.WriteLine();
			Menu solveMenu = new SolveMenuInit("Use one of the commands: ");
			Console.Clear();
			solveMenu.Start();
		}

		protected void SolveFromCLIAdapter() => Solve(new ConsolePolynomialReader());

		protected void SolveFromTXTAdapter() => Solve(new TxtPolynomialReader());

		protected void Solve(IPolynomialReader reader)
		{
			int power;
			double[] factors;

			power = reader.ReadPower();
			factors = reader.ReadFactors(power);

			service.GetRealroots(power, factors, out double[] roots, out int rootsCount);

			service.Solutions.Add(new PolynomialItem(power, factors, roots, rootsCount));

			Console.WriteLine("\n" + service.StrFullFormer.Form(service.Solutions[service.Solutions.Count - 1]) + "\n");
		}

		protected void Find()
		{
			int index;

			if (0 == service.Solutions.Count)
			{
				Console.WriteLine("No solutions found!\n");
				return;
			}
			do
			{
				index = NumberReader.ReadInteger("index of the solution");
				if (index < 1 || index > service.Solutions.Count) Console.WriteLine("No such index found!\n");
			} while (index < 1 || index > service.Solutions.Count);

			Console.WriteLine("\n" + service.StrFullFormer.Form(service.Solutions[service.Solutions.Count - 1]) + "\n");
		}

		protected void Save()
		{
			using (TextWriter writer = File.CreateText("Polynomial.txt"))
			{
				TxtBasePolynomialFormer _strBaseFormer = new TxtBasePolynomialFormer();
				for (int i = 0; i < service.Solutions.Count; ++i)
					writer.WriteLine(_strBaseFormer.Form(service.Solutions[i]));
			}
			service.Solutions.Clear();
			Console.WriteLine("\n***Saved***\n");
		}
	}

	class MainMenuInit : MenuTasks
	{
		public MainMenuInit(string menuLabel) : base(menuLabel) => Init();

		private void Init()
		{
			AddMenuItem(new MenuItem { Label = "Solve", Task = Solve });
			AddMenuItem(new MenuItem { Label = "Find", Task = Find });
			AddMenuItem(new MenuItem { Label = "Save in txt format", Task = Save });
			AddMenuItem(new MenuItem { Label = "Exit", Task = () => { } });
		}	
	}

	class SolveMenuInit : MenuTasks
	{
		public SolveMenuInit(string menuLabel) : base(menuLabel) => Init();

		private void Init()
		{
			AddMenuItem(new MenuItem { Label = "Read from console", Task = SolveFromCLIAdapter });
			AddMenuItem(new MenuItem { Label = "Read from txt file by index", Task = SolveFromTXTAdapter });
			AddMenuItem(new MenuItem { Label = "Back", Task = () => { Console.WriteLine(); } });
		}
	}
}
