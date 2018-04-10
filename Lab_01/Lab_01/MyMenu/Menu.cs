using System;
using System.Collections.Generic;

namespace PolynomialSolver.MyMenu
{
	public class Menu
	{
		private string _menuLabel;
		protected List<MenuItem> items;

		public Menu(string menuLabel)
		{
			items = new List<MenuItem>();
			_menuLabel = menuLabel;
		}

		protected void AddMenuItem(MenuItem item) => items.Add(item);

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
	}
}