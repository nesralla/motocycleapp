using System;

namespace Motocycle.Infra.CrossCutting.Commons.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime? SetKindUtc(this DateTime? dateTime)
        {
            if (dateTime.HasValue)
                return dateTime.Value.SetKindUtc();

            return null;
        }

        public static DateTime SetKindUtc(this DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Utc) { return dateTime; }
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
        }

        public static DateTime SetTime(this DateTime dateTime, int hour, int min, int sec)
            => dateTime.AddHours(hour).AddMinutes(min).AddSeconds(sec);
    }
}
