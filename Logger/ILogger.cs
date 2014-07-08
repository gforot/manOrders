using System;

namespace Logger
{
    public interface ILogger
    {
        void Log();
        void Log(string data);
        void Log(string data, object arg);
        void Log(string data, object arg1, object arg2);
        void Log(string data, object[] args);
        void LogException(string sourceDescription, Exception ex);
    }
}
