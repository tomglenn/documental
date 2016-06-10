using System.Threading.Tasks;
using Documental.Config;

namespace Documental.Core
{
    public interface IDocumentDbCreationStrategy
    {
        Task Create(IDocumentDbConfiguration configuration);
    }
}
