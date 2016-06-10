using System;
using Documental.Core.Config;
using Microsoft.Azure.Documents.Client;

namespace Documental.Core
{
    internal static class DocumentClientFactory
    {
        public static DocumentClient Create(IConfiguration configuration)
        {
            return new DocumentClient(new Uri(configuration.EndpointUri), configuration.Key);
        }
    }
}
