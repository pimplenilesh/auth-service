using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Models
{
    public class LoginDTO
    {
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
