using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdyllicWeb.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static T ParseEnum<T>(this string value)
        {
            if (value == null)
            {
                value = "Select";
            }
            //if (value != null)
            //{
            //    return (T)Enum.Parse(typeof(T), value, true);
            //}
            return (T)Enum.Parse(typeof(T), value, true);
        }
        public static DateTime ToDateTime(this string dateString)
        {
            DateTime resultDate;
            if (DateTime.TryParse(dateString, out resultDate))
                return resultDate;

            return DateTime.Now;//default
        }
        public static DateTime? ToNullableDateTime(this string dateString)
        {
            if (string.IsNullOrEmpty((dateString ?? "").Trim()))
                return null;

            DateTime resultDate;
            if (DateTime.TryParse(dateString, out resultDate))
                return resultDate;

            return null;
        }

        public static int ToInt32(this string value, int defaultIntValue = 0)
        {
            int parsedInt;
            if (int.TryParse(value, out parsedInt))
            {
                return parsedInt;
            }

            return defaultIntValue;
        }

        public static int? ToNullableInt32(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return value.ToInt32();
        }

        public static long ToInt64(this string value, long defaultInt64Value = 0)
        {
            long parsedInt64;
            if (Int64.TryParse(value, out parsedInt64))
            {
                return parsedInt64;
            }

            return defaultInt64Value;
        }

        public static long? ToNullableInt64(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return value.ToInt64();
        }

        public static bool IsExistsInList(this string value, params string[] names)
        {
            return names.Any(n => n == value);
        }
    }
}