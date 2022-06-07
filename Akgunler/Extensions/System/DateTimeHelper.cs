using System;
using System.Globalization;
using System.Linq;

namespace Akgunler.Extensions
{
    public static class DateTimeHelper
    {
        public static long ToUnixEpochDate(this DateTime value) => new DateTimeOffset(value).ToUniversalTime().ToUnixTimeSeconds();

        public static DateTime ToSmallDateTimeMinValue(this DateTime sqlDateTime) => new DateTime(1900, 01, 01, 00, 00, 00);

        public static DateTime ToSmallDateTimeMaxValue(this DateTime sqlDateTime) => new DateTime(2079, 06, 06, 23, 59, 00);
    }
}
