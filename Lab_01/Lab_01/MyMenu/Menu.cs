using System;
using System.Collections.Generic;

namespace Lab_01.MyMenu
{
	class Menu
	{
		private string menuLabel;
		private List<MenuItem> items;

		public Menu(string MenuLabel)
		{
			items = new List<MenuItem>();
			menuLabel = MenuLabel;
		}

		private void AddMenuItem(MenuItem item) => items.Add(item);

		public void InitMenuItems(SolutionList Solutions)
		{
			AddMenuItem(new MenuItem { Label = "Solve", Action = Solutions.Solve });
			AddMenuItem(new MenuItem { Label = "Find", Action =  Solutions.Find });
			AddMenuItem(new MenuItem { Label = "Exit", Action = () => { return OperationState.ExitClearCompleted; } });
		}

		public OperationState Start()
		{
			ConsoleKeyInfo keyInfo;
			OperationState currentOperationState;

			do
			{
				Console.WriteLine(menuLabel);
				for (int i = 0; i < items.Count; ++i)
				{
					Console.WriteLine($"{i + 1}. {items[i].Label}");
				}
				keyInfo = Console.ReadKey(true);
				if (keyInfo.Key < ConsoleKey.D1 || keyInfo.Key > ConsoleKey.D3)
				{
					return OperationState.ERROR_NO_SUCH_ITEM_FOUND;
				}
				currentOperationState = items[(int)Char.GetNumericValue(keyInfo.KeyChar) - 1].Action();
			} while (currentOperationState == OperationState.FuncClearCompleted);

			return currentOperationState;
		}

	}
}
