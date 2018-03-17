using static System.Console;

namespace Lab_01.Readers
{
	class NumberReader
	{
		public static int ReadInteger(string message) => ReadNum<int>(message, int.TryParse);

		public static double ReadDouble(string message) => ReadNum<double>(message, double.TryParse);

		private delegate bool TryParse<T>(string str, out T value);

		private static T ReadNum<T>(string message, TryParse<T> parseFunc)
		{

			Write("Please, input " + message + ": ");

			T res;
			string strRes = null;

			do
			{
				if (strRes != null)
				{
					WriteLine("Can't parse number. Try again");
				}
				strRes = ReadLine();
			} while (!parseFunc(strRes, out res));

			return res;
		}
	}
}
