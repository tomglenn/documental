using System.Threading.Tasks;
using Documental.Core.Config;
using Microsoft.Azure.Documents.Client;

namespace Documental.Contributors
{
    public interface IDocumentDbCreationStrategyContributor
    {
        Task OnPreCreate(DocumentClient client, IConfiguration configuration);
        Task Contribute(DocumentClient client, IConfiguration configuration);
        Task OnPostCreate(DocumentClient client, IConfiguration configuration);
    }
}
