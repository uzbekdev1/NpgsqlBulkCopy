using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Npgsql.BulkCopy.Helpers
{
    public static class XLinqHelper
    {
        public static XDocument GetConfigDocument(string path)
        {
            var content = File.ReadAllText(path, Encoding.UTF8);

            return XDocument.Parse(content, LoadOptions.None);
        }

    }
}
