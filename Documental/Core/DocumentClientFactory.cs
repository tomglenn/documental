using System;
using Documental.Config;
using Microsoft.Azure.Documents.Client;

namespace Documental.Core
{
    internal static class DocumentClientFactory
    {
        public static DocumentClient Create(IDocumentDbConfiguration configuration)
        {
            return new DocumentClient(new Uri(configuration.EndpointUri), configuration.Key);
        }
    }
}
