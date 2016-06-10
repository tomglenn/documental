using Documental.Contributors;
using Documental.Core;
using Documental.Samples.Console.Documents;

namespace Documental.Samples.Console
{
    public class SampleDocumentDbCreationStrategy : DocumentDbCreationStrategy
    {
        public SampleDocumentDbCreationStrategy()
        {
            AddContributor(new DeleteDatabaseIfExistsContributor());
            AddContributor(new FromAttributesMappingContributor<PersonDocument>());
        }
    }
}
