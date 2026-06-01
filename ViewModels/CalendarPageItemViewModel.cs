using Avalonia;
using System;

namespace DynaRealm.ViewModels
{
    public class CalendarPageItemViewModel : ViewModelBase
    {
        public Guid PageId { get; set; }

        public string Title { get; set; } = string.Empty;

        public Guid TabId { get; set; }

        public string TabName { get; set; } = string.Empty;

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public bool HasTime => StartTime.HasValue;

        public string TabColor
        {
            get
            {
                if (TabName == "予定")
                {
                    return "#E3F2FD";
                }

                if (TabName == "学習")
                {
                    return "#E8F5E9";
                }

                return "#F7F7F7";
            }
        }

        public string TabBorderColor
        {
            get
            {
                if (TabName == "予定")
                {
                    return "#90CAF9";
                }

                if (TabName == "学習")
                {
                    return "#A5D6A7";
                }
                return "#DDDDDD";
            }
        }

        public double TopMargin
        {
            get
            {
                if (!StartTime.HasValue)
                {
                    return 0;
                }

                return StartTime.Value.Minutes / 60.0 * 64;
            }
        }

        public Thickness CardMargin =>
            new Thickness(0, TopMargin + 2, 0, 4);

        public double Height
        {
            get
            {
                if (!StartTime.HasValue || !EndTime.HasValue)
                {
                    return 40;
                }

                var minutes = (EndTime.Value - StartTime.Value).TotalMinutes;

                if (minutes <= 0)
                {
                    return 40;
                }

                return Math.Max(40, minutes / 60 * 64);
            }
        }

        public string TimeText
        {
            get
            {
                if (StartTime.HasValue && EndTime.HasValue)
                {
                    return $"{StartTime.Value:hh\\:mm} - {EndTime.Value:hh\\:mm}";
                }

                if (StartTime.HasValue)
                {
                    return $"{StartTime.Value:hh\\:mm} -";
                }

                return string.Empty;
            }
        }
    }
}
