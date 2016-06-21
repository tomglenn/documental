using System.Net;
using System.Threading.Tasks;
using Documental.Config;
using Documental.Extensions;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Documental.Contributors
{
    public sealed class FromAttributesMappingContributor<TDocument> : DocumentDbCreationStrategyContributor where TDocument : Document 
    {
        public override async Task Contribute(DocumentClient client, IDocumentDbConfiguration configuration)
        {
            var attribute = typeof (TDocument).GetDocumentTypeAttribute();
            if (attribute == null)
            {
                return;
            }

            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(configuration.DatabaseName, attribute.CollectionName));
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode != HttpStatusCode.NotFound)
                {
                    throw;
                }
                
                var collectionInfo = new DocumentCollection
                {
                    Id = attribute.CollectionName,
                    IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) { Precision = -1 })
                };

                await client.CreateDocumentCollectionAsync(UriFactory.CreateDatabaseUri(configuration.DatabaseName), collectionInfo);
            }
        }
    }
}
