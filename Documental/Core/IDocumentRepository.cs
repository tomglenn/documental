using System.Threading.Tasks;

namespace Documental.Core
{
    public interface IDocumentRepository
    {
        Task Save<T>(T document) where T : Document;
    }
}
