using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogForU.Core.Appenders.Interfaces;
using LogForU.Core.Enums;
using LogForU.Core.Loggers.Interfaces;
using LogForU.Core.Models;

namespace LogForU.Core.Loggers
{
    public class Logger : ILogger
    {

        private ICollection<IAppender> appenders;

        public Logger(params IAppender[] appenders)
        {
            this.appenders = appenders;
        }
        public void Info(string dateTime, string message)
            => AppendAll(dateTime, message, ReportLevel.Info);

        public void Warning(string dateTime, string message)
            => AppendAll(dateTime, message, ReportLevel.Warning);

        public void Error(string dateTime, string message)
            => AppendAll(dateTime, message, ReportLevel.Error);

        public void Critical(string dateTime, string message)
            => AppendAll(dateTime, message, ReportLevel.Critial);

        public void Fatal(string dateTime, string message)
            => AppendAll(dateTime, message, ReportLevel.Fatal);

        private void AppendAll(string dateTime, string text, ReportLevel reportLevel)
        {
            Message message = new Message(dateTime, text, reportLevel);
            foreach (IAppender appender in appenders)
            {
                if (message.ReportLevel >= appender.ReportLevel)
                {
                    appender.Append(message);
                }

            }
        }
    }
}
