using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    //@M|1.0.13|dt|aggiunta evento logging - utile per ProtocolManager
    public delegate void OnLogHandler(int curLevel, string text);

    public class Logger : ILogger
    {
        public static List<Logger> AllLoggers = new List<Logger>();

        public event OnLogHandler OnLog;

        private readonly LogFile _logFile = new LogFile();

        public enum LineType
        {
            SLL,
            MLL
        }


        #region log level
        public enum LogLevelType
        {
            //cs|introdotti per TestDriveManager eliminando commenti/dubbi iniziali
            OFF = 0,
            ALL = 1,
            ERROR = 2,
            INFO = 3,
            DEBUG = 4,
        }

        //@M|1.0.39|cs|modificato accesso a proprieta' is...Enabled
        public bool IsDebugEnabled
        {
            get;
            private set;
        }
        public bool IsInfoEnabled
        {
            get;
            private set;
        }
        public bool IsErrorEnabled
        {
            get;
            private set;
        }

        private void SetLevelEnabled()
        {
            LogLevelType llt = (LogLevelType)_logLevel;
            switch (llt)
            {
                case LogLevelType.ALL:
                case LogLevelType.DEBUG:
                    IsDebugEnabled = true;
                    IsInfoEnabled = true;
                    IsErrorEnabled = true;
                    break;
                case LogLevelType.ERROR:
                    IsDebugEnabled = false;
                    IsInfoEnabled = false;
                    IsErrorEnabled = true;
                    break;
                case LogLevelType.INFO:
                    IsDebugEnabled = false;
                    IsInfoEnabled = true;
                    IsErrorEnabled = true;
                    break;
                case LogLevelType.OFF:
                    IsDebugEnabled = false;
                    IsInfoEnabled = false;
                    IsErrorEnabled = false;
                    break;
            }
        }

        public void SetLevel(LogLevelType levelt)
        {
            _logLevel = (int)levelt;
            SetLevelEnabled();
        }

        private int _logLevel;
        public void SetLevel(int level)
        {
            int oldLogLevel = _logLevel;
            _logLevel = level;
            SetLevelEnabled();

            //LR added because user can modify log level at run time, using the back office
            if (oldLogLevel == 0 && _logLevel > 0 && !String.IsNullOrEmpty(_logOutputPath))
            {
                BuildLogFile();
            }

        }
        public int GetLevel()
        {
            return _logLevel;
        }
        #endregion

        //@N|1.0.5|dt|aggiunto metodo SetPasswordsHidden per nascondere passwords nei log
        #region pwdHide
        //@N|1.0.6|dt|password nascoste in modalità RELEASE, altrimenti visibili
#if DEBUG
        private bool _pwdHiding;
#else
        private bool _pwdHiding = true;
#endif

        public void SetPasswordsHidden(bool val)
        {
            _pwdHiding = val;
        }
        public bool GetPasswordsHidden()
        {
            return _pwdHiding;
        }
        #endregion

        //@N|1.0.1|dt|restituisce la directory del log
        public string GetFolder()
        {
            return _logOutputPath;
        }

        private string _logOutputPath = "";
        private string _productName = "";
        public void SetLogFileParams(string productName, string path)
        {
            _productName = productName;
            _logOutputPath = path;
            BuildLogFile();
        }

        private string _fileName;

        public Logger()
        {
            Logger.AllLoggers.Add(this);
        }

        public Logger(string logOutputPath, string productName, int level)
            : this()
        {
            try
            {
                _logLevel = level;
                SetLevelEnabled();
                _logOutputPath = logOutputPath;
                _productName = productName;
                if (level == 0) return;
                BuildLogFile();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //@N|3.2.1|dt|added log capabilities
        [DefaultValue(false)]
        public bool AutoClassAndMethodNamePrefix
        {
            get;
            set;
        }

        ////@N|1.0.2|dt|gestione ID sessione di log
        //string _curSessionId = string.Empty;
        //public string CurSessionID {
        //    get { return _curSessionId; }
        //    set { _curSessionId = value; }
        //}

        private void BuildLogFile()
        {
            //@N|1.0.1|dt|creazione automatica dir log
            if (!Directory.Exists(_logOutputPath))
            {
                Directory.CreateDirectory(_logOutputPath);
            }
            _fileName = string.Format("{0}_{1}.log", new string[] { _productName, DateTime.Now.ToString("yyyyMMdd") });
            //@N|1.0.2|dt|storicizzazione log
            string logFileFullPath = Path.Combine(_logOutputPath, _fileName);
            if (File.Exists(logFileFullPath))
            {
                //@M|1.0.4|dt|variazione regola backup log
                //@B|1.0.3|dt|introdotta sovrascrittura file esistenti
                //File.Copy(logFileFullPath, logFileFullPath + "_" + (new Random().Next()).ToString(), true);
                string dir = Path.GetDirectoryName(logFileFullPath);
                string fName = Path.GetFileNameWithoutExtension(logFileFullPath);
                //@N|1.0.6|dt|gestione suffiso dei file storicizzati come da specifiche
                //@B|1.0.8|dt|correzione formato ora dei log storicizzati
                //string suffix = DateTime.Now.ToString("hh:mm:ss");
                string suffix = DateTime.Now.ToString("HH_mm_ss");
                //MessageBox.Show("file exists - suffix: " + suffix);
                //string newfName = fName + "_" + (new Random().Next()).ToString() + ".log";
                string newfName = fName + "_" + suffix + ".log";
                string newFullpath = Path.Combine(dir, newfName);
                //MessageBox.Show("file exists - new file name: " + newFullpath);
                File.Copy(logFileFullPath, newFullpath, true);
                File.Delete(logFileFullPath);
            }
            _logFile.CreateFile(logFileFullPath, true);

        }

        //private void WriteHeader() {
        //    //@N|1.0.2|dt|gestione ID sessione di log
        //    _curSessionId = string.Format("{0}-{1}", _productName, (new Random().Next()).ToString());
        //    _logFile.Append(string.Format("##### log session: {0} #####", _curSessionId));
        //}

        public void Log()
        {
            Log(string.Empty, null);
        }

        public void Log(string data)
        {
            Log(data, null);
        }

        public void Log(string data, object arg)
        {
            Log(data, new[] { arg });
        }

        public void Log(string data, object arg1, object arg2)
        {
            Log(data, new[] { arg1, arg2 });
        }

        //public void Log(string data, object[] args) {
        //    switch (_logLevel) {
        //        case 0:
        //            return;
        //        default:
        //            break;
        //    }
        //    if (!_headerWritten) {
        //        _headerWritten = true;
        //        WriteHeader();
        //    }
        //    string errorMessage;
        //    try {
        //        if (_logFile == null) {
        //            errorMessage = "unable to access the log file (check if InitLogFile has been called)";
        //            throw new BTException(errorMessage);
        //        }
        //        if (args != null) {
        //            data = string.Format(data, args);
        //        }
        //        string dateTime = "{" + DateTime.Now.ToString("HH:mm:ss") + "}";
        //        _logFile.Append(dateTime + " " + data);
        //    } catch (Exception ex) {
        //        errorMessage = "unable to write to log file";
        //        throw new BTException(errorMessage, ex);
        //    }
        //}

        public void Log(string data, object[] args)
        {
            if (_logSuspended) { return; }
            switch (_logLevel)
            {
                case 0:
                    return;
                default:
                    break;
            }
            try
            {
                if (_fileTransferLogMode)
                {
                    _logFile.Append("#");
                    return;
                }
                if (_logFile == null)
                {
                    return;
                }
                if (args != null)
                {
                    data = string.Format(data, args);
                }

                string prefix = string.Empty;
                if (AutoClassAndMethodNamePrefix)
                {
                    StackTrace trace = new StackTrace();
                    prefix = string.Format("[{0}][{1}] ",
                                           trace.GetFrame(2).GetMethod().DeclaringType.Name,
                                           trace.GetFrame(2).GetMethod().Name);
                    data = string.Format("{0}{1}", new[] { prefix, data });
                }

                //evento di logging - solleva l'evento riportando il contenuto del messaggio, non esattamente quanto viene tracciato nel file di log, che ha un suo formato specifico (mll, sll, ecc)
                if (OnLog != null)
                {
                    OnLog(_logLevel, data);
                }

                LineType lineType = LineType.SLL;

                if (data.Contains("\r"))
                {
                    lineType = LineType.MLL;
                    data = data.Replace("\r", "<CR>\r");
                }

                string dateTime = string.Format("{0}", DateTime.Now.ToString("HH:mm:ss"));
                string mllStr = "{" + string.Format("{0}", lineType) + "}";

                if (_pwdHiding)
                {
                    if (data.Contains("password") || data.Contains("pwd") || data.Contains("PASSWORD") || data.Contains("PWD"))
                    {
                        data = "*** log line containing one or more passwords ***";
                    }
                }

                string strData = string.Format("{0}\t{1}\t[{2}]\t{3}", new[] { mllStr, dateTime, _productName, data });
                _logFile.AppendLn(strData);
                if (lineType == LineType.MLL)
                {
                    _logFile.AppendLn(mllStr);
                }

            }
            catch (Exception)
            {
                return;
            }
        }

        //@N|3.1.22|dt|improved exception logging
        public void LogException(string sourceDescription, Exception ex)
        {
            Log("--- start Exception ---");
            Log("");
            Log("Source  = {0}", sourceDescription);
            Log("Message = {0}", ex.Message);
            Log("StackTrace = {0}", ex.StackTrace);
            Log("");
            Log("--- end Exception ---");

            if (ex.InnerException != null)
            {
                Log("--- start InnerException ---");
                string innerTxt = GetInnerException(ex);
                Log(innerTxt);
                Log("--- end InnerException ---");
            }
        }

        public static string GetInnerException(Exception ex)
        {
            if (ex.InnerException != null)
            {
                string currentInnerExc = ex.InnerException.Message + "\n" + ex.InnerException.StackTrace;
                string internalInnerExc = GetInnerException(ex.InnerException);
                return currentInnerExc + (internalInnerExc == string.Empty ? string.Empty : "\n>\n") + internalInnerExc;
            }
            return string.Empty;
        }

        private bool _logSuspended;
        public void SuspendLog()
        {
            _logSuspended = true;
        }
        public void ResumeLog()
        {
            _logSuspended = false;
        }

        #region file transfer log mode
        private bool _fileTransferLogMode;
        public void SetFileTransferLogMode()
        {
            _fileTransferLogMode = true;
        }
        public void ResetFileTransferLogMode()
        {
            try
            {
                _fileTransferLogMode = false;
                if (_logFile == null)
                {
                    return;
                }
                _logFile.AppendLn("");
            }
            catch (Exception)
            {
            }
        }
        #endregion


        //@M|1.0.9|dt|integrazione modifiche CS per TestDriveManager
        #region log level trace method
        public void Info(string str)
        {
            if (!IsInfoEnabled)
                return;
            Log("Info : " + str);
        }

        public void Debug(string str)
        {
            if (!IsDebugEnabled)
                return;
            Log("Debug: " + str);
        }

        public void Error(string str)
        {
            if (!IsErrorEnabled)
                return;
            Log("Error: " + str);
        }

        public void ErrorExc(string srcDesc, Exception ex)
        {
            if (!IsErrorEnabled)
                return;
            LogException(srcDesc, ex);
        }
        #endregion

    }
}
