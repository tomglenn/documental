using System.Linq;
using Microsoft.Azure.Documents;

namespace Documental.Queries
{
    public abstract class SingleDocumentQuery<T> where T : Document
    {
        protected abstract T ExecuteQuery(IOrderedQueryable<T> query);

        public T Execute(IOrderedQueryable<T> query)
        {
            return ExecuteQuery(query);
        }
    }
}
