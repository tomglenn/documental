using Documental.Attributes;
using Documental.Core;
using Newtonsoft.Json;

namespace Documental.Samples.Console.Documents
{
    [DocumentType("people")]
    public class PersonDocument : AbstractDocument
    {
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }
    }
}
