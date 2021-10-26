using System.Runtime.Serialization;

namespace FiscalCodeValidator.Models
{
    [DataContract]
    public class Place
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "province")]
        public string Province { get; set; }
    }
}
