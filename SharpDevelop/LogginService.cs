using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core.Services;

namespace IdeBridge
{
    class LogginService : ILoggingService
    {
        #region ILoggingService Members

        public void Debug(object message)
        {
            Logger.Debug(message.ToString());
        }

        public void DebugFormatted(string format, params object[] args)
        {
            Logger.Debug(string.Format(format, args));
        }

        public void Error(object message, Exception exception)
        {
            Error(message + ":");
            Logger.Exception(exception);
        }

        public void Error(object message)
        {
            Logger.Error(message.ToString());
        }

        public void ErrorFormatted(string format, params object[] args)
        {
            Logger.Error(string.Format(format, args));
        }

        public void Fatal(object message, Exception exception)
        {
            Error(message + ":");
            Logger.Exception(exception);
        }

        public void Fatal(object message)
        {
            Error(message);
        }

        public void FatalFormatted(string format, params object[] args)
        {
            Logger.Error(string.Format(format, args));
        }

        public void Info(object message)
        {
            Logger.Info(message.ToString());
        }

        public void InfoFormatted(string format, params object[] args)
        {
            Logger.Info(string.Format(format, args));
        }

        public bool IsDebugEnabled
        {
            get { return true; }
        }

        public bool IsErrorEnabled
        {
            get { return true; }
        }

        public bool IsFatalEnabled
        {
            get { return true; }
        }

        public bool IsInfoEnabled
        {
            get { return true; }
        }

        public bool IsWarnEnabled
        {
            get { return true; }
        }

        public void Warn(object message, Exception exception)
        {
            Warn(message + ":");
            Logger.Exception(exception);
        }

        public void Warn(object message)
        {
            Logger.Error(message.ToString());
        }

        public void WarnFormatted(string format, params object[] args)
        {
            Logger.Error(string.Format(format, args));
        }

        #endregion
    }
}
