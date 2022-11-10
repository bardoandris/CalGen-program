using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GenTryFramwork
{
	class StringFiller
	{
		CultureInfo CI;
		DateTime janFirst;
		string[] daysOfWeek;
		int weekDayIndex;
		Weekday<string> WD;
		string[] monthnames;

		public StringFiller(string[][] CalendarStrings)
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
			CI = new CultureInfo("hu-HU");

			janFirst = new DateTime(DateTime.Today.Year + 1, 1, 1);        // VIGYÁZZ A +1-EL!!! // PLUS ONE, THEREFORE 
			daysOfWeek = CI.DateTimeFormat.DayNames;                       //SETS NEXT CALENDAR YEAR
			WD = new Weekday<string>(CI.DateTimeFormat.DayNames, Daytrans[janFirst.DayOfWeek.ToString()]);
			monthnames = CI.DateTimeFormat.MonthNames;
			for (int i = 0; i < 12; i++)
			{
				CalendarStrings[i] = MonthResolver(i);
			}
			HolidayResolver(ref CalendarStrings);


		}
		string[] MonthResolver(int month)
		{
			int monthlength = CI.Calendar.GetDaysInMonth(DateTime.Today.Year + 1, month + 1);
			string[] monthstrings = new string[monthlength];
			for (int i = 0; i < monthlength; i++)
			{
				monthstrings[i] = monthnames[month] + " " + (i + 1) + " " + WD.GetWeekday();
			}
			return monthstrings;
		}
		void HolidayResolver(ref string[][] CalendarStrings)
		{
			CalendarStrings[HolidayDateClass.EasterSunday(DateTime.Today.Year + 1).Month - 1][HolidayDateClass.EasterSunday(DateTime.Today.Year + 1).Day - 1] += ", húsvét";
			CalendarStrings[HolidayDateClass.Whitday(DateTime.Today.Year + 1).Month - 1][HolidayDateClass.Whitday(DateTime.Today.Year + 1).Day - 1] += ", pünkösd";
			CalendarStrings[11][24] += ", karácsony";
		}
	}
}
