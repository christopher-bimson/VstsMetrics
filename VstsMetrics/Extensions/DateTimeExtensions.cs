using System;

namespace VstsMetrics.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }

        public static bool IsNewerThan(this DateTime date, DateTime? sinceDate)
        {
            if (sinceDate == null)
                return true;

            return date >= sinceDate;
        }
    }
}