using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogForU.Core.Appenders.Interfaces;
using LogForU.Core.Enums;
using LogForU.Core.IO.Interfaces;
using LogForU.Core.Layout.Interfaces;
using LogForU.Core.Models;

namespace LogForU.Core.Appenders
{
    public class FileAppender : IAppender
    {
        public FileAppender(ILayout layout,ILogFile logfile, ReportLevel report = ReportLevel.Info)
        {
            LogFile = logfile;
            Layout = layout;
            ReportLevel = report;
        }

        public ILayout Layout { get; private set; }
        public ILogFile LogFile { get; private set; }
        public ReportLevel ReportLevel { get; set; }
        public int MessagesAppended { get; private set; }
        public void Append(Message message)
        {
           string content = string.Format(Layout.Format, message.CreatedTime, message.ReportLevel, message.Text) + Environment.NewLine;

           File.AppendAllText(LogFile.FullPath, content);
            MessagesAppended++;
        }
    }
}
