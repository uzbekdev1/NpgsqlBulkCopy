using System;
using System.Xml.Linq;

namespace Npgsql.BulkCopy.Extensions
{
    public static class XMLExtension
    {
        public static string GetElementValue(this XElement element, string key)
        {
            if (element == null)
                return String.Empty;

            var elem = element.Element(key);

            return elem == null ? String.Empty : elem.Value;
        }

    }
}
