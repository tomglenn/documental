using System.Threading.Tasks;
using Documental.Config;
using Microsoft.Azure.Documents.Client;

namespace Documental.Contributors
{
    public interface IDocumentDbCreationStrategyContributor
    {
        Task OnPreCreate(DocumentClient client, IDocumentDbConfiguration configuration);
        Task Contribute(DocumentClient client, IDocumentDbConfiguration configuration);
        Task OnPostCreate(DocumentClient client, IDocumentDbConfiguration configuration);
    }
}
