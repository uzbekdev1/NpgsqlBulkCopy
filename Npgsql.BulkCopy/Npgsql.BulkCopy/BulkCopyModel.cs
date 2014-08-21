using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql.BulkCopy.Enums;

namespace Npgsql.BulkCopy
{

    public struct ConnectionSettings
    {

        public string Host { get; set; }

        public int Port { get; set; }

        public string Database { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

    }

    public struct BulkCopyModel
    {
        public PgVersions MajorVersion { get; set; }

        public ConnectionSettings Connection { get; set; }

    }
}
