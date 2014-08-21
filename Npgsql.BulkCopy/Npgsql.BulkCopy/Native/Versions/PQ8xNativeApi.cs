using System.Runtime.InteropServices;

namespace Npgsql.BulkCopy.Native.Versions
{
    public unsafe static class PQ8xNativeApi
    {
        private const string PQDLL_NAME = @"PgCPP.DLL.8.x.dll";

        #region DllImport

        [DllImport(PQDLL_NAME,
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl,
            SetLastError = true,
            EntryPoint = "openLocaleConn")]
        public static extern void openLocaleConn(char* dbname, char* user, char* password);

        [DllImport(PQDLL_NAME,
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl,
            SetLastError = true,
            EntryPoint = "openRemoteConn")]
        public static extern void openRemoteConn(char* hostaddr, int port, char* dbname, char* user, char* password);

        [DllImport(PQDLL_NAME,
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl,
            SetLastError = true,
            EntryPoint = "cleanUp")]
        public static extern void cleanUp();

        [DllImport(PQDLL_NAME,
          CharSet = CharSet.Unicode,
          CallingConvention = CallingConvention.Cdecl,
          SetLastError = true,
          EntryPoint = "closeConn")]
        public static extern void closeConn();

        [DllImport(PQDLL_NAME,
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl,
            SetLastError = true,
            EntryPoint = "setColumns")]
        public static extern void setColumns(char* columns);

        [DllImport(PQDLL_NAME,
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl,
            SetLastError = true,
            EntryPoint = "setTableName")]
        public static extern void setTableName(char* tableName);

        [DllImport(PQDLL_NAME,
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl,
            SetLastError = true,
            EntryPoint = "setBatchSize")]
        public static extern void setBatchSize(int batchSize);

        [DllImport(PQDLL_NAME,
           CharSet = CharSet.Unicode,
           CallingConvention = CallingConvention.Cdecl,
           SetLastError = true,
           EntryPoint = "addRow")]
        public static extern void addRow(char* row);

        [DllImport(PQDLL_NAME,
           CharSet = CharSet.Unicode,
           CallingConvention = CallingConvention.Cdecl,
           SetLastError = true,
           EntryPoint = "runBulkCopy")]
        public static extern void runBulkCopy();

        #endregion

    }
}
