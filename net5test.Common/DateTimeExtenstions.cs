using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net5test.Common
{
    public static class DateTimeExtenstions
    {
        /// <summary>
        ///   UTC時間轉換成秒數。
        /// </summary>
        public static long ToUnixTimestamp(this DateTime d)
        {
            var epoch = d - new DateTime(1970, 1, 1, 0, 0, 0);

            return (long)epoch.TotalSeconds;
        }
        /// <summary>
        ///   秒數轉換成UTC時間。
        /// </summary>
        public static DateTime FromUnixTimestamp(this long t)
        {
            DateTime dt = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(t);

            return dt;
        }
        /// <summary>
        ///   UTC+8時間轉換成秒數。
        /// </summary>
        public static long ToUnixTimestampTW(this DateTime d)
        {
            var epoch = d - new DateTime(1970, 1, 1, 0, 0, 0).AddHours(8);

            return (long)epoch.TotalSeconds;
        }
        /// <summary>
        ///   秒數轉換成UTC+8時間。
        /// </summary>
        public static DateTime FromUnixTimestampTW(this long t)
        {
            DateTime dt = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(t).AddHours(8);

            return dt;
        }
    }
}
