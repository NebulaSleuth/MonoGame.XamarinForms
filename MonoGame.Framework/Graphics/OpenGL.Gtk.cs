// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Runtime.InteropServices;
using MonoGame.Utilities;

namespace MonoGame.OpenGL
{
    partial class GL
    {
        private static IntPtr Library;

        static partial void LoadPlatformEntryPoints()
        {
            BoundApi = RenderApi.GL;

            if (CurrentPlatform.OS == OS.Windows) // TODO: Use wglGetProcAddress on Windows
                Library = FuncLoader.LoadLibrary("opengl32.dll");
            else if (CurrentPlatform.OS == OS.Linux)
                Library = FuncLoader.LoadLibrary("libGL.so.1");
            else
                Library = FuncLoader.LoadLibrary("/System/Library/Frameworks/OpenGL.framework/OpenGL");
        }

        private static T LoadFunction<T>(string function, bool throwIfNotFound = false)
        {
            return FuncLoader.LoadFunction<T>(Library, function, throwIfNotFound);
        }

        private static IGraphicsContext PlatformCreateContext(IWindowInfo info)
        {
            return new GraphicsContext(info);
        }
    }
}
