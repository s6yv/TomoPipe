using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace CommTest
{
    //
    // Kod z: https://stackoverflow.com/a/15923324
    //

    class NativeConsole
    {

        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(UInt32 dwProcessId);
        [DllImport("kernel32.dll")]
        private static extern bool GetFileInformationByHandle(SafeFileHandle hFile,out BY_HANDLE_FILE_INFORMATION lpFileInformation);
        [DllImport("kernel32.dll")]
        private static extern SafeFileHandle GetStdHandle(UInt32 nStdHandle);
        [DllImport("kernel32.dll")]
        private static extern bool SetStdHandle(UInt32 nStdHandle, SafeFileHandle hHandle);
        [DllImport("kernel32.dll")]
        private static extern bool DuplicateHandle(
            IntPtr hSourceProcessHandle,
            SafeFileHandle hSourceHandle,
            IntPtr hTargetProcessHandle,
            out SafeFileHandle lpTargetHandle,
            UInt32 dwDesiredAccess,
            Boolean bInheritHandle,
            UInt32 dwOptions
            );
        private const UInt32 ATTACH_PARENT_PROCESS = 0xFFFFFFFF;
        private const UInt32 STD_OUTPUT_HANDLE = 0xFFFFFFF5;
        private const UInt32 STD_ERROR_HANDLE = 0xFFFFFFF4;
        private const UInt32 DUPLICATE_SAME_ACCESS = 2;

        struct BY_HANDLE_FILE_INFORMATION
        {
            public UInt32 FileAttributes;
            public FILETIME CreationTime;
            public FILETIME LastAccessTime;
            public FILETIME LastWriteTime;
            public UInt32 VolumeSerialNumber;
            public UInt32 FileSizeHigh;
            public UInt32 FileSizeLow;
            public UInt32 NumberOfLinks;
            public UInt32 FileIndexHigh;
            public UInt32 FileIndexLow;
        }

        public static void InitConsoleHandles()
        {
            SafeFileHandle hStdOut, hStdErr, hStdOutDup, hStdErrDup;
            BY_HANDLE_FILE_INFORMATION bhfi;

            hStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
            hStdErr = GetStdHandle(STD_ERROR_HANDLE);

            IntPtr hProcess = Process.GetCurrentProcess().Handle;
            DuplicateHandle(hProcess, hStdOut, hProcess, out hStdOutDup,
                0, true, DUPLICATE_SAME_ACCESS);
            DuplicateHandle(hProcess, hStdErr, hProcess, out hStdErrDup,
                0, true, DUPLICATE_SAME_ACCESS);

            AttachConsole(ATTACH_PARENT_PROCESS);

            if (GetFileInformationByHandle(GetStdHandle(STD_OUTPUT_HANDLE), out bhfi))
                SetStdHandle(STD_OUTPUT_HANDLE, hStdOutDup);
            else
                SetStdHandle(STD_OUTPUT_HANDLE, hStdOut);
            if (GetFileInformationByHandle(GetStdHandle(STD_ERROR_HANDLE), out bhfi))
                SetStdHandle(STD_ERROR_HANDLE, hStdErrDup);
            else
                SetStdHandle(STD_ERROR_HANDLE, hStdErr);
        }


    }
}
