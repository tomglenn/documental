using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Documental.Config;
using Documental.Contributors;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Documental.Core
{
    public abstract class DocumentDbCreationStrategy : IDocumentDbCreationStrategy
    {
        private readonly IList<IDocumentDbCreationStrategyContributor> contributors = new List<IDocumentDbCreationStrategyContributor>();
          
        public async Task Create(IDocumentDbConfiguration configuration)
        {
            var client = DocumentClientFactory.Create(configuration);

            await OnPreCreate(client, configuration);
            await CreateDatabase(client, configuration);
            await Contribute(client, configuration);
            await OnPostCreate(client, configuration);
        }

        protected void AddContributor(IDocumentDbCreationStrategyContributor contributor)
        {
            contributors.Add(contributor);
        }

        private async Task OnPreCreate(DocumentClient client, IDocumentDbConfiguration configuration)
        {
            foreach (var contributor in contributors)
            {
                await contributor.OnPreCreate(client, configuration);
            }
        }

        private async Task CreateDatabase(DocumentClient client, IDocumentDbConfiguration configuration)
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(configuration.DatabaseName));
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = configuration.DatabaseName });
                }
            }
        }

        private async Task Contribute(DocumentClient client, IDocumentDbConfiguration configuration)
        {
            foreach (var contributor in contributors)
            {
                await contributor.Contribute(client, configuration);
            }
        }

        private async Task OnPostCreate(DocumentClient client, IDocumentDbConfiguration configuration)
        {
            foreach (var contributor in contributors)
            {
                await contributor.OnPostCreate(client, configuration);
            }
        }
    }
}
