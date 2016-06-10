using System;
using System.Net;
using System.Threading.Tasks;
using Documental.Attributes;
using Documental.Core.Config;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Documental.Contributors
{
    public sealed class FromAttributesMappingContributor<TDocument> : DocumentDbCreationStrategyContributor where TDocument : class 
    {
        public override async Task Contribute(DocumentClient client, IConfiguration configuration)
        {
            var attribute = (DocumentAttribute)Attribute.GetCustomAttribute(typeof(TDocument), typeof(DocumentAttribute));
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
