using System;
using Documental.Attributes;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace Documental.Samples.Console.Documents
{
    [DocumentType(CollectionName = "people")]
    public class PersonDocument : Document
    {
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        public PersonDocument()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
