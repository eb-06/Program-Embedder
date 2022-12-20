using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Program_Embedder
{
    internal class Embed
    {
        public static string activeProcess;
        private static IntPtr dockedWindow;

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr windowChild, IntPtr newWindowParent);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr window, int x, int y, int width, int height, bool image);

        [DllImport("user32.DLL")]
        private static extern int SetWindowLong(IntPtr window, int index, int newLong);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr window, int windowShow);

        [DllImport("user32.DLL")]
        private static extern int GetWindowLong(IntPtr window, int index);

        public static void ReSize(Panel panel) => MoveWindow(dockedWindow, 0, 0, panel.Width, panel.Height, true);

        public static void Open(Panel panel)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    Process process = Process.Start(fileDialog.FileName);
                    process.WaitForInputIdle();
                    dockedWindow = process.MainWindowHandle;
                    while (process.MainWindowHandle == IntPtr.Zero)
                    {
                        Thread.Sleep(100);
                        process.Refresh();
                    }
                    SetParent(process.MainWindowHandle, panel.Handle);
                    SetWindowLong(process.MainWindowHandle, -16, GetWindowLong(process.MainWindowHandle, 0x00800000));
                    ShowWindowAsync(process.MainWindowHandle, 3);
                    activeProcess = process.ProcessName;
                }
            }
        }

        public static void Close(string activeProcess)
        {
            foreach (Process process in Process.GetProcessesByName(activeProcess))
                process.Kill();
        }
    }
}