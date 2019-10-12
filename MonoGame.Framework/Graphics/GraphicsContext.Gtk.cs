// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Gtk;
using OpenTK;
using OpenTK.GLWidget;

namespace MonoGame.OpenGL
{
    internal class GraphicsContext : IGraphicsContext, IDisposable
    {
        private GLWidget _glarea;

        public int SwapInterval { get; set; }

        public bool IsCurrent => true;

        public bool IsDisposed => false;

        public GraphicsContext(IWindowInfo info)
        {
            _glarea = GtkGameWindow.TempGLArea;

            // GL entry points must be loaded after the GL context creation, otherwise some Windows drivers will return only GL 1.3 compatible functions
            try
            {
                OpenGL.GL.LoadEntryPoints();
            }
            catch (EntryPointNotFoundException)
            {
                throw new PlatformNotSupportedException(
                    "MonoGame requires OpenGL 3.0 compatible drivers, or either ARB_framebuffer_object or EXT_framebuffer_object extensions. " +
                    "Try updating your graphics drivers.");
            }
        }

        public void MakeCurrent(IWindowInfo info)
        {
            _glarea.Realize();
            //_glarea.MakeCurrent();
        }

        public void SwapBuffers()
        {

        }

        public void Dispose()
        {
            if (IsDisposed)
                return;
        }

        private void SetWindowHandle(IWindowInfo info)
        {

        }
    }
}
