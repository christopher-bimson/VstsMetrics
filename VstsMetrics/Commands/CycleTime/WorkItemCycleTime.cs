﻿using System;

namespace VstsMetrics.Commands.CycleTime
{
    public class WorkItemCycleTime
    {
        public int Id { get; private set; }

        public string Title { get; private set; }

        public string Tags { get; private set; }

        public DateTime StartTime { get; private set; }

        public DateTime EndTime { get; private set; }

        public double ElapsedCycleTimeInHours
        {
            get { return Math.Round(GetElapsedTimeExcludingWeekendsBetween(StartTime, EndTime).TotalHours, 2); }
        }

        public double ApproximateWorkingCycleTimeInHours
        {
            get
            {
                return Math.Round(GetElapsedWorkingTimeBetween(StartTime, EndTime).TotalHours, 2);               
            }
        }

        public WorkItemCycleTime(int id, string title, string tags, 
            DateTime startTime, DateTime endTime)
        {
            Id = id;
            Title = title;
            Tags = tags;
            StartTime = startTime;
            EndTime = endTime;
        }

        private TimeSpan GetElapsedTimeExcludingWeekendsBetween(DateTime start, DateTime end)
        {
            return CountTimeBetween(start, end, NotTheWeekend);
        }

        private TimeSpan GetElapsedWorkingTimeBetween(DateTime start, DateTime end)
        {
            return CountTimeBetween(start, end, OnlyWorkingHoursCount);
        }

        private TimeSpan CountTimeBetween(DateTime start, DateTime end, Func<DateTime, bool> shouldThisMinuteCount)
        {
            var count = 0;
            for (var dateTime = start; dateTime < end; dateTime = dateTime.AddMinutes(1))
            {
                if (shouldThisMinuteCount(dateTime))
                    count++;
            }
            return TimeSpan.FromMinutes(count);
        }

        private bool OnlyWorkingHoursCount(DateTime dateTime)
        {
            return NotTheWeekend(dateTime) && (ItIsBeforeLunch(dateTime) || ItIsAfterLunch(dateTime));
        }

        private bool NotTheWeekend(DateTime dateTime)
        {
            return (dateTime.DayOfWeek != DayOfWeek.Saturday && dateTime.DayOfWeek != DayOfWeek.Sunday);
        }

        private static bool ItIsAfterLunch(DateTime dateTime)
        {
            return dateTime.TimeOfDay.Hours >= 13 && dateTime.TimeOfDay.Hours < 17;
        }
        
        private static bool ItIsBeforeLunch(DateTime dateTime)
        {
            return dateTime.TimeOfDay.Hours >= 8 && dateTime.TimeOfDay.Hours < 12;
        }
    }
}