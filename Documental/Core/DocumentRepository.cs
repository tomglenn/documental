using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Documental.Config;
using Documental.Extensions;
using Documental.Queries;
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

        public async Task<T> FindById<T>(string id) where T : Document
        {
            var documentUri = GetDocumentUri<T>(id);
            var response = await client.ReadDocumentAsync(documentUri);
            
            return response.Resource as T;
        }

        public T Query<T>(SingleDocumentQuery<T> query) where T : Document
        {
            var documentCollectionUri = GetDocumentCollectionUri<T>();
            var queryable = client.CreateDocumentQuery<T>(documentCollectionUri);

            return query.Execute(queryable);
        }

        public IEnumerable<T> Query<T>(MultipleDocumentQuery<T> query) where T : Document
        {
            var documentCollectionUri = GetDocumentCollectionUri<T>();
            var queryable = client.CreateDocumentQuery<T>(documentCollectionUri);

            return query.Execute(queryable);
        }

        public async Task Save<T>(T document) where T : Document
        {
            var documentAttribute = typeof(T).GetDocumentAttribute();
            if (documentAttribute == null)
            {
                throw new InvalidOperationException("The object you are trying to save must be decorated with the Document attribute");
            }

            var documentUri = GetDocumentUri<T>(document.Id);
            var documentCollectionUri = GetDocumentCollectionUri<T>();

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

        public async Task Delete<T>(T document) where T : Document
        {
            var documentUri = GetDocumentUri<T>(document.Id);
            await client.DeleteDocumentAsync(documentUri);
        }

        private Uri GetDocumentCollectionUri<T>() where T : Document
        {
            var documentAttribute = typeof(T).GetDocumentAttribute();
            if (documentAttribute == null)
            {
                throw new InvalidOperationException("The object you are trying to save must be decorated with the Document attribute");
            }
            
            return UriFactory.CreateDocumentCollectionUri(configuration.DatabaseName, documentAttribute.CollectionName);
        }

        private Uri GetDocumentUri<T>(string id)
        {
            var documentAttribute = typeof(T).GetDocumentAttribute();
            if (documentAttribute == null)
            {
                throw new InvalidOperationException("The object you are trying to save must be decorated with the Document attribute");
            }

            return UriFactory.CreateDocumentUri(configuration.DatabaseName, documentAttribute.CollectionName, id);
        }
    }
}
