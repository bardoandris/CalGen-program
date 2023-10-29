using System;
namespace GenTryFramwork
{
	public static class HolidayDateClass // Returns DateTimes of Easter Sunday, and whitday (pünkösd)
	{
		public static DateTime EasterSunday(int year)
		{
			int day = 0;
			int month = 0;

			int g = year % 19;
			int c = year / 100;
			int h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25) + 19 * g + 15) % 30;
			int i = h - (int)(h / 28) * (1 - (int)(h / 28) * (int)(29 / (h + 1)) * (int)((21 - g) / 11));

			day = i - ((year + (int)(year / 4) + i + 2 - c + (int)(c / 4)) % 7) + 28;
			month = 3;

			if (day > 31)
			{
				month++;
				day -= 31;
			}

			return new DateTime(year, month, day);
		}
		public static DateTime Whitday(int year)
		{
			return EasterSunday(year).AddDays(49);
		}
	}
}
