using System;
using System.IO;
using System.Text;

namespace Logger
{
    public class LogFile
    {
        Object thisLock = new Object();
        private string _fileName = null;

        internal void CreateFile(string fileName, bool append)
        {
            _fileName = fileName;
        }

        internal void Append(string data)
        {
            lock (thisLock)
            {
                using (StreamWriter _logSW = new StreamWriter(_fileName, true, Encoding.UTF8))
                {
                    _logSW.Write(data);
                    _logSW.Flush();
                }
            }
        }

        internal void AppendLn(string data)
        {
            //Append(data + "\r");
            Append(data + Environment.NewLine);
        }
    }
}
