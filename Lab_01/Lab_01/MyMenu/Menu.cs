using System;
using System.Collections.Generic;
using System.IO;
using Lab_01.MyInterfaces;
using Lab_01.Readers;

namespace Lab_01.MyMenu
{
	class Menu
	{
		private string _menuLabel;
		private List<MenuItem> items;

		private Polynomial service;

		public Menu(string menuLabel)
		{
			items = new List<MenuItem>();
			service = Polynomial.getService(new TxtFullPolynomialFormer());
			_menuLabel = menuLabel;
		}

		private void AddMenuItem(MenuItem item) => items.Add(item);

		public void InitMenuItems()
		{
			AddMenuItem(new MenuItem { Label = "Solve", Task = Solve });
			AddMenuItem(new MenuItem { Label = "Find", Task =  Find });
			AddMenuItem(new MenuItem { Label = "Save in txt format", Task = Save });
			AddMenuItem(new MenuItem { Label = "Exit", Task = () => {} });
		}

		private void InitSolveMenuItems()
		{
			AddMenuItem(new MenuItem { Label = "Read from console", Task = SolveFromCLIAdapter });
			AddMenuItem(new MenuItem { Label = "Read from txt file by index", Task = SolveFromTXTAdapter});
			AddMenuItem(new MenuItem { Label = "Back", Task = () => { Console.WriteLine(); } });
		}

		public void Start()
		{
			ConsoleKeyInfo keyInfo;

			do
			{
				Console.WriteLine(_menuLabel);

				for (int i = 0; i < items.Count; ++i)
				{
					Console.WriteLine($"{i + 1}. {items[i].Label}");
				}

				keyInfo = Console.ReadKey(true);
				if (keyInfo.Key < ConsoleKey.D1 || (int)keyInfo.Key > (int)ConsoleKey.D1 + items.Count - 1)
					Console.WriteLine("No such item found!\n");
				else items[(int)Char.GetNumericValue(keyInfo.KeyChar) - 1].Task();
			} while ((int)keyInfo.Key != (int)ConsoleKey.D1 + items.Count - 1);
		}

		private void Solve()
		{
			Console.WriteLine();
			Menu solveMenu = new Menu("Use one of the commands: ");
			solveMenu.InitSolveMenuItems();
			Console.Clear();
			solveMenu.Start();
		}

		private void SolveFromCLIAdapter() => Solve(new ConsolePolynomialReader());

		private void SolveFromTXTAdapter() => Solve(new TxtPolynomialReader()); 

		private void Solve(IPolynomialReader reader)
		{
			int power;
			double[] factors;

			power = reader.ReadPower();
			factors = reader.ReadFactors(power);

			service.GetRealroots(power, factors, out double[] roots, out int rootsCount);

			service.Solutions.Add(new PolynomialItem(power, factors, roots, rootsCount));

			Console.WriteLine("\n" + service.StrFullFormer.Form(service.Solutions[service.Solutions.Count - 1]) + "\n");
		}

		private void Find()
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

		private void Save()
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
}

