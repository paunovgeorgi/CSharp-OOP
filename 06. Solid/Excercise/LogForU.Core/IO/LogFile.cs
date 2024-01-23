using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogForU.Core.Exceptions;
using LogForU.Core.IO.Interfaces;

namespace LogForU.Core.IO
{
    public class LogFile : ILogFile
    {
        private static readonly string DefaultName = $"Log_{DateTime.Now:yyyy-MM-dd-HH-mm-ss}";
        private const string DefaultExtension = "txt";
        private static readonly string DefaultPath = $"{Directory.GetCurrentDirectory()}";

        public LogFile()
        {
            Name = DefaultName;
            Extension = DefaultExtension;
            Path = DefaultPath;
        }

        public LogFile(string name, string extension, string path) : this()
        {
            Name = name;
            Extension = extension;
            Path = path;
        }

        private string name;
        private string extension;
        private string path;


        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new EmptyFileNameException();
                }

                name = value;
            }
            
        }

        public string Extension
        {
            get => extension;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new EmptyFileExtensionException();
                }

                extension = value;
            }
        }

        public string Path
        {
            get => path;
            set
            {
                if (!Directory.Exists(value))
                {
                    throw new InvalidPathException();
                }
                path = value;
            }
        }
        public string FullPath => System.IO.Path.GetFullPath($"{Path}/{Name}.{Extension}");
        public int Size => File.ReadAllText(FullPath).Length;
    }
}
