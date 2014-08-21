using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql.BulkCopy.Enums;
using Npgsql.BulkCopy.Extensions;
using Npgsql.BulkCopy.Native;
using Npgsql.BulkCopy.Native.Versions;

namespace Npgsql.BulkCopy.Demo
{
    class Program
    {

        static unsafe void Main(string[] args)
        {
            var model = BulkCopyFactory.GetModel();
            var columnData = String.Join(",", new[]
                    {
                        "EmployeeID",
                        "LastName",
                        "FirstName",
                        "Title",
                        "BirthDate",
                        "Address"
                    }.Select(s => String.Format("\"{0}\"", s)));
            var tableName = "\"public\".\"employees\"";
            var batchSize = 10000;
            var recordSize = 10000000;

            PQNativeApi.LoadDLLDirectory(model.MajorVersion);

            switch (model.MajorVersion)
            {
                case PgVersions.PG8x:
                    {
                        PQ8xNativeApi.openLocaleConn(model.Connection.Database.AsPointer(), model.Connection.UserName.AsPointer(), model.Connection.Password.AsPointer());
                        PQ8xNativeApi.setColumns(columnData.AsPointer());
                        PQ8xNativeApi.setTableName(tableName.AsPointer());
                        PQ8xNativeApi.setBatchSize(batchSize);

                        for (var i = 0; i < recordSize; i++)
                        {
                            var row = String.Join(",", new object[]
                            {
                                i + 1,
                                "'Elyor'",
                                "'Laipov'",
                                "'Software Developer'",
                                "now()",
                                "'Uzbekistan,Bukhara,Shofirkan'"
                            });

                            PQ8xNativeApi.addRow(row.AsPointer());
                        }

                        PQ8xNativeApi.runBulkCopy();
                        PQ8xNativeApi.closeConn();
                        PQ8xNativeApi.cleanUp();

                    }
                    break;
                case PgVersions.PQ9x:
                    {
                        PQ9xNativeApi.openLocaleConn(model.Connection.Database.AsPointer(), model.Connection.UserName.AsPointer(), model.Connection.Password.AsPointer());
                        PQ9xNativeApi.setColumns(columnData.AsPointer());
                        PQ9xNativeApi.setTableName(tableName.AsPointer());
                        PQ9xNativeApi.setBatchSize(batchSize);

                        for (var i = 0; i < recordSize; i++)
                        {
                            var row = String.Join(",", new object[]
                            {
                                i + 1,
                                "'Elyor'",
                                "'Laipov'",
                                "'Software Developer'",
                                "now()",
                                "'Uzbekistan,Bukhara,Shofirkan'"
                            });

                            PQ9xNativeApi.addRow(row.AsPointer());
                        }

                        PQ9xNativeApi.runBulkCopy();
                        PQ9xNativeApi.closeConn();
                        PQ9xNativeApi.cleanUp();

                    }
                    break;
            }


            Console.WriteLine("Bulk copy successfully");
            Console.ReadKey();
        }
    }
}
