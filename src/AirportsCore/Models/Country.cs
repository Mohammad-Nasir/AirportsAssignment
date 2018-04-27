using System.Runtime.Serialization;

namespace AirportsCore.Models
{
    [DataContract]
    public class Country
    {
        [DataMember]
        public string IsoCode { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Region { get; set; }
    }
}