using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Documents;

namespace Documental.Queries
{
    public class MultipleDocumentQuery<T> where T : Document
    {
        protected virtual IEnumerable<T> ExecuteQuery(IOrderedQueryable<T> query)
        {
            return query;
        }

        public IEnumerable<T> Execute(IOrderedQueryable<T> query)
        {
            return ExecuteQuery(query);
        }
    }
}
