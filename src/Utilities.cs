using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;

namespace OpenARIANA.Utilities
{

    [Serializable]
    public class OArianaException : Exception
    {
        public int ErrorCode { get; private set; } // An example additional property

        public OArianaException() : base() { }

        public OArianaException(string message) : base(message) { }

        public OArianaException(string message, Exception innerException) : base(message, innerException) { }

        public OArianaException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        // This constructor is needed for deserialization
        protected OArianaException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
            ErrorCode = info.GetInt32("ErrorCode");
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ErrorCode", ErrorCode);
        }
    }

    public static class Logger
    {
        private static string logFilePath;

        public static string LogFilePath
        {
            get { return logFilePath; }
        }

        // Implement the Logger asynchronously to ensure that logging operations do not impede the main computational processes.
        private static ConcurrentQueue<string> logQueue = new ConcurrentQueue<string>();
        private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private static Task logTask;

        public enum VerbosityLevel
        {
            ErrorsOnly = 0,
            ErrorsAndWarnings = 1,
            All = 2
        }
        public static VerbosityLevel Verbosity { get; set; }

        static Logger()
        {
            int verbosityLevel = Properties.Settings.Default.VerbosityLevel;

            if (Enum.IsDefined(typeof(VerbosityLevel), verbosityLevel))
            {
                Verbosity = (VerbosityLevel)verbosityLevel;
            }
            else
            {
                Verbosity = VerbosityLevel.ErrorsAndWarnings;
            }

            FileSystemManager fileSystemManager = new FileSystemManager();
            logFilePath = fileSystemManager.GetAbsoluteFilePath("log.log", "OpenARIANA\\Logs");

            File.WriteAllText(logFilePath, string.Empty);

            // Start the logging task
            logTask = Task.Run(() => ProcessLogQueue(), cancellationTokenSource.Token);
        }

        public static void UpdateLoggerSettings()
        {
            // Update Verbosity Level
            int verbosityLevel = Properties.Settings.Default.VerbosityLevel;
            if (Enum.IsDefined(typeof(VerbosityLevel), verbosityLevel))
            {
                Verbosity = (VerbosityLevel)verbosityLevel;
                // Restart logging task
                RestartLoggingTask();
                LogSystem($"Logger verbosity level changed to '{Verbosity.ToString()}'.");
            }
            else
            {
                Verbosity = VerbosityLevel.ErrorsAndWarnings;
                LogSystem($"Verbosity level {verbosityLevel} invalid. Set to default 'ErrorsAndWarning'.");
                throw new ArgumentException($"Verbosity level {verbosityLevel} invalid. Set to default 'ErrorsAndWarning'.");

            }
        }

        private static void RestartLoggingTask()
        {
            // Cancel the existing task
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();

                // It's a good practice to wait for the task to complete
                try
                {
                    logTask?.Wait();
                }
                catch (AggregateException) { }
                finally
                {
                    cancellationTokenSource.Dispose();
                }
            }

            // Clear the log queue if required
            // logQueue = new ConcurrentQueue<string>();

            // Create a new CancellationTokenSource
            cancellationTokenSource = new CancellationTokenSource();

            // Start a new logging task
            logTask = Task.Run(() => ProcessLogQueue(), cancellationTokenSource.Token);
        }
        private static void ProcessLogQueue()
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                if (logQueue.TryDequeue(out string logEntry))
                {
                    File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
                }
                else
                {
                    // Sleep for a short time if the queue is empty
                    Task.Delay(500, cancellationTokenSource.Token).Wait();
                }
            }
        }

        public static void StopLogging()
        {
            cancellationTokenSource.Cancel();
            logTask.Wait(); // Optionally wait for the logging task to finish
        }

        private static void EnqueueLog(string message)
        {
            string timestamp = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
            string logEntry = $"{timestamp}: {message}";
            logQueue.Enqueue(logEntry);
        }

        private static string GetCallingMethodName()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(2); // '2' is usually the number to get the calling method

            return sf.GetMethod().Name;
        }

        public static void LogInfo(string message)
        {
            if (Verbosity >= VerbosityLevel.All)
            {
                string callingMethod = GetCallingMethodName();
                EnqueueLog($"INFO - {callingMethod}: {message}");
            }
        }

        public static void LogWarning(string message)
        {
            if (Verbosity >= VerbosityLevel.ErrorsAndWarnings)
            {
                string callingMethod = GetCallingMethodName();
                EnqueueLog($"WARNING - {callingMethod}: {message}");
            }
        }

        public static void LogError(string message)
        {
            if (Verbosity >= VerbosityLevel.ErrorsOnly)
            {
                string callingMethod = GetCallingMethodName();
                EnqueueLog($"ERROR - {callingMethod}:{message}");
            }
        }

        public static void LogSystem(string message)
        {
            string callingMethod = GetCallingMethodName();
            EnqueueLog($"SYSTEM - {callingMethod}: {message}");
        }

    }

    public class FileSystemManager
    {
        private string baseDirectory;

        public FileSystemManager(string baseDir = null)
        {
            string pathToCheck = baseDir ?? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            CheckPermissions(pathToCheck);
            baseDirectory = pathToCheck;
        }

        public void SetBaseDirectory(string baseDir)
        {
            CheckPermissions(baseDir);
            baseDirectory = baseDir;

        }

        private bool CheckPermissions(string path = null)
        {
            string pathToCheck = path ?? baseDirectory;
            try
            {
                // Get the directory's access control
                DirectorySecurity directorySecurity = Directory.GetAccessControl(pathToCheck);
                return true; // If no exception is thrown, it means permissions are granted
            }
            catch (UnauthorizedAccessException)
            {
                Logger.LogError($"Not authorized to access {pathToCheck}.");
                throw new OArianaException($"Not authorized to access {pathToCheck}", errorCode: 1001);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Unexpected error while checking permissions: {ex.ToString()}");
                throw new OArianaException($"Unexpected error while checking permissions: {ex.ToString()}", errorCode: 1002);
            }
        }

        private void EnsureDirectoryExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                Logger.LogSystem($"Directory '{directoryPath}' created.");
            }
        }

        public string GetAbsoluteFilePath(string fileName, string subDirectory = null)
        {

            string directory = Path.Combine(baseDirectory, subDirectory ?? string.Empty);
            EnsureDirectoryExists(directory);

            string filePath = Path.Combine(directory, fileName);
            if (!File.Exists(filePath))
            {
                using (var fileStream = File.Create(filePath))
                {

                }

                Logger.LogSystem($"{fileName} created at {filePath}.");
            }

            return filePath;
        }

        public void MoveFile(string sourcePath, string targetPath) { }

        public void DeleteFile(string fileName) { }



    }
}