using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Documents;

namespace Documental.Queries
{
    public abstract class MultipleDocumentQuery<T> where T : Document
    {
        protected abstract IEnumerable<T> ExecuteQuery(IOrderedQueryable<T> query);

        public IEnumerable<T> Execute(IOrderedQueryable<T> query)
        {
            return ExecuteQuery(query).AsEnumerable();
        }
    }

    public abstract class MultipleDocumentQuery<T, TReturn> where T : Document
    {
        protected abstract IEnumerable<TReturn> ExecuteQuery(IOrderedQueryable<T> query);

        public IEnumerable<TReturn> Execute(IOrderedQueryable<T> query)
        {
            return ExecuteQuery(query).AsEnumerable();
        }
    }
}
