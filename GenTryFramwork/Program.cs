using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;

namespace GenTryFramwork
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Dictionary<string, string> Daytrans = new Dictionary<string, string>();

            #region dict

            Daytrans.Add("Monday", "hétfő");
            Daytrans.Add("Tuesday", "kedd");
            Daytrans.Add("Wednesday", "szerda");
            Daytrans.Add("Thursday", "csütörtök");
            Daytrans.Add("Friday", "péntek");
            Daytrans.Add("Saturday", "szombat");
            Daytrans.Add("Sunday", "vasárnap");

            #endregion dict

            Directory.CreateDirectory(@"A:\imgdirectory");
            for (int i = 1; i <= 12; i++)
            {
                Directory.CreateDirectory("A:\\imgdirectory\\" + Convert.ToString(i));
            }
            Generator ImgGen = new Generator();
            CultureInfo CI = new CultureInfo("hu-HU");
            DateTime janFirst = new DateTime(DateTime.Today.Year, 1, 1);
            string[] daysOfWeek = CI.DateTimeFormat.DayNames;
            int weekDayIndex = Array.IndexOf(daysOfWeek, Daytrans[janFirst.DayOfWeek.ToString()]);
            string[] months = CI.DateTimeFormat.MonthNames;
            //Console.Write(janFirst.DayOfWeek.ToString());
            int[] calendarIndex = ImgGen.CalendarIndex;

            for (int i = 0; i <= 11; i++)
            {
                for (int n = 1; n <= calendarIndex[i]; n++)
                {
                    string textToBeWritten = months[i] + " " + n + ", " + daysOfWeek[weekDayIndex];
                    if (weekDayIndex == 6)
                    {
                        weekDayIndex = 0;
                    }
                    else { weekDayIndex++; }
                    Bitmap BMP = ImgGen.Drawing(textToBeWritten);
                    BMP.Save($@"A:\imgdirectory\{i + 1}\{n}.jpg", ImageFormat.Jpeg); // You can rename it to whatever you like
                }
            }
        }
    }

    internal class Generator
    {
        private readonly int[] calendarIndex = new int[12];
        private StringFormat stringFormat;
        public string[] months;
        private Bitmap CurrentIMG;
        private Graphics graphics;
        public Font font;

        public int[] CalendarIndex
        {
            get { return calendarIndex; }
        }

        public Generator()
        {
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
                calendarIndex[i - 1] = CI.Calendar.GetDaysInMonth(DateTime.Today.Year, i);
            }
            string[] months = CI.DateTimeFormat.MonthNames;
        }

        public Bitmap Drawing(string date)
        {
            CurrentIMG = new Bitmap(2480, 3508);
            graphics = Graphics.FromImage(CurrentIMG);
            graphics.Clear(Color.White);
            Brush TxTBrush = new SolidBrush(Color.Black);
            graphics.DrawString(date, font, TxTBrush, 670, 170, stringFormat);
            return CurrentIMG;
        }
    }
}