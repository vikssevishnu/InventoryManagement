using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace InventoryManagement.Utilities
{
    public class Logger
    {
        private static readonly Logger instance = new Logger();
        private static object obj = new object();
        private string filePath = string.Empty;

        private Logger()
        {            
        }
        public static Logger Instance()
        {
            return instance;
        }

        public void LogMessage(string logMessage)
        {            
            try
            {
                lock (obj)
                {
                    if (string.IsNullOrEmpty(filePath))
                        filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["LogFileName"]);

                    using (StreamWriter w = File.AppendText(filePath))
                    {
                        Log(logMessage, w);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while logging :  " + ex.Message);
            }
        }

        private void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("{0} {1}", DateTime.Now.ToString(),
                    DateTime.Now.ToString());
                txtWriter.WriteLine("  :{0}", logMessage);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while logging :  " + ex.Message);
            }
        }
    }
}