﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleGame.Infrastructure
{
    public abstract class LogBase
    {
        protected readonly object lockObj = new object();
        public abstract void Log(string message);
    }

    class Logger: LogBase
    {
        private Logger() { }
        private static Logger _logger { get; set; }

        public static Logger GetInstance()
        {
            if (_logger == null)
            {
                _logger = new Logger();
            }
            return _logger;
        }

        public string filePath = @"C:\Users\mylif\source\repos\Projects\ConsoleGame\Log\log.txt";
        public override void Log(string message)
        {
            lock (lockObj)
            {
                using (StreamWriter streamWriter = new StreamWriter(filePath, true, System.Text.Encoding.Default))
                {
                    streamWriter.WriteLine(message);
                    streamWriter.Close();
                }
            }
        }
    }
}
