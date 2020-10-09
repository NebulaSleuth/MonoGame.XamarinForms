// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Diagnostics;
using System.Drawing;
using Microsoft.Xna.Framework;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Threading.Tasks;

namespace MonoGame.Framework
{
    class WPFFormsGamePlatform : GamePlatform
    {
        //internal static string LaunchParameters;

        private WPFFormsGameWindow _window;

        public WPFFormsGamePlatform(Game game)
            : base(game)
        {
            
            MediaPlayer.Reset();

            _window = new WPFFormsGameWindow(this);

            Keyboard.SetActive(true);

            TouchPanel.PrimaryWindow = _window;
            var touchCap = TouchPanel.GetCapabilities();

            Mouse.WindowHandle = Game.MainWindowHandle;
            Window = _window;
        }

        public void WaitForExit()
        {
			_window?.StopRendering();
			System.Threading.Thread.Sleep(50);
        }


        public override GameRunBehavior DefaultRunBehavior
        {
            get { return GameRunBehavior.Asynchronous; }
        }

        protected override void OnIsMouseVisibleChanged()
        {
            //_window.MouseVisibleToggled();
        }

        public override bool BeforeRun()
        {
            _window.UpdateWindows();
            return base.BeforeRun();
        }

        public override void BeforeInitialize()
        {
            base.BeforeInitialize();

            var gdm = Game.graphicsDeviceManager;
            if (gdm == null)
            {
                _window.Initialize(GraphicsDeviceManager.DefaultBackBufferWidth, GraphicsDeviceManager.DefaultBackBufferHeight);
            }
            else
            {
                var pp = Game.GraphicsDevice.PresentationParameters;
                _window.Initialize(pp);
            }
        }

        public override void RunLoop()
        {
            throw new NotSupportedException("The WPFForms platform does not support synchronous run loops");
        }

        public override void StartRunLoop()
        {
            Console.WriteLine("StartRunLoop");
            _window?.StartRendering();
        }

        public override void Exit()
        {
            _window?.StopRendering();

            RaiseAsyncRunLoopEnded();

            if (_window != null)
                _window.Dispose();
            _window = null;
            Window = null;
        }

        public override bool BeforeUpdate(GameTime gameTime)
        {
            return true;
        }

        public override bool BeforeDraw(GameTime gameTime)
        {
#if WINDOWS_SWAPCHAIN
            var device = Game.GraphicsDevice;
            if (device != null)
            {
                // For a UAP app we need to re-apply the
                // render target before every draw.  
                // 
                // I guess the OS changes it and doesn't restore it?
                device.ResetRenderTargets();
            }
#endif
            return true;
        }

        public override void EnterFullScreen()
        {
        }

        public override void ExitFullScreen()
        {
        }

        internal override void OnPresentationChanged(PresentationParameters pp)
        {
            Console.WriteLine("OnPresentationChanged");

            //_window.OnPresentationChanged(pp);
        }

        public override void EndScreenDeviceChange(string screenDeviceName, int clientWidth, int clientHeight)
        {
        }

        public override void BeginScreenDeviceChange(bool willBeFullScreen)
        {
            _window?.BeginScreenDeviceChange(willBeFullScreen);
        }

        public override void Log(string message)
        {
            Debug.WriteLine(message);
        }

        public override void Present()
        {
            var device = Game.GraphicsDevice;
            if (device != null)
            {
                device.Present();
            }
        }
		
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_window != null)
                {
                    _window.Dispose();
                    _window = null;
                    //Window = null;
                }
                Microsoft.Xna.Framework.Media.MediaManagerState.CheckShutdown();
            }

            base.Dispose(disposing);
        }
    }
}
