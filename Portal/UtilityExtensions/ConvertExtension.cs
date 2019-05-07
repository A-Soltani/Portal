using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Portal.UtilityExtensions
{
    public static class ConvertExtension
    {
        /// <summary>
        /// Convert a date time object to Unix time representation.
        /// </summary>
        public static long ToUnixTime(this DateTime datetime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)(datetime - sTime).TotalSeconds;
        }

        /// <summary>
        /// تبدیل رشته به عدد
        /// </summary>
        public static Decimal ToDecimal(this String str)
        {
            Decimal result;
            if (Decimal.TryParse(str, out result))
                return result;
            else
                return 0;
        }

        /// <summary>
        /// تبدیل آبجکت به عدد
        /// </summary>
        public static Decimal ToDecimal(this object obj)
        {
            Decimal result;
            if (Decimal.TryParse(obj.ToString(), out result))
                return result;
            else
                return 0;
        }

        /// <summary>
        /// تبدیل رشته به عدد
        /// </summary>
        public static Double ToDouble(this String str)
        {
            Double result;
            if (Double.TryParse(str, out result))
                return result;
            else
                return 0;
        }

        /// <summary>
        /// تبدیل رشته به عدد
        /// </summary>
        public static Int32 ToInt32(this String str)
        {
            Int32 result;
            if (Int32.TryParse(str, out result))
                return result;
            else
                return 0;
        }


        /// <summary>
        /// تبدیل رشته به اعشار
        /// </summary>
        public static float ToFloat(this String str)
        {
            float result;
            if (float.TryParse(str, out result))
                return result;
            else
                return 0;
        }

        /// <summary>
        /// تبدیل آبجکت به عدد
        /// </summary>
        public static Int32 ToInt32(this object obj)
        {
            Int32 result;
            if (Int32.TryParse(obj.ToString(), out result))
                return result;
            else
                return 0;
        }

        /// <summary>
        /// تبدیل رشته به عدد
        /// </summary>
        public static UInt64 ToUInt64(this String str)
        {
            UInt64 result;
            if (UInt64.TryParse(str, out result))
                return result;
            else
                return 0;
        }

        /// <summary>
        /// تبدیل رشته به عدد
        /// </summary>
        public static Int64 ToInt64(this String str)
        {
            Int64 result;
            if (Int64.TryParse(str, out result))
                return result;
            else
                return 0;
        }

        /// <summary>
        /// تبدیل آبجکت به عدد
        /// </summary>
        public static Int64 ToInt64(this object obj)
        {
            Int64 result;
            if (Int64.TryParse(obj.ToString(), out result))
                return result;
            else
                return 0;
        }

        

        /// <summary>
        /// تبدیل آبجکت به بایت
        /// </summary>
        public static Byte ToByte(this object obj)
        {
            Byte result;
            if (Byte.TryParse(obj.ToString(), out result))
                return result;
            else
                return 0;
        }

        /// <summary>
        /// تبدیل رشته به بایت
        /// </summary>
        public static Byte ToByte(this String str)
        {
            Byte result;
            if (Byte.TryParse(str, out result))
                return result;
            else
                return 0;
        }

        /// <summary>
        /// فرمت تاریخ شمسی
        /// </summary>
        public static string SetShamsiDateFormat(this String date)
        {
            if (date.Length == 8)
                return date.Substring(0, 4) + "/" + date.Substring(4, 2) + "/" + date.Substring(6, 2);
            else
                return date;
        }

        /// <summary>
        /// تبدیل تاریخ شمسی به تاریخ میلادی
        /// </summary>
        public static DateTime ToMiladiDateTime(this String str, bool SetToEndDay)
        {
            DateTime miladyDate;
            if (str.Length == 8)
                str = SetShamsiDateFormat(str);

            //PersianCalendar pc = new PersianCalendar();
            //if (str.Length == 10 && str.Substring(4, 1) == "/" && str.Substring(7, 1) == "/")
            //{
            //    Int32 year = str.Substring(0, 4).ToInt32();
            //    Int32 month = str.Substring(5, 2).ToInt32();
            //    Int32 day = str.Substring(8, 2).ToInt32();

            //    try
            //    {
            //        if (month == 12 && day == 30)
            //        {
            //            miladyDate = pc.ToDateTime(year, month, 29, 0, 0, 0, 0);
            //            miladyDate = miladyDate.AddDays(1);
            //        }
            //        else
            //            miladyDate = pc.ToDateTime(year, month, day, 0, 0, 0, 0);
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}
            ////else
            ////    miladyDate = Helper.CurrentDateTime();

            ////if (SetToEndDay)
            ////{
            ////    miladyDate = new DateTime(miladyDate.Year, miladyDate.Month, miladyDate.Day, 23, 59, 59, 999); // در این حالت اس کیوال زمان را به 00:00:00:000 رند می کند
            ////    //MiladyDate = new DateTime(MiladyDate.Year, MiladyDate.Month, MiladyDate.Day, 23, 59, 59, 997); // در این حالت 3 صدم ثانیه آخر را خروجی نمی دهد در نتیجه در گزارشات به خطا می خورد
            ////}

            //return miladyDate;
            return DateTime.Now;
        }

        private static string toShamsiDate(DateTime? date)
        {
            if (date == null)
                return String.Empty;

            DateTime inputDate = (DateTime)date;
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(inputDate) + "/" + pc.GetMonth(inputDate).ToString("00") + "/" + pc.GetDayOfMonth(inputDate).ToString("00");
        }

        /// <summary>
        /// تبدیل تاریخ میلادی به تاریخ شمسی
        /// </summary>
        public static string ToShamsiDate(this DateTime? date)
        {
            return toShamsiDate(date);
            //PersianCalendar pc = new PersianCalendar();
            //return pc.GetYear(date).ToString() + "/" + pc.GetMonth(date).ToString("00") + "/" + pc.GetDayOfMonth(date).ToString("00");
        }

        /// <summary>
        /// تبدیل تاریخ میلادی به تاریخ شمسی
        /// </summary>
        public static string ToShamsiDate(this DateTime date)
        {
            return toShamsiDate(date);
            //PersianCalendar pc = new PersianCalendar();
            //return pc.GetYear(date).ToString() + "/" + pc.GetMonth(date).ToString("00") + "/" + pc.GetDayOfMonth(date).ToString("00");
        }

        public static string ToShamsiDateTime(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return toShamsiDate(date) + "  " + pc.GetHour(date).ToString("00") + ":" + pc.GetMinute(date).ToString("00") + ":" + pc.GetSecond(date).ToString("00");
        }

        /// <summary>
        /// تبدیل لیست به جدول
        /// </summary>
        public static DataTable ToDataTable<T>(this IEnumerable<T> list)
        {
            Type elementType = typeof(T);
            using (var dataTable = new DataTable())
            {
                PropertyInfo[] properties = elementType.GetProperties();
                foreach (PropertyInfo propInfo in properties)
                {
                    Type propertyType = propInfo.PropertyType;
                    Type colType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
                    dataTable.Columns.Add(propInfo.Name, colType);
                }

                foreach (T item in list)
                {
                    DataRow row = dataTable.NewRow();
                    foreach (PropertyInfo propInfo in properties)
                    {
                        row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                    }

                    dataTable.Rows.Add(row);
                }

                return dataTable;
            }
        }

        public static DataTable SystemTypeListToDataTable<T>(this IEnumerable<T> list)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Item", typeof(int));
            foreach (T item in list)
            {
                DataRow row = dataTable.NewRow();
                row["Item"] = item;
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        private static ArrayList GetDayOfWeek()
        {
            ArrayList Days = new ArrayList();
            Days.Add("یک شنبه");
            Days.Add("دو شنبه");
            Days.Add("سه شنبه");
            Days.Add("چهار شنبه");
            Days.Add("پنج شنبه");
            Days.Add("جمعه");
            Days.Add("شنبه");
            return Days;
        }

        private static string toLongShamsiDate(DateTime date)
        {
            return "";
        }

        /// <summary>
        /// تبدیل تاریخ میلادی به تاریخ شمسی
        /// </summary>
        public static string ToLongShamsiDate(this DateTime date)
        {
            return toLongShamsiDate(date);
        }

        public static string ToLongShamsiDateTime(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return toLongShamsiDate(date) + " - ساعت : " + pc.GetHour(date).ToString("00") + ":" + pc.GetMinute(date).ToString("00") + ":" + pc.GetSecond(date).ToString("00");
        }



        public static string ToFinglish(this string persianText)
        {
            string result = string.Empty;
            for (int index = 0; index < persianText.Length; index++)
                result = result + FinglishChars.Where(t => t.Key == persianText[index]).Select(t => t.Value).SingleOrDefault() ?? "";
            return result;
        }

        public static List<KeyValuePair<char, string>> FinglishChars = new List<System.Collections.Generic.KeyValuePair<char, string>>()
        {
            new KeyValuePair<char, string>('و' ,  "o"),
            new KeyValuePair<char, string>('ک' ,  "k"),
            new KeyValuePair<char, string>('ج'  , "j"),
            new KeyValuePair<char, string>('پ'  , "p"),
            new KeyValuePair<char, string>('چ'  , "ch"),
            new KeyValuePair<char, string>('ش'  , "sh"),
            new KeyValuePair<char, string>('ذ'  , "z" ),
            new KeyValuePair<char, string>('ز'  , "z" ),
            new KeyValuePair<char, string>('ي'  , "i" ),
            new KeyValuePair<char, string>('ی'  , "i" ),
            new KeyValuePair<char, string>('ث'  , "s" ),
            new KeyValuePair<char, string>('ب'  , "b" ),
            new KeyValuePair<char, string>('ل'  , "l" ),
            new KeyValuePair<char, string>('ا'  , "a" ),
            new KeyValuePair<char, string>('ه'  , "h" ),
            new KeyValuePair<char, string>('ت'  , "t" ),
            new KeyValuePair<char, string>('ن'  , "n" ),
            new KeyValuePair<char, string>('م'  , "m" ),
            new KeyValuePair<char, string>('ئ'  , "e" ),
            new KeyValuePair<char, string>('د'  , "d" ),
            new KeyValuePair<char, string>('خ'  , "kh"),
            new KeyValuePair<char, string>('ح'  , "h" ),
            new KeyValuePair<char, string>('ض'  , "z" ),
            new KeyValuePair<char, string>('ق'  , "gh"),
            new KeyValuePair<char, string>('س'  , "s" ),
            new KeyValuePair<char, string>('ف'  , "f" ),
            new KeyValuePair<char, string>('ع'  , "a" ),
            new KeyValuePair<char, string>('ر'  , "r" ),
            new KeyValuePair<char, string>('ص'  , "s" ),
            new KeyValuePair<char, string>('ط'  , "t" ),
            new KeyValuePair<char, string>('غ'  , "gh"),
            new KeyValuePair<char, string>('ظ'  , "z" ),
            new KeyValuePair<char, string>('ژ'  , "j" ),
            new KeyValuePair<char, string>('گ'  , "g" ),
            new KeyValuePair<char, string>('آ'  , "a"),
            new KeyValuePair<char, string>(' '  , " "),
            new KeyValuePair<char, string>('.'  , "." ),
            new KeyValuePair<char, string>('ك'  , "k" ),
            new KeyValuePair<char, string>('ء'  , "" )

        };

    }
}