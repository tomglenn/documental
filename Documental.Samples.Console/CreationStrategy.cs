using Documental.Contributors;
using Documental.Core;
using Documental.Samples.Console.Documents;

namespace Documental.Samples.Console
{
    public class CreationStrategy : DocumentDbCreationStrategy
    {
        public CreationStrategy()
        {
            AddContributor(new DeleteDatabaseIfExistsContributor());
            AddContributor(new FromAttributesMappingContributor<PersonDocument>());
        }
    }
}
