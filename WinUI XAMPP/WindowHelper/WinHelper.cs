using System;
using System.Runtime.InteropServices;

using Microsoft.UI.Xaml;

using WinRT.Interop;

using static WinUI_XAMPP.WindowHelper.WinHelper;

namespace WinUI_XAMPP.WindowHelper
{
    public class WinHelper
    {
        // Define the MINMAXINFO structure
        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        // Define the POINT structure
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        // Import the necessary functions from user32.dll
        public static class NativeMethods
        {
            [DllImport("user32.dll")]
            public static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

            [DllImport("user32.dll")]
            public static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

            public const int GWLP_WNDPROC = -4;
            public const uint WM_GETMINMAXINFO = 0x0024;
        }

        // Define the custom window procedure at the class level
        public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        public static WndProcDelegate _wndProcDelegate;
    }

    public class CustomWindowHelper
    {
        private static IntPtr _originalWndProcPtr;
        private IntPtr _hWnd;

        public CustomWindowHelper(Window window)
        {
            _hWnd = WindowNative.GetWindowHandle(window);
            _wndProcDelegate = new WndProcDelegate(WindowProc);
            _originalWndProcPtr = NativeMethods.SetWindowLongPtr(_hWnd, NativeMethods.GWLP_WNDPROC, Marshal.GetFunctionPointerForDelegate(_wndProcDelegate));
        }

        private static IntPtr WindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            if (msg == NativeMethods.WM_GETMINMAXINFO)
            {
                MINMAXINFO mmi = Marshal.PtrToStructure<MINMAXINFO>(lParam);
                mmi.ptMinTrackSize.x = 700; // Set the minimum width here
                mmi.ptMinTrackSize.y = 500; // Set the minimum height here
                Marshal.StructureToPtr(mmi, lParam, true);
            }

            return NativeMethods.CallWindowProc(_originalWndProcPtr, hWnd, msg, wParam, lParam);
        }
    }

}
