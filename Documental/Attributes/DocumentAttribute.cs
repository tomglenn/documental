using System;

namespace Documental.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DocumentAttribute : Attribute
    {
        public string CollectionName { get; set; }
    }
}
