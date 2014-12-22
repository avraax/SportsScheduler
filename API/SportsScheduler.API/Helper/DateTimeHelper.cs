using System;

namespace SportsScheduler.API.Helper
{
    public class DateTimeHelper
    {
        public static DateTime FromMillisecondsSinceUnixEpoch(long milliseconds)
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return unixEpoch.AddMilliseconds(milliseconds);
        }
    }
}