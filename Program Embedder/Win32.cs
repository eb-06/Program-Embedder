using System;
using System.Runtime.InteropServices;

namespace Program_Embedder
{
    internal class Win32
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr windowChild, IntPtr newWindowParent);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr window, int x, int y, int width, int height, bool image);

        [DllImport("user32.DLL")]
        public static extern int SetWindowLong(IntPtr window, int index, int newLong);

        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr window, int windowShow);

        [DllImport("user32.DLL")]
        public static extern int GetWindowLong(IntPtr window, int index);
    }
}
