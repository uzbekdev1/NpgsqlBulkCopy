using System.IO;
using System.Runtime.InteropServices;

namespace Npgsql.BulkCopy.Native
{
    public static class Win32NativeApi
    {

        private const string KERNEL = "kernel32.dll";

        #region Environment
        [DllImport(KERNEL,
            SetLastError = true,
            CharSet = CharSet.Unicode)]
        private static extern bool SetDllDirectory(string lpPathName);

        public static void SetupDirectory(string dllPath)
        {
            var location = Path.GetFullPath(dllPath);

            SetDllDirectory(location);
        }
        #endregion

    }
}
