using System;
using System.Net;
using System.Threading.Tasks;
using Documental.Config;
using Documental.Extensions;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Documental.Core
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IDocumentDbConfiguration configuration;
        private readonly DocumentClient client;

        public DocumentRepository(IDocumentDbConfiguration configuration)
        {
            this.configuration = configuration;
            client = DocumentClientFactory.Create(configuration);
        }

        public async Task Save<T>(T document) where T : Document
        {
            var documentAttribute = typeof(T).GetDocumentAttribute();
            if (documentAttribute == null)
            {
                throw new InvalidOperationException("The object you are trying to save must be decorated with the Document attribute");
            }

            var documentUri = UriFactory.CreateDocumentUri(configuration.DatabaseName, documentAttribute.CollectionName, document.Id);
            var documentCollectionUri = UriFactory.CreateDocumentCollectionUri(configuration.DatabaseName, documentAttribute.CollectionName);

            try
            {
                var response = await client.ReadDocumentAsync(documentUri);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await client.ReplaceDocumentAsync(documentUri, document);
                }
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentAsync(documentCollectionUri, document);
                }
            }
        }
    }
}
