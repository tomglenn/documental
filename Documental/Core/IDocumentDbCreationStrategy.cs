using System.Threading.Tasks;
using Documental.Core.Config;

namespace Documental.Core
{
    public interface IDocumentDbCreationStrategy
    {
        Task Create(IConfiguration configuration);
    }
}
