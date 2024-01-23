using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogForU.Core.Appenders.Interfaces;
using LogForU.Core.Enums;
using LogForU.Core.Layout.Interfaces;
using LogForU.Core.Models;

namespace LogForU.Core.Appenders
{
    public class ConsoleAppender : IAppender 

    {
        // TODO: Layout

        public ConsoleAppender(ILayout layout, ReportLevel report = ReportLevel.Info)
        {
            Layout = layout;
            ReportLevel = report;
        }

        public ILayout Layout { get; private set; }
        public ReportLevel ReportLevel { get; set; }
        public int MessagesAppended { get; private set; }
        public void Append(Message message)
        {
            Console.WriteLine(string.Format(Layout.Format, message.CreatedTime, message.ReportLevel, message.Text));
           MessagesAppended++;
        }
    }
}
