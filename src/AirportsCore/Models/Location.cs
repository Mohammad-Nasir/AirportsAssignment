using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace AirportsCore.Models
{
    [DataContract]
    public class Location
    {
        [DataMember]
        public string iata { get; set; }
        [DataMember]
        public string lon { get; set; }
        [DataMember]
        public string iso { get; set; }
        [DataMember]
        public int status { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string continent { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string lat { get; set; }
        [DataMember]
        public string size { get; set; }
    }
}