using Lab_01.MyMenu;

namespace Lab_01
{
	class Program
	{
		static void Main(string[] args)
		{
			Introduction Intro = new Introduction("Polynomial solver");
			Intro.Greeting();
			SolutionList Solution = new SolutionList();
			Menu mainMenu = new Menu("Use one of the commands");
			mainMenu.InitMenuItems(Solution);
			Intro.Parting(mainMenu.Start());
		}
	}
}
