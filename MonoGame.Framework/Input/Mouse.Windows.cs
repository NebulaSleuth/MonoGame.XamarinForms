// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Input
{
    public static partial class Mouse
    {
        [DllImportAttribute("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int X, int Y);

#if FORMS
        private static IntPtr _window;

        private static IntPtr PlatformGetWindowHandle()
        {
            return _window;
        }

        private static void PlatformSetWindowHandle(IntPtr windowHandle)
        {
            _window = windowHandle;
        }
#else
        private static Control _window;

        private static IntPtr PlatformGetWindowHandle()
        {
            return _window.Handle;
        }

        private static void PlatformSetWindowHandle(IntPtr windowHandle)
        {
            _window = Control.FromHandle(windowHandle);
        }
#endif
        private static MouseState PlatformGetState(GameWindow window)
        {
            return window.MouseState;
        }

        private static void PlatformSetPosition(int x, int y)
        {
            PrimaryWindow.MouseState.X = x;
            PrimaryWindow.MouseState.Y = y;

#if FORMS
            
            //var pt = _window.PointToScreen(new System.Drawing.Point(x, y));
            //SetCursorPos(pt.X, pt.Y);
#else
            var pt = _window.PointToScreen(new System.Drawing.Point(x, y));
            SetCursorPos(pt.X, pt.Y);
#endif
        }

        public static void PlatformSetCursor(MouseCursor cursor)
        {
#if FORMS
#else
            _window.Cursor = cursor.Cursor;
#endif
        }
    }
}
