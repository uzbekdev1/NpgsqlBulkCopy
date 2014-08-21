using System.Runtime.InteropServices;

namespace Npgsql.BulkCopy.Extensions
{
    public static class NativeExtension
    {
        #region Convert

        public static unsafe char* AsPointer(this string str)
        {
            return (char*)Marshal.StringToHGlobalAnsi(str);
        }

        #endregion
         
    }
}
