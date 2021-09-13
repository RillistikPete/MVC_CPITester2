using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MVCTesterCPI2.Infrastructure.DbSetup
{
    public static class LoggerLQ
    {
        private static readonly object lockObject = new object();
        public static void LogQueue(string message)
        {
            lock (lockObject)
            {
                var optionsBuilder = new DbContextOptionsBuilder<LogContext>();
                optionsBuilder.UseSqlServer(ConfigurationManager.AppSettings["localExpressDb"]);
                using (var db = new LogContext(optionsBuilder.Options))
                {
                    DateTime cstTime = DateTime.Now;
                    InfoLog il = new InfoLog
                    {
                        ProjectName = "CPI",
                        Message = message,
                        DateAdded = cstTime
                    };
                    db.InfoLog.Add(il);
                    db.SaveChanges();
                }
            }
        }
    }
}