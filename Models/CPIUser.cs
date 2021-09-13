using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTesterCPI2.Models
{
    public class CPIUser
    {
        public int ID { get; set; }

        [JsonProperty("Username")]
        public string CPIUsername { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LastEditBy { get; set; }
        public DateTime LastEditDate { get; set; }
        public DateTime CreatedDate { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
        public int ProgramID { get; set; }
    }
}