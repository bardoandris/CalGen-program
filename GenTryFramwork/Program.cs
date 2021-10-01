using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;


namespace GenTryFramwork
{
	static internal class Program
	{
		static readonly string[][] dates = new string[12][];

		static int DayCounter = 1;
		static string PicString(int PicNum) =>
			PicNum < 10 ? "00" + Convert.ToString(PicNum) :
			PicNum < 100 ? "0" + Convert.ToString(PicNum) : Convert.ToString(PicNum);

		private static void Main(string[] args)
		{

			Directory.CreateDirectory("images");
			StringFiller sf = new StringFiller(ref dates);
			Generator ImgGen = new Generator();

			for (int i = 0; i < dates.Length; i++)
			{
				for (int x = 0; x < dates[i].Length; x++)
				{
					ImgGen.Drawing(dates[i][x]).Save($@"images\{PicString(DayCounter++)}.png", ImageFormat.Png);
				}
			}



		}
	}


	class Generator
	{
		private StringFormat stringFormat;
		private Bitmap CurrentIMG;
		private Graphics graphics;
		public Font font;
		Brush TxTBrush;
		Rectangle rect;

		public int[] CalendarIndex { get; } = new int[12];

		public Generator()
		{
			rect = new Rectangle();
			rect.Width = 2000;
			rect.Height = 15;
			rect.X = 230;

			CurrentIMG = new Bitmap(2480, 3508);
			graphics = Graphics.FromImage(CurrentIMG);


			TxTBrush = new SolidBrush(Color.Black);

			CultureInfo CI = new CultureInfo("hu-HU");

			FontFamily fontFamily = new FontFamily("Arial");
			font = new Font(
			  fontFamily,
			  130,
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

		public Bitmap Drawing(string date)
		{


			graphics.Clear(Color.White);
			graphics.DrawString(date, font, TxTBrush, 670, 170, stringFormat);
			for (int i = 0; i < 11; i++)
			{
				rect.Y = 550 + i * 280;
				graphics.FillRectangle(Brushes.Gray, rect);

			}
			return CurrentIMG;
		}
	}
}