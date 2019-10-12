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
using Windows.System.Threading;

namespace MonoGame.Framework
{
    class WPFFormsGamePlatform : GamePlatform
    {
        //internal static string LaunchParameters;

        private WPFFormsSCPGameWindow _window;
        internal bool IsExiting { get; set; }


        public WPFFormsGamePlatform(Game game)
            : base(game)
        {
            
            MediaPlayer.Reset();

            _window = new WPFFormsSCPGameWindow(this);

            Keyboard.SetActive(true);

            TouchPanel.PrimaryWindow = _window;
            var touchCap = TouchPanel.GetCapabilities();

            Mouse.WindowHandle = Game.MainWindowHandle;
            Window = _window;
        }

        public void UpdateWindow(Game game)
        {
#if FORMS
            ((WPFFormsSCPGameWindow)Window).Dispose();
            ((WPFFormsSCPGameWindow)Window).Initialize(game.SwapChainPanel);
#endif

        }

#if FORMS
        public override GameRunBehavior DefaultRunBehavior
        {
            get { return GameRunBehavior.Asynchronous; }
        }
#else
        public override GameRunBehavior DefaultRunBehavior
        {
            get { return GameRunBehavior.Synchronous; }
        }
#endif
        public override void RunLoop()
        {
            IsExiting = false;
            ((WPFFormsSCPGameWindow)Window).RunLoop();
        }

        private bool _exited;

        public override void StartRunLoop()
        {
            IsExiting = false;

            var workItemHandler = new WorkItemHandler((action) =>
            {
                _exited = false;
                while (!IsExiting)
                {
                    ((WPFFormsSCPGameWindow)Window).Tick();
                }
                _exited = true;
#if FORMS
                RaiseAsyncRunLoopEnded();
#endif
            });
            var tickWorker = ThreadPool.RunAsync(workItemHandler, WorkItemPriority.High, WorkItemOptions.TimeSliced);
        }

        public override void Exit()
        {
#if FORMS
            IsExiting = true;
#else
            if (!IsExiting)
            {
				IsExiting = true;
                Application.Current.Exit(); 
            }
#endif
        }

        public void WaitForExit()
        {
            while (!_exited)
            {
                System.Threading.Thread.Sleep(10);
            }
        }
        //public override bool BeforeUpdate(GameTime gameTime)
        //{
        //    TouchQueue.ProcessQueued();
        //    return true;
        //}

        //public override bool BeforeDraw(GameTime gameTime)
        //{
        //    var device = Game.GraphicsDevice;
        //    if (device != null)
        //    {
        //        // For a UAP app we need to re-apply the
        //        // render target before every draw.  
        //        // 
        //        // I guess the OS changes it and doesn't restore it?
        //        device.ResetRenderTargets();
        //    }

        //    return true;
        //}
        //public override bool BeforeRun()
        //{
        //    _window.UpdateWindows();
        //    return base.BeforeRun();
        //}

        //public override void BeforeInitialize()
        //{
        //    base.BeforeInitialize();

        //    var gdm = Game.graphicsDeviceManager;
        //    if (gdm == null)
        //    {
        //        _window.Initialize(GraphicsDeviceManager.DefaultBackBufferWidth, GraphicsDeviceManager.DefaultBackBufferHeight);
        //    }
        //    else
        //    {
        //        var pp = Game.GraphicsDevice.PresentationParameters;
        //        _window.Initialize(pp);
        //    }
        //}

        public override void EndScreenDeviceChange(string screenDeviceName, int clientWidth, int clientHeight)
        {
        }

        public override void BeginScreenDeviceChange(bool willBeFullScreen)
        {
        }

        public override void Log(string Message)
        {
            Debug.WriteLine(Message);
        }

        public override void Present()
        {
            var device = Game.GraphicsDevice;
            if (device != null)
                device.Present();
        }

        //public override void Exit()
        //{
        //    _window?.StopRendering();

        //    RaiseAsyncRunLoopEnded();

        //    if (_window != null)
        //        _window.Dispose();
        //    _window = null;
        //    Window = null;
        //}

        public override bool BeforeUpdate(GameTime gameTime)
        {
            return true;
        }

        public override bool BeforeDraw(GameTime gameTime)
        {
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
