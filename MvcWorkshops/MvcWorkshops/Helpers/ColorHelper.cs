using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWorkshops.Helpers
{
    public static class ColorHelper
    {
		public static string GetColor()
		{
			var random = new Random();
			var color = String.Format("{0:X6}", random.Next(0x1000000));

			return color;
		}
	}
}
