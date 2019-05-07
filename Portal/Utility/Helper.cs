using Portal.UtilityExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Portal.Utility
{
    public class Helper
    {
        public enum DateType
        {
            StartDate, EndDate
        }

        public static string ReplaceArabicChar(string inputString)
        {
            string outputString = inputString;

            try
            {
                outputString = inputString
                    .Replace(" ", "")
                    .Replace(",", "")
                    .Replace("ي", "ی")
                    .Replace("ک", "ك");
            }
            catch (Exception ex)
            {
                //LogManagement.SaveError(ex);
            }

            return outputString;
        }

        #region Fields

        private static byte[] _optionalEntropy = { 8, 9, 9, 7, 6 };

        #endregion Fields

        public static string CurrentIPAddress
        {
            get
            {
                string ipAddress;
                try
                {
                    HttpContext context = HttpContext.Current;
                    ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                    if (!string.IsNullOrEmpty(ipAddress))
                    {
                        string[] addresses = ipAddress.Split(',');
                        if (addresses.Length != 0)
                        {
                            ipAddress = addresses[0];
                        }
                    }
                    else
                        ipAddress = context.Request.ServerVariables["REMOTE_ADDR"];
                }
                catch (Exception ex)
                {
                    ipAddress = HttpContext.Current.Request.UserHostAddress;

                    //LogManagement.SaveError(ex, "CurrentIPAddress");
                }

                return ipAddress;
            }
        }

        public static bool IsLocalIP(string ipAddress)
        {
            return ipAddress.StartsWith("192.168") || // کلیه لکال ها و اینترانتی ها
                   ipAddress.StartsWith("172.16") || // کلیه لکال ها و اینترانتی ها
                   ipAddress.StartsWith("172.2.2") || // VPN
                   ipAddress.StartsWith("127.0.0") || // Local
                   ipAddress.StartsWith("93.118.126.149") || // آی پی دستگاه آقای شهیدی در منزل
                   ipAddress.StartsWith("93.118.127.15") || // آی پی دستگاه آقای شهیدی در ساختمان رضا
                   ipAddress.StartsWith("::1");
        }

        public static string GetPageURL()
        {
            return HttpContext.Current.Request.Url.PathAndQuery;
        }

        /// <summary>
        /// تاریخ میلادی روز 
        /// </summary>
        public static DateTime CurrentDateTime()
        {
            return DateTime.Now;
            //string str = DateTime.Now.ToString("dddd dd MMMM yyyy - HH:MM");
            //return str;
        }

        /// <summary>
        /// تاریخ شمسی روز 
        /// </summary>

        /// <summary>
        /// دوره ماهانه 
        /// </summary>

        public static double DiffMillisecondsOfDates(DateTime startDate, DateTime endDate)
        {
            TimeSpan span = endDate - startDate;
            return span.TotalMilliseconds;
        }

        public static Int32 DiffDayofDates(DateTime StartDate, DateTime EndDate)
        {
            System.TimeSpan span = StartDate - EndDate;
            return Math.Abs((int)span.TotalDays);
        }

        public static DateTime AddTimeToDate(DateTime date, string time, DateType dateType)
        {
            if (!string.IsNullOrEmpty(time))
            {
                string[] GetTime = time.Split(':');
                int Hours = GetTime[0].ToInt32();
                int Minutes = GetTime[1].ToInt32();

                if (dateType == DateType.EndDate && Hours == 23 && Minutes == 59)
                {
                    TimeSpan endTimeSpan = new TimeSpan(1, 0, 0, 0, 000);
                    date = date.Date.Add(endTimeSpan);
                }
                else
                {
                    date = new DateTime(date.Year, date.Month, date.Day, Hours, Minutes, 0);
                }
            }

            return date;
        }

        public static Boolean IsNumeric(String number)
        {
            Int64 result;
            return Int64.TryParse(number, out result);
        }

        public static string GenerateRandomStringNumber(int lenght)
        {
            string number = "";
            int randNumber = 0;
            Random random = new Random();
            for (int i = 0; i < lenght; i++)
            {
                randNumber = random.Next(0, 9);
                if (i == 0 && randNumber == 0)
                    randNumber = random.Next(1, 9);
                number += randNumber.ToString();
            }
            return number;
        }

        public static Int32 SubtractMinuts(DateTime Date1, DateTime Date2)
        {
            TimeSpan timeSpan = Date2.Subtract(Date1);
            return (timeSpan.Days * 24 * 60) + (timeSpan.Hours * 60) + timeSpan.Minutes;
        }

        /// <summary>
        /// دانلود فایل ها
        /// </summary>
        public static void DownloadFile(string fileName)
        {
            string path = fileName;
            if (path.StartsWith("~"))
                path = HttpContext.Current.Server.MapPath(path);
            string name = System.IO.Path.GetFileName(path);
            string ext = System.IO.Path.GetExtension(path);
            string type = "";
            // set known types based on file extension  
            if (ext != null)
            {
                switch (ext.ToLower())
                {
                    case ".htm":
                    case ".html":
                        type = "Text/HTML";
                        break;

                    case ".txt":
                        type = "Text/Plain";
                        break;

                    case ".doc":
                    case ".docx":
                    case ".rtf":
                        type = "Application/msWord";
                        break;

                    case ".xlsx":
                    case ".xls":
                        type = "Application/msExcel";
                        break;

                    case ".pdf":
                        type = "Application/adobeAcrobat";
                        break;

                    case ".zip":
                        type = "Application/zipArchive";
                        break;

                    case ".csv":
                        type = "text/csv";
                        break;

                    case ".jpg":
                        type = "image/jpeg";
                        break;
                }
            }

            HttpContext.Current.Response.AppendHeader("content-disposition", "attachment; filename=" + name);

            if (type != string.Empty)
                HttpContext.Current.Response.ContentType = type;

            HttpContext.Current.Response.WriteFile(path);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static string GenerateRandomFileName()
        {
            return Path.GetRandomFileName();
        }

        public static string StringFormatDate(string date)
        {
            switch (date.Length)
            {
                case 8:
                    return date.Substring(0, 4) + "/" + date.Substring(4, 2) + "/" + date.Substring(6, 2);
                case 6:
                    return date.Substring(0, 4) + "/" + date.Substring(4, 2);
            }

            return date;
        }

        public static string GetFileName(string fileName)
        {
            if (fileName.StartsWith("~"))
                return HttpContext.Current.Server.MapPath(fileName);
            else
                return fileName;
        }

        public static string GetFileNameFromFilePath(string FilePath)
        {
            FilePath = GetFileName(FilePath);
            string FileName = System.IO.Path.GetFileName(FilePath);
            return FileName;
        }

        //public static string ExecuteProgram(string Program)
        //{

        //    System.Diagnostics.ProcessStartInfo IranCellProcess = new System.Diagnostics.ProcessStartInfo(Program);
        //    IranCellProcess.RedirectStandardOutput = true;
        //    IranCellProcess.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        //    IranCellProcess.UseShellExecute = false;
        //    System.Diagnostics.Process listFiles;

        //    IranCellProcess.WorkingDirectory = Settings.CurrentPath;
        //    IranCellProcess.Domain = ".";
        //    IranCellProcess.UserName = "vahid";

        //    System.String rawPassword = "per112";
        //    System.Security.SecureString encPassword = new System.Security.SecureString();
        //    foreach (System.Char c in rawPassword)
        //    {
        //        encPassword.AppendChar(c);
        //    }

        //    IranCellProcess.Password = encPassword;


        //    listFiles = System.Diagnostics.Process.Start(IranCellProcess);
        //    System.IO.StreamReader myOutput = listFiles.StandardOutput;
        //    listFiles.WaitForExit(2000);
        //    string output = "";
        //    if (listFiles.HasExited)
        //    {
        //        output = myOutput.ReadToEnd();
        //    }

        //    return output;


        //    //try
        //    //{
        //    //    System.Diagnostics.Process IranCellProcess = new System.Diagnostics.Process();
        //    //    IranCellProcess.StartInfo.WorkingDirectory = Common.Settings.CurrentPath;
        //    //    IranCellProcess.StartInfo.FileName = Program;
        //    //    IranCellProcess.StartInfo.UserName = "vahid";

        //    //    System.String rawPassword = "per112";
        //    //    System.Security.SecureString encPassword = new System.Security.SecureString();
        //    //    foreach (System.Char c in rawPassword)
        //    //    {
        //    //        encPassword.AppendChar(c);
        //    //    }

        //    //    IranCellProcess.StartInfo.Password = encPassword;

        //    //     //The UseShellExecute flag must be turned off in order to supply a password:
        //    //    IranCellProcess.StartInfo.UseShellExecute = false;

        //    //    IranCellProcess.StartInfo.Domain = ".";
        //    //    IranCellProcess.Start();
        //    //    return true;
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    //System.IO.StreamReader sRd = System.IO.File.OpenText(Portal.Common.Settings.CurrentPath + "\\bin\\error.Txt");
        //    //    //System.IO.File.WriteAllText(Portal.Common.Settings.CurrentPath + "\\bin\\error.Txt", ex.ToString());
        //    //    Common.LogManagement.SaveError(ex.ToString(), "بروز خطا");
        //    //    return false;
        //    //}
        //}


        public static Int64 GetFileSize(string FilePath)
        {
            Int64 Bytes = new FileInfo(FilePath).Length;
            return Bytes;
        }



        public static List<string> GetMounts()
        {
            List<string> Mounts = new List<string>();
            Mounts.Add("فروردین");
            Mounts.Add("اردیبهشت");
            Mounts.Add("خرداد");
            Mounts.Add("تیر");
            Mounts.Add("مرداد");
            Mounts.Add("شهریور");
            Mounts.Add("مهر");
            Mounts.Add("آبان");
            Mounts.Add("آذر");
            Mounts.Add("دی");
            Mounts.Add("بهمن");
            Mounts.Add("اسفند");
            return Mounts;
        }

        #region Enum
        public static List<KeyValuePair<string, string>> GetEnumList<T>()
        {
            var list = new List<KeyValuePair<string, string>>();
            foreach (var e in Enum.GetValues(typeof(T)))
            {
                list.Add(new KeyValuePair<string, string>(e.ToString(), (string)e));
            }
            return list;
        }

        public static string GetEnumNameById<T>(object id)
        {
            return Enum.GetName(typeof(T), id);
        }
        #endregion

    }
}