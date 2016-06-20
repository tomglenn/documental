using System.Collections.Generic;
using System.Threading.Tasks;
using Documental.Queries;
using Microsoft.Azure.Documents;

namespace Documental.Core
{
    public interface IDocumentRepository
    {
        Task<T> FindById<T>(string id) where T : Document;
        T Query<T>(SingleDocumentQuery<T> query) where T : Document;
        IEnumerable<T> Query<T>(MultipleDocumentQuery<T> query) where T : Document;
        Task Save<T>(T document) where T : Document;
        Task Delete<T>(T document) where T : Document;
    }
}
