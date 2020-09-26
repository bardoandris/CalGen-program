using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenTryFramwork
{
	class Weekday<Type>
	{
		Type[] Days;
		int index;
		public Weekday(Type[] DaysOfWeek, string Janfirst)
		{
			Days = DaysOfWeek; index =  Array.IndexOf(Days, Janfirst);
		}
		public Type GetWeekday()
		{
			if (index == 6)
			{
				index = 0;
				return Days[6];
			}
			else
			{
				return Days[index++];
			}
		}
	}
}
