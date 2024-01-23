using LogForU.Core;
using LogForU.Core.Appenders;
using LogForU.Core.Enums;
using LogForU.Core.Layout;
using LogForU.Core.Loggers;
using LogForU.Core.Loggers.Interfaces;
using LogForU.Core.Models;
using LogForU.Core.Utils;
using System;
using LogForU.ConsoleApp.CustomLayouts;
using LogForU.Core.IO;
using Microsoft.VisualBasic;

namespace LogForU.ConsoleApp
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            var simpleLayout = new SimpleLayout();
            var xmlLayout = new XmlLayout();
            var consoleAppender = new ConsoleAppender(simpleLayout);

            var file = new LogFile();

            var fileAppender = new FileAppender(simpleLayout,file);

            var logger = new Logger(consoleAppender, fileAppender);

            logger.Error("3/26/2015 2:08:11 PM", "Error parsing JSON.");

            logger.Info("3/26/2015 2:08:11 PM", "User Pesho successfully registered.");

        }
    }
}