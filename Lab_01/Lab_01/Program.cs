using Lab_01.MyMenu;

namespace Lab_01
{
	class Program
	{
		static void Main(string[] args)
		{
			Introduction Intro = new Introduction("Polynomial solver");
			Intro.Greeting();
			Menu mainMenu = new Menu("Use one of the commands: ");
			mainMenu.InitMenuItems();
			mainMenu.Start();
			Intro.Parting();
		}
	}
}
