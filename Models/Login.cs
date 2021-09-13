using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTesterCPI2.Models
{
    public partial class Login
    {
        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("Token")]
        public string Token { get; set; }

        [JsonProperty("PasswordCreatedDateTime")]
        public string PasswordCreatedDateTime { get; set; }

        [JsonProperty("UserID")]
        public long UserId { get; set; }
    }
}