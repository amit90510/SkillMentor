using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Serilog;
using System.Reflection;
using System.IO;
using Utilities;

namespace Service
{
    public static class Logger
    {

        public static void Log(string logMessage, Serilog.Events.LogEventLevel logEventLevel = Serilog.Events.LogEventLevel.Information)
        {
            var filePath = $"{Constant.LogFilePathDirectory + System.DateTime.Today.ToString("dd-MM-yyyy")}.txt";
            var log = new LoggerConfiguration().WriteTo.File(filePath).CreateLogger();

            switch (logEventLevel)
            {
                case Serilog.Events.LogEventLevel.Verbose:
                    log.Verbose(messageTemplate: logMessage);
                    break;
                case Serilog.Events.LogEventLevel.Debug:
                    log.Debug(messageTemplate: logMessage);
                    break;
                case Serilog.Events.LogEventLevel.Information:
                    log.Information(messageTemplate: logMessage);
                    break;
                case Serilog.Events.LogEventLevel.Warning:
                    log.Warning(messageTemplate: logMessage);
                    break;
                case Serilog.Events.LogEventLevel.Error:
                    log.Error(messageTemplate: logMessage);
                    break;
                case Serilog.Events.LogEventLevel.Fatal:
                    log.Fatal(messageTemplate: logMessage);
                    break;
            }
        }
    }
}