using System;

namespace VstsMetrics.Commands.CycleTime
{
    public class WorkItemCycleTime
    {
        public int Id { get; private set; }

        public string Title { get; private set; }

        public string Tags { get; private set; }

        public DateTime StartTime { get; private set; }

        public DateTime EndTime { get; private set; }

        public double AbsoluteCycleTimeInHours
        {
            get { return Math.Round(EndTime.Subtract(StartTime).TotalHours, 2); }
        }

        public double BusinessHoursCycleTimeInHours
        {
            get
            {
                return Math.Round(GetWorkingTimeBetween(StartTime, EndTime).TotalHours, 2);               
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

        private TimeSpan GetWorkingTimeBetween(DateTime start, DateTime end)
        {
            //Consider making the 'weekend' days and working hours command line inputs.
            int count = 0;
            for (var i = start; i < end; i = i.AddMinutes(1))
            {
                if (i.DayOfWeek != DayOfWeek.Saturday && i.DayOfWeek != DayOfWeek.Sunday)
                {
                    if (i.TimeOfDay.Hours >= 8 && i.TimeOfDay.Hours < 17)
                    {
                        count++;
                    }
                }
            }
            return TimeSpan.FromMinutes(count);  
        }
    }
}