using System;
using Documental.Attributes;

namespace Documental.Extensions
{
    internal static class TypeExtensions
    {
        public static DocumentTypeAttribute GetDocumentAttribute(this Type type)
        {
            return (DocumentTypeAttribute) Attribute.GetCustomAttribute(type, typeof(DocumentTypeAttribute));
        }
    }
}
