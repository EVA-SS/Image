using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.ServiceProcess
{
    [ComVisible(false), SuppressUnmanagedCodeSecurity()]
    internal static class SafeNativeMethods
    {
        [DllImport(ExternDll.Advapi32, CharSet = CharSet.Unicode, SetLastError = true)]
        public extern static IntPtr OpenSCManager(string machineName, string databaseName, int access);

        [DllImport(ExternDll.Advapi32, CharSet = CharSet.Unicode, SetLastError = true), ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public extern static bool CloseServiceHandle(IntPtr handle);

        [DllImport(ExternDll.Advapi32, CharSet = CharSet.Unicode, SetLastError = false)]
        public static extern int LsaClose(IntPtr objectHandle);

        [DllImport(ExternDll.Advapi32, SetLastError = false)]
        public static extern int LsaFreeMemory(IntPtr ptr);

        [DllImport(ExternDll.Advapi32, CharSet = CharSet.Unicode, SetLastError = false)]
        public static extern int LsaNtStatusToWinError(int ntStatus);
    }
}