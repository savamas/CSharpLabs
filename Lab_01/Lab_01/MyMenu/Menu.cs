using System;
using System.Collections.Generic;

namespace Lab_01.MyMenu
{
	class Menu
	{
		private string _mLabel;
		private List<MenuItem> _items;

		public Menu(string MenuLabel)
		{
			_items = new List<MenuItem>();
			_mLabel = MenuLabel;
		}

		private void AddMenuItem(MenuItem item)
		{
			_items.Add(item);
		}

		public void InitMenuItems(SolutionList obSolutions)
		{
			AddMenuItem(new MenuItem() { Label = "Solve", Action = obSolutions.Solve });
			AddMenuItem(new MenuItem() { Label = "Find", Action =  obSolutions.Find });
			AddMenuItem(new MenuItem() { Label = "Exit", Action = () => { return OperationState.ExitClearCompleted; } });
		}

		public OperationState Start()
		{
			ConsoleKeyInfo KeyInfo;
			OperationState CurrentOperationState;

			do
			{
				Console.WriteLine(_mLabel);
				for (int i = 0; i < _items.Count; ++i)
				{
					Console.WriteLine($"{i + 1}. {_items[i].Label}");
				}
				KeyInfo = Console.ReadKey(true);
				if (KeyInfo.Key < ConsoleKey.D1 || KeyInfo.Key > ConsoleKey.D3)
				{
					return OperationState.ERROR_NO_SUCH_ITEM_FOUND;
				}
				CurrentOperationState = _items[(int)Char.GetNumericValue(KeyInfo.KeyChar) - 1].Action();
			} while (CurrentOperationState == OperationState.FuncClearCompleted);

			return CurrentOperationState;
		}

	}
}
