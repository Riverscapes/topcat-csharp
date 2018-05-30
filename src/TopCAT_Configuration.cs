using System;
using System.IO;
using System.Reflection;

namespace tat.classes.TopCAT
{
    public static partial class TopCAT_Configuration

    {
        private static volatile bool _configuredTopCAT = false;
        private static string _sTopCATPath;

        /// <summary>
        /// Function to determine which platform we're on
        /// </summary>
        private static string GetPlatform()
        {
            return IntPtr.Size == 4 ? "x86" : "x64";
        }

        private static string GetExecutableName()
        {
            return String.Format("TopCAT{0}.exe",
                                  GetPlatform());
        }

        /// <summary>
        /// Construction of TopCAT path
        /// </summary>
        static TopCAT_Configuration()
        {
            var executingAssemblyFile = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath;
            var executingDirectory = Path.GetDirectoryName(executingAssemblyFile);

            if (string.IsNullOrEmpty(executingDirectory))
                throw new InvalidOperationException("cannot get executing directory");

            string sTopCATPath = Path.Combine(executingDirectory, "TopCAT");
            _sTopCATPath = Path.Combine(sTopCATPath, 
                                        GetPlatform(),
                                        GetExecutableName());
        }

        /// <summary>
        /// Method to ensure the static constructor is being called.
        /// </summary>
        /// <remarks>Be sure to call this function before using Gdal/Ogr/Osr</remarks>
        public static void ConfigureTopCAT()
        {
            if (_configuredTopCAT) return;

            // test configuration
            _configuredTopCAT = true;
            PrintToPCATConfig();
        }

        public static string GetTopCATPath()
        {
            return _sTopCATPath;
        }

        public static void PrintToPCATConfig()
        {
#if DEBUG
            Console.WriteLine(string.Format("TOPCAT EXECUTING PATH: {0}", _sTopCATPath));
#endif
        }
    }
}
