using System.Threading.Tasks;
using Documental.Config;
using Microsoft.Azure.Documents.Client;

namespace Documental.Contributors
{
    public abstract class DocumentDbCreationStrategyContributor : IDocumentDbCreationStrategyContributor
    {
        public virtual Task OnPreCreate(DocumentClient client, IDocumentDbConfiguration configuration)
        {
            return Task.FromResult(default(object));
        }

        public virtual Task Contribute(DocumentClient client, IDocumentDbConfiguration configuration)
        {
            return Task.FromResult(default(object));
        }

        public virtual Task OnPostCreate(DocumentClient client, IDocumentDbConfiguration configuration)
        {
            return Task.FromResult(default(object));
        }
    }
}
