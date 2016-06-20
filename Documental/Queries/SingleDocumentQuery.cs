using System.Linq;
using Microsoft.Azure.Documents;

namespace Documental.Queries
{
    public class SingleDocumentQuery<T> where T : Document
    {
        protected virtual T ExecuteQuery(IOrderedQueryable<T> query)
        {
            return query.FirstOrDefault();
        }

        public T Execute(IOrderedQueryable<T> query)
        {
            return ExecuteQuery(query);
        }
    }
}
