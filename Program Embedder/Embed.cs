using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Program_Embedder
{
    internal class Embed
    {
        public static string activeProcess = "";
        private static IntPtr dockedWindow = IntPtr.Zero;

        public static void ReSize(Panel panel) => Win32.MoveWindow(dockedWindow, 0, 0, panel.Width, panel.Height, true);

        public static void Open(Panel panel)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (Process process = Process.Start(fileDialog.FileName))
                    {
                        process.WaitForInputIdle();
                        dockedWindow = process.MainWindowHandle;

                        while (process.MainWindowHandle == IntPtr.Zero)
                        {
                            Thread.Sleep(100);
                            process.Refresh();
                        }

                        Win32.SetParent(process.MainWindowHandle, panel.Handle);
                        Win32.SetWindowLong(process.MainWindowHandle, -16, Win32.GetWindowLong(process.MainWindowHandle, 0x00800000));
                        Win32.ShowWindowAsync(process.MainWindowHandle, 3);
                        activeProcess = process.ProcessName;
                    }
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
