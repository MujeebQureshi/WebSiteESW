using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteBackEnd.ResumeParse
{
    public class Resume
    {
        [JsonProperty(PropertyName = "first-name")]
        public string firstname {get; set;}
        [JsonProperty(PropertyName = "last-name")]
        public string lastname { get; set; } 
        public string gender { get; set; }
        [JsonProperty(PropertyName = "email-address")]
        public string emailaddress {get; set;}
        [JsonProperty(PropertyName = "phone-numbers")]
        public string phonenumbers {get; set;}
        public string languages {get; set;}
        [JsonProperty(PropertyName = "summary-description")]
        public string summarydescription {get; set;}
        public string skills {get; set;}
        public string location {get; set;}
        public List<positions> positions { get; set;}
        public string projects {get; set;}
        [JsonProperty(PropertyName = "social-profiles")]
        public string socialprofiles {get; set;}
        public List<educations> educations { get; set; }
        public string courses {get; set;}
        public string awards {get; set;}
    }
    
    public class positions
    {
        public string title { get; set; }
        [JsonProperty(PropertyName = "start-date")]
        public string startdate {get; set;}
        [JsonProperty(PropertyName = "end-date")]
        public string enddate {get; set;}
        public string company { get; set; }
        public string summary { get; set; }
    }

    public class educations
    {
        public string school { get; set; }
        public string course { get; set; }
        [JsonProperty(PropertyName = "start-date")]
        public string startdate {get; set;}
        [JsonProperty(PropertyName = "end-date")]
        public string enddate {get; set;}
    }
}
