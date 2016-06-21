using System;
using Microsoft.Azure.Documents;

namespace Documental.Core
{
    public abstract class AbstractDocument : Document
    {
        protected AbstractDocument()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
