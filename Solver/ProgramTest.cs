using System.Text;

public class ProgramTest
{
	public void Bob()
	{
		// See https://aka.ms/new-console-template for more information

		Console.WriteLine("Hello, World!");
		int bufferWidth = Console.BufferWidth;
		int bufferHeight = Console.BufferHeight;

		int windowWidth = Console.WindowWidth; //211
		int windowHeight = Console.WindowHeight;//5
		Console.OutputEncoding = Encoding.UTF8;

		Console.WriteLine(bufferHeight);
		Console.BackgroundColor = ConsoleColor.Blue;
		Console.Write("Buffer Width: ");
		Console.WriteLine(bufferWidth);
		Console.Write("Window Height: ");
		Console.WriteLine(windowHeight);
		Console.Write("Window Width: ");
		Console.WriteLine(windowWidth);
		//Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);


		Console.ForegroundColor = ConsoleColor.DarkGreen;
		Console.Write("Window Width: ▏▎','░'▓'◕'',▋',▊',▉',█',█'");
		Console.CursorLeft = 10;
		Console.CursorTop = 2;
		Console.Title = "Hean pauy";

		DrawingUtils.DrawBox(10, 10, 20, 10, "A box", ConsoleColor.Black, ConsoleColor.DarkBlue, ConsoleColor.White);
	}
}