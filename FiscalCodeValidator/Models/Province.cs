using System.Runtime.Serialization;

namespace FiscalCodeValidator.Models
{
    [DataContract]
    public class Province
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
