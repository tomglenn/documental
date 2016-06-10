using System;
using Documental.Attributes;

namespace Documental.Samples.Console.Documents
{
    [Document(CollectionName = "people")]
    public class PersonDocument
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public PersonDocument()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
