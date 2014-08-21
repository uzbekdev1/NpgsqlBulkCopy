NpgsqlBulkCopy
==============

Npgsql Bulk Copy


I simple demostration:

Scripts:

-- Database: northwind

-- DROP DATABASE northwind;

CREATE DATABASE northwind
  WITH OWNER = postgres
       ENCODING = 'UTF8'
       TABLESPACE = pg_default
       LC_COLLATE = 'English_United States.1252'
       LC_CTYPE = 'English_United States.1252'
       CONNECTION LIMIT = -1;
	   
-- Table: employees

-- DROP TABLE employees;

CREATE TABLE employees
(
  "EmployeeID" smallint NOT NULL,
  "LastName" character varying(20) NOT NULL,
  "FirstName" character varying(10) NOT NULL,
  "Title" character varying(30),
  "TitleOfCourtesy" character varying(25),
  "BirthDate" date,
  "HireDate" date,
  "Address" character varying(60),
  "City" character varying(15),
  "Region" character varying(15),
  "PostalCode" character varying(10),
  "Country" character varying(15),
  "HomePhone" character varying(24),
  "Extension" character varying(4),
  "Photo" bytea,
  "Notes" text,
  "ReportsTo" smallint,
  "PhotoPath" character varying(255),
  CONSTRAINT pk_employees PRIMARY KEY ("EmployeeID")
)
WITH (
  OIDS=FALSE
);
ALTER TABLE employees
  OWNER TO postgres;



Config file:

BulkCopyConfig.xml

<BulkCopy>
  <MajorVersion>9</MajorVersion> 
  <Connection>
    <Host>127.0.0.1</Host>
    <Port>5432</Port>
    <UserName>northwind</UserName>
    <UserName>postgres</UserName>
    <Password>web@1234</Password>
  </Connection>
</BulkCopy>


Source:


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
        
        
Performance:

Batch size:10.000 , Rows -> 10.000.000 records to load time ~5.8 min
Batch size: 100.000 ,Rows-> 10.000.000 records to load time ~5.9 min
