using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogForU.Core.Enums;
using LogForU.Core.Layout.Interfaces;
using LogForU.Core.Models;

namespace LogForU.Core.Appenders.Interfaces
{
    public interface IAppender
    {

        // TODO: Layout

        ILayout Layout { get; }
        ReportLevel ReportLevel { get; set; }

        int MessagesAppended { get; }

        void Append(Message message);
    }
}
