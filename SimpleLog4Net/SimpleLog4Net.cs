using System;
using System.IO;
using System.Reflection;

namespace SimpleLog4Net
{
    public class SimpleLogger
    {
        #region static

        private static Assembly LOG4NET;
        static SimpleLogger()
        {
            LOG4NET = null;

            // Get the stream of the resource-file 
            Stream confStream = GetResourceFileByPath("SimpleLog4Net.Resources.log4net.config");
            Stream dllStream = GetResourceFileByPath("SimpleLog4Net.Resources.log4net.dll");

            // Initialize Log4Net
            InitLog4Net(dllStream, confStream);

            // Release Stream
            confStream.Close();
            dllStream.Close();
        }

        /// <summary>
        /// According to the path, return the stream of the resource-file
        /// </summary>
        /// <param name="resourcePath">the path of the resource-file</param>
        /// <returns>the stream of the resource-file</returns>
        private static Stream GetResourceFileByPath(string resourcePath)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
        }

        /// <summary>
        /// Initialize log4net
        /// </summary>
        /// <param name="dllStream"></param>
        /// <param name="confStream"></param>
        private static void InitLog4Net(Stream dllStream, Stream confStream)
        {
            byte[] dllByte = new byte[dllStream.Length];
            dllStream.Read(dllByte, 0, dllByte.Length);
            LOG4NET = Assembly.Load(dllByte);

            // XmlConfigurator.Configure(p_confStream)
            Type xmlConfiguratorClass = LOG4NET.GetType("log4net.Config.XmlConfigurator");
            MethodInfo configureMethod = xmlConfiguratorClass.GetMethod("Configure", new Type[] { typeof(System.IO.Stream) });
            configureMethod.Invoke(null, new object[] { confStream });
        }

        #endregion

        private object logger;

        private MethodInfo DebugMethod;
        private MethodInfo DebugMethod_WithException;

        private MethodInfo InfoMethod;
        private MethodInfo InfoMethod_WithException;

        private MethodInfo WarnMethod;
        private MethodInfo WarnMethod_WithException;

        private MethodInfo ErrorMethod;
        private MethodInfo ErrorMethod_WithException;

        private MethodInfo FatalMethod;
        private MethodInfo FatalMethod_WithException;

        public SimpleLogger(Type p_loggerType)
        {
            Initialization();

            Type ILog = ReflectILog();
            InitLogger(p_loggerType);
            InitDebug(ILog);
            InitInfo(ILog);
            InitWarn(ILog);
            InitError(ILog);
            InitFatal(ILog);
        }

        public SimpleLogger(string loggerName)
        {
            Initialization();

            Type ILog = ReflectILog();
            InitLogger(loggerName);
            InitDebug(ILog);
            InitInfo(ILog);
            InitWarn(ILog);
            InitError(ILog);
            InitFatal(ILog);
        }

        #region InitFunction

        private void Initialization()
        {
            logger = null;

            DebugMethod = null;
            DebugMethod_WithException = null;

            InfoMethod = null;
            InfoMethod_WithException = null;

            WarnMethod = null;
            WarnMethod_WithException = null;

            ErrorMethod = null;
            ErrorMethod_WithException = null;

            FatalMethod = null;
            FatalMethod_WithException = null;
        }

        private void InitLogger(Type loggerType)
        {
            Type LogManager = LOG4NET.GetType("log4net.LogManager");
            MethodInfo GetLoggerMethod = LogManager.GetMethod("GetLogger", new Type[] { typeof(System.Type) });
            logger = GetLoggerMethod.Invoke(null, new object[] { loggerType });
        }

        private void InitLogger(string loggerName)
        {
            Type LogManager = LOG4NET.GetType("log4net.LogManager");
            MethodInfo GetLoggerMethod = LogManager.GetMethod("GetLogger", new Type[] { typeof(System.String) });
            logger = GetLoggerMethod.Invoke(null, new object[] { loggerName });
        }

        private void InitDebug(Type ILog)
        {
            DebugMethod = ILog.GetMethod("Debug", new Type[] { typeof(System.String) });
            DebugMethod_WithException = ILog.GetMethod("Debug", new Type[] { typeof(System.String), typeof(System.Exception) });
        }

        private void InitInfo(Type ILog)
        {
            InfoMethod = ILog.GetMethod("Info", new Type[] { typeof(System.String) });
            InfoMethod_WithException = ILog.GetMethod("Info", new Type[] { typeof(System.String), typeof(System.Exception) });
        }

        private void InitWarn(Type ILog)
        {
            WarnMethod = ILog.GetMethod("Warn", new Type[] { typeof(System.String) });
            WarnMethod_WithException = ILog.GetMethod("Warn", new Type[] { typeof(System.String), typeof(System.Exception) });
        }

        private void InitError(Type ILog)
        {
            ErrorMethod = ILog.GetMethod("Error", new Type[] { typeof(System.String) });
            ErrorMethod_WithException = ILog.GetMethod("Error", new Type[] { typeof(System.String), typeof(System.Exception) });
        }

        private void InitFatal(Type ILog)
        {
            FatalMethod = ILog.GetMethod("Fatal", new Type[] { typeof(System.String) });
            FatalMethod_WithException = ILog.GetMethod("Fatal", new Type[] { typeof(System.String), typeof(System.Exception) });
        }

        #endregion

        public void Debug(string message)
        {
            DebugMethod.Invoke(logger, new object[] { message });
        }

        public void Debug(string message, Exception exception)
        {
            DebugMethod_WithException.Invoke(logger, new object[] { message, exception });
        }

        public void Info(string message)
        {
            InfoMethod.Invoke(logger, new object[] { message });
        }

        public void Info(string message, Exception exception)
        {
            InfoMethod_WithException.Invoke(logger, new object[] { message, exception });
        }

        public void Warn(string message)
        {
            WarnMethod.Invoke(logger, new object[] { message });
        }

        public void Warn(string message, Exception exception)
        {
            WarnMethod_WithException.Invoke(logger, new object[] { message, exception });
        }

        public void Error(string message)
        {
            ErrorMethod.Invoke(logger, new object[] { message });
        }

        public void Error(string message, Exception exception)
        {
            ErrorMethod_WithException.Invoke(logger, new object[] { message, exception });
        }

        public void Fatal(string message)
        {
            FatalMethod.Invoke(logger, new object[] { message });
        }

        public void Fatal(string message, Exception exception)
        {
            FatalMethod_WithException.Invoke(logger, new object[] { message, exception });
        }

        /// <summary>
        ///  Reflect the Interface whose name is "ILog"
        /// </summary>
        /// <returns></returns>
        private Type ReflectILog()
        {
            Type ILog = null;

            foreach (Type item in LOG4NET.GetTypes())
            {
                if (item.Name == "ILog")
                {
                    ILog = item;
                    break;
                }
            }

            return ILog;
        }

    }
}
