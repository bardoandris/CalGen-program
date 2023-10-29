using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Diagnostics;
using System.Runtime;
using Microsoft.SqlServer.Server;
using System.Runtime.Remoting.Messaging;


namespace GenTryFramwork
{
	static internal class Program
	{

		static string namesFile = "./Helper/format-namedays/names.txt";
		public static List<string> names = new List<string>(File.ReadAllLines(namesFile));
		static int NextYear = DateTime.Today.Year + 1;
		static readonly string[][] dates = new string[12][];

		
		private static void Main(string[] args)
		{
			if (runNameDayScrpt() == 1) {
				Environment.Exit(1);
			}
			for (int i = 1; i < 13; i++)
			{
				Directory.CreateDirectory($"./images/{i}");
			}
			Directory.CreateDirectory("images");
			StringFiller sf = new StringFiller(dates);
			Generator ImgGen = new Generator();

			for (int i = 0; i < dates.Length; i++)
			{
				for (int x = 0; x < dates[i].Length; x++)
				{
					ImgGen.Drawing(dates[i][x], new DateTime(NextYear, i + 1, x + 1).DayOfYear).Save($@"images\{i + 1}\{x + 1}.jpeg", ImageFormat.Jpeg);
				}
			}



		}
		private static int runNameDayScrpt()
		{
			ProcessStartInfo startInfo = new ProcessStartInfo($"python3", " Helper\\format-namedays\\main.py");
			startInfo.UseShellExecute = true;
			Console.WriteLine("Starting python nameday script");
			try
			{
			Process.Start(startInfo).WaitForExit();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
				Console.WriteLine("Script futtatása nem járt sikerrel! kilépés...");
				return 1;
			}
			return 0;
		}
	}


	class Generator
	{
		private StringFormat stringFormat = new StringFormat();
		private Bitmap CurrentIMG;
		private Graphics graphics;
		public Font font;
		readonly Brush TxTBrush;
		Rectangle rectangle;

		public int[] CalendarIndex { get; } = new int[12];

		public Generator()
		{
			rectangle = new Rectangle()
			{
				Width = 2000,
				Height = 15,
				X = 230
			};

			CurrentIMG = new Bitmap(2480, 3508);
			graphics = Graphics.FromImage(CurrentIMG);


			TxTBrush = new SolidBrush(Color.Black);

			CultureInfo CI = new CultureInfo("hu-HU");

			FontFamily fontFamily = new FontFamily("Arial");
			font = new Font(
			  fontFamily,
			  110,
			  FontStyle.Regular,
			  GraphicsUnit.Pixel
			  );
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = StringAlignment.Center;
			stringFormat.LineAlignment = StringAlignment.Center;
			for (int i = 1; i <= 12; i++)
			{
				CalendarIndex[i - 1] = CI.Calendar.GetDaysInMonth(DateTime.Today.Year + 1, i);
			}
			string[] months = CI.DateTimeFormat.MonthNames;
		}

		public Bitmap Drawing(string date, int dayNumber)
		{


			graphics.Clear(Color.White);
			graphics.DrawString(date + $" ({Program.names[dayNumber - 1]})", font, TxTBrush, 450, 170, stringFormat);
			for (int i = 0; i < 11; i++)
			{
				rectangle.Y = 550 + i * 280;
				graphics.FillRectangle(Brushes.Gray, rectangle);

			}
			return CurrentIMG;
		}
	}
}