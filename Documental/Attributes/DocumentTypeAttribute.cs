using System;

namespace Documental.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DocumentTypeAttribute : Attribute
    {
        public string CollectionName { get; set; }
    }
}
