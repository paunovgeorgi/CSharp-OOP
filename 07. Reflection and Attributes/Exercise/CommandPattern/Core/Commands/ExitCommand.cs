﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandPattern.Core.Contracts;

namespace CommandPattern.Core.Commands
{
    public class ExitCommand : ICommand
    {
        private const int DefaultExitCode = 0;

        public string Execute(string[] args)
        {
            Environment.Exit(DefaultExitCode);

            return null;
        }
    }
}
