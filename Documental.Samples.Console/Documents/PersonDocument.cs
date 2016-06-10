using Documental.Attributes;
using Documental.Core;

namespace Documental.Samples.Console.Documents
{
    [DocumentType(CollectionName = "people")]
    public class PersonDocument : Document
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
