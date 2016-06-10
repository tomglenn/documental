using System;
using Newtonsoft.Json;

namespace Documental.Core
{
    public abstract class Document
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        protected Document()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
