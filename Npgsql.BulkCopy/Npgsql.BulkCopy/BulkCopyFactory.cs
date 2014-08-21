using System;
using System.IO;
using Npgsql.BulkCopy.Enums;
using Npgsql.BulkCopy.Extensions;
using Npgsql.BulkCopy.Helpers;

namespace Npgsql.BulkCopy
{
    public static class BulkCopyFactory
    {
        private static BulkCopyModel _model;

        private static void PreInit()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BulkCopySettings.CONFIG_FILE);
            var doc = XLinqHelper.GetConfigDocument(path);
            var root = doc.Element("Repository");


            _model = new BulkCopyModel
            {
                MajorVersion = root.GetElementValue("MajorVersion").AsEnum<PgVersions>() 
            };

            var connection = root.Element("Connection");

            _model.Connection = new ConnectionSettings
            {
                Host = connection.GetElementValue("Host"),
                Database = connection.GetElementValue("Database"),
                Port = connection.GetElementValue("Port").AsInt(),
                UserName = connection.GetElementValue("UserName"),
                Password = connection.GetElementValue("Password")
            };

        }

        static BulkCopyFactory()
        {
            PreInit();
        }


        public static BulkCopyModel GetModel()
        {
            return _model;
        }

    }
}
