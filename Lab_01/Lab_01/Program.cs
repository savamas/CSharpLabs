using Lab_01.MyMenu;

namespace Lab_01
{
	public class Program
	{
		static void Main(string[] args)
		{
			Introduction Intro = new Introduction("Polynomial solver");
			Intro.Greeting();
			Menu mainMenu = new MainMenuInit("Use one of the commands: ", new Polynomial(new FullStringPolynomialFormer(),
				                                                                         new PolynomialStringParser()));
			mainMenu.Start();
			Intro.Parting();
		}
	}
}
