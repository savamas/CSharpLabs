using PolynomialLib;
using PolynomialSolver.MyMenu;

namespace PolynomialSolver
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
