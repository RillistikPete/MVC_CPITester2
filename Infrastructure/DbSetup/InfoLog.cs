using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCTesterCPI2.Infrastructure.DbSetup
{
    public class InfoLog
    {
        [Key]
        [JsonProperty("LogId")]
        public int LogId { get; set; }
        public string ProjectName { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime DateAdded { get; set; }
    }
}