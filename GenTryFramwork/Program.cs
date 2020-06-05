using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;

namespace GenTryFramwork
{
     static internal class Program
    {
        static string PicString(int PicNum) =>
            PicNum < 10 ? "00"+ Convert.ToString(PicNum) :
            PicNum < 100 ? "0" + Convert.ToString(PicNum) : Convert.ToString(PicNum);

       static bool GoodDrive = false;
        static char DriveLetter;
        private static void Main(string[] args)
        {

            do
            {
                Console.WriteLine("Which Drive Letter Should it go into");
                DriveLetter = Char.ToUpper(Console.ReadKey().KeyChar);
                if (Directory.Exists(DriveLetter + @":\"))
                {
                    GoodDrive = true;
                }
                else
                {
                    Console.WriteLine("Not Valid Drive Letter");
                }
            } while (GoodDrive != true);


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

            Directory.CreateDirectory(DriveLetter + @":\imgdirectory");
            for (int i = 1; i <= 12; i++)
            {
                Directory.CreateDirectory(DriveLetter + ":\\imgdirectory\\" + Convert.ToString(i));
            }
            Generator ImgGen = new Generator();
            CultureInfo CI = new CultureInfo("hu-HU");
            DateTime janFirst = new DateTime(DateTime.Today.Year + 1, 1, 1);        // VIGYÁZZ A +1-EL!!!
            int NumofDays = CI.Calendar.GetDaysInYear(DateTime.Today.Year + 1);
            string[] daysOfWeek = CI.DateTimeFormat.DayNames;
            int weekDayIndex = Array.IndexOf(daysOfWeek, Daytrans[janFirst.DayOfWeek.ToString()]);
            string[] months = CI.DateTimeFormat.MonthNames;
            //Console.Write(janFirst.DayOfWeek.ToString());
            int[] calendarIndex = ImgGen.CalendarIndex;
            int DayIndex = 1;
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
                    BMP.Save($@"{DriveLetter}:\imgdirectory\{PicString(DayIndex++) }.jpg", ImageFormat.Jpeg); // You can rename it to whatever you like
                    
                }

            }        }
    }

    internal class Generator
    {
        private readonly int[] calendarIndex = new int[12];
        private StringFormat stringFormat;
        public string[] months;
        private Bitmap CurrentIMG;
        private Graphics graphics;
        public Font font;
        Brush TxTBrush;
        Rectangle rect;

        public int[] CalendarIndex
        {
            get { return calendarIndex; }
        }

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
                calendarIndex[i - 1] = CI.Calendar.GetDaysInMonth(DateTime.Today.Year + 1, i);
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