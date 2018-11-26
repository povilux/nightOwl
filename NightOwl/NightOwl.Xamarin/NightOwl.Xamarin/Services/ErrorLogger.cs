using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NightOwl.Xamarin.Services
{
    public class ErrorLogger
    {
        private static ErrorLogger _ErrorLogger { get; set; }

        private static readonly Lazy<ErrorLogger> errorLogger =
               new Lazy<ErrorLogger>(() => new ErrorLogger());

        private readonly string exceptionLogFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "exceptionLog.txt");
        private readonly string errorsLogFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "errorsLog.txt");

        private ErrorLogger() {}

        public static ErrorLogger Instance
        {
            get
            {
                return errorLogger.Value;
            }
        }

        public void LogException(Exception ex)
        {
            using (StreamWriter streamWriter = File.AppendText(exceptionLogFilePath))
            {
                streamWriter.WriteLine($"{DateTime.Now} {ex.Message} {Environment.NewLine} {ex.StackTrace} {Environment.NewLine} {Environment.NewLine}");
                streamWriter.Close();
            }
        }

        public void LogError(string error)
        {
            using (StreamWriter streamWriter = File.AppendText(errorsLogFilePath))
            {
                streamWriter.WriteLine($"{DateTime.Now} {error} {Environment.NewLine} {Environment.NewLine} {Environment.NewLine}");
                streamWriter.Close();
            }
        }
    }
}
