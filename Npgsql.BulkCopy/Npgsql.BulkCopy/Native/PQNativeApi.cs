using System;
using Npgsql.BulkCopy.Enums;

namespace Npgsql.BulkCopy.Native
{
    public static class PQNativeApi
    {
        public const string DLL_DIR_NAME = "libpqDLL";

        public static void LoadDLLDirectory(PgVersions version)
        {
            var majorVersion = (int) version;
            var dllDirectory = String.Concat(Environment.CurrentDirectory, Environment.Is64BitProcess
                    ? string.Format("\\{0}\\{1}.x\\x64", DLL_DIR_NAME, majorVersion)
                    : string.Format("\\{0}\\{1}.x\\x86", DLL_DIR_NAME, majorVersion));

            Win32NativeApi.SetupDirectory(dllDirectory);
        }

    }
}
