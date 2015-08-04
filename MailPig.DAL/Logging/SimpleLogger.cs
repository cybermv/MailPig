namespace MailPig.DAL.Logging
{
    using System;
    using System.IO;
    using System.Text;

    public static class SimpleLogger
    {
        private static Stream _stream;
        //private const string logFilesPath = @"F:\MailPigLogFiles";

        static SimpleLogger()
        {
            _stream = new MemoryStream();

            //return;

            //if (!Directory.Exists(logFilesPath))
            //{
            //    Directory.CreateDirectory(logFilesPath);
            //}

            //_stream = new FileStream(
            //    Path.Combine(
            //        logFilesPath,
            //        string.Format("{0}-MailPigLogFile.txt", DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"))),
            //    FileMode.Create);

            //Log("Application started");
        }

        public static void Log(string text)
        {
            string message = string.Format("{0} - {1}", DateTime.Now.ToString("s"), text) + Environment.NewLine;

            LogInternal(message);
        }

        public static void Log(string sender, string text)
        {
            string message = string.Format("{0} - {1} -> {2}",
                DateTime.Now.ToString("s"),
                sender,
                text) + Environment.NewLine;

            LogInternal(message);
        }

        private static void LogInternal(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            _stream.Write(bytes, 0, bytes.Length);
            _stream.Flush();
        }
    }
}