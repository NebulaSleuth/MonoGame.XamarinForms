﻿// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Windows;
using MonoGame.Framework.WPFForms;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Point = System.Drawing.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using XnaPoint = Microsoft.Xna.Framework.Point;

namespace MonoGame.Framework
{
    class WPFGameWindow : GameWindow, IDisposable
    {
        System.Windows.Controls.Image Host;

        static private ReaderWriterLockSlim _allWindowsReaderWriterLockSlim = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        static private List<WPFGameWindow> _allWindows = new List<WPFGameWindow>();

        private WPFGamePlatform _platform;
        private bool _switchingFullScreen;


        TouchLocationState TouchState = TouchLocationState.Invalid;

        private readonly object _graphicsDeviceLock = new object();
        private GraphicsDevice _graphicsDevice;
        private GraphicsDeviceManager _gfxManager;
        private int _referenceCount;
        private RenderTarget2D _renderTarget = null;
        private RenderTarget2D _renderTarget2 = null;
        private SpriteBatch _aaSpriteBatch = null;
        private D3D11Image _d3D11Image;
        private readonly Stopwatch _timer;
        private TimeSpan _lastRenderingTime;
        private bool _resetBackBuffer;

        private bool _isResizable;
        private bool _isMouseHidden;
        private bool _isMouseInBounds;

        // true if window position was moved either through code or by dragging/resizing the form
        #region Internal Properties

        internal Game Game { get; private set; }

        #endregion

        #region Public Properties

        public override IntPtr Handle { get { return IntPtr.Zero; } }

        public override string ScreenDeviceName { get { return String.Empty; } }

        public override Rectangle ClientBounds
        {
            get
            {
                if (Host != null)
                {
                    //var position = Host.PointToScreen(new System.Windows.Point(0, 0));
                    //var size = Host.RenderSize;
                    if (Game.AspectRatio != 0)
                    {
                        System.Drawing.Size bounds = new System.Drawing.Size((int)Host.ActualWidth, (int)Host.ActualHeight);


                        float displayAspectRatio = (float)bounds.Width / (float)bounds.Height;
                        float preferredAspectRatio = Game.AspectRatio;

                        float adjustedAspectRatio = preferredAspectRatio;

                        if ((preferredAspectRatio > 1.0f && displayAspectRatio < 1.0f) ||
                            (preferredAspectRatio < 1.0f && displayAspectRatio > 1.0f))
                        {
                            // Invert preferred aspect ratio if it's orientation differs from the display mode orientation.
                            // This occurs when user sets preferredBackBufferWidth/Height and also allows multiple supported orientations
                            adjustedAspectRatio = 1.0f / preferredAspectRatio;
                        }

                        const float EPSILON = 0.01f;
                        var newClientBounds = new Rectangle();
                        if (displayAspectRatio > (adjustedAspectRatio + EPSILON))
                        {
                            // Fill the entire height and reduce the width to keep aspect ratio
                            newClientBounds.Height = (int)bounds.Height;
                            newClientBounds.Width = (int)(newClientBounds.Height * adjustedAspectRatio);
                            newClientBounds.X = (int)(bounds.Width - newClientBounds.Width) / 2;
                        }
                        else if (displayAspectRatio < (adjustedAspectRatio - EPSILON))
                        {
                            // Fill the entire width and reduce the height to keep aspect ratio
                            newClientBounds.Width = (int)bounds.Width;
                            newClientBounds.Height = (int)(newClientBounds.Width / adjustedAspectRatio);
                            newClientBounds.Y = (int)(bounds.Height - newClientBounds.Height) / 2;
                        }
                        else
                        {
                            // Set the ClientBounds to match the DisplayMode
                            newClientBounds.Width = (int)bounds.Width;
                            newClientBounds.Height = (int)bounds.Height;
                        }

                        Host.Stretch = Stretch.Uniform;
                        return newClientBounds;
                    }
                    else
                    {
                        Host.Stretch = Stretch.Fill;
                        return new Rectangle(0, 0, (int)Host.ActualWidth, (int)Host.ActualHeight);
                    }
                }
                return new Rectangle(0,0,0,0);
            }
        }

        public override bool AllowUserResizing
        {
            get { return _isResizable; }
            set
            {
                if (_isResizable != value)
                {
                    _isResizable = value;
                    //Form.MaximizeBox = _isResizable;
                }
                else
                    return;
                //Form.FormBorderStyle = _isResizable ? FormBorderStyle.Sizable : FormBorderStyle.FixedSingle;
            }
        }

        public override DisplayOrientation CurrentOrientation
        {
            get { return DisplayOrientation.Default; }
        }

        public override XnaPoint Position
        {
            get
            {
                if (Host != null)
                {
                    var pt = Host.PointToScreen(new System.Windows.Point(0, 0));
                    return new XnaPoint((int)pt.X, (int)pt.Y);
                }
                return new XnaPoint(0, 0);
            }
            set
            {
                //_wasMoved = true;
                //Form.Location = new Point(value.X, value.Y);
                RefreshAdapter();
            }
        }

        protected internal override void SetSupportedOrientations(DisplayOrientation orientations)
        {
        }


        public bool IsFullScreen { get; private set; }
        public bool HardwareModeSwitch { get; private set; }

        #endregion

        internal WPFGameWindow(WPFGamePlatform platform)
        {
            _timer = new Stopwatch();
            //IsFullScreen = true;
            _platform = platform;
            Game = platform.Game;
            if (Game.HostControl != null)
            {
                Host = Game.HostControl;

                Host.SizeChanged += OnResize;
                Host.Unloaded += OnDeactivate;
                Host.Loaded += OnActivated;

                Window parentWindow = Window.GetWindow(Host);
                // Capture mouse events.
                Microsoft.Xna.Framework.Input.Mouse.WindowHandle = new WindowInteropHelper(parentWindow).Handle;
                Host.PreviewMouseWheel += OnMouseScroll;
                //Host.MouseHorizontalWheel += OnMouseHorizontalScroll;
                Host.MouseEnter += OnMouseEnter;
                Host.MouseLeave += OnMouseLeave;

//                Touch.FrameReported += new TouchFrameEventHandler(Touch_FrameReported);
                //Host.KeyDown += OnKeyPress;
            }
            RegisterToAllWindows();
        }

        void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {
            if (Host != null)
            {
                foreach (TouchPoint _touchPoint in e.GetTouchPoints(Host))
                {
                    if (_touchPoint.Action == TouchAction.Down)
                    {
                        // capture the touch 
                        _touchPoint.TouchDevice.Capture(Host);

                    }

                    else if (_touchPoint.Action == TouchAction.Move && e.GetPrimaryTouchPoint(Host) != null)
                    {
                        // This is the first (primary) touch point. Just record its position.
                        if (_touchPoint.TouchDevice.Id == e.GetPrimaryTouchPoint(Host).TouchDevice.Id)
                        {
                            var vec = new Vector2((float)_touchPoint.Position.X, (float)_touchPoint.Position.Y);
                            TouchState = TouchLocationState.Pressed;
                            TouchPanelState.AddEvent(_touchPoint.TouchDevice?.Id ?? 0, TouchState, vec, false);
                        }

                        // This is not the first touch point. Draw a line from the first point to this one.
                        else if (_touchPoint.TouchDevice.Id != e.GetPrimaryTouchPoint(Host).TouchDevice.Id)
                        {
                            var vec = new Vector2((float)_touchPoint.Position.X, (float)_touchPoint.Position.Y);
                            TouchState = TouchLocationState.Moved;
                            TouchPanelState.AddEvent(_touchPoint.TouchDevice?.Id ?? 0, TouchState, vec, false);
                            //pt2.X = _touchPoint.Position.X;
                            //pt2.Y = _touchPoint.Position.Y;

                            //Line _line = new Line();
                            //_line.Stroke = new RadialGradientBrush(Colors.White, Colors.Black);
                            //_line.X1 = pt1.X;
                            //_line.X2 = pt2.X;
                            //_line.Y1 = pt1.Y;
                            //_line.Y2 = pt2.Y;
                            //_line.StrokeThickness = 2;
                            //this.canvas1.Children.Add(_line);
                        }
                    }
                    else if (_touchPoint.Action == TouchAction.Up)
                    {
                        // If this touch is captured to the canvas, release it.
                        if (_touchPoint.TouchDevice.Captured == Host)
                        {
                            var vec = new Vector2((float)_touchPoint.Position.X, (float)_touchPoint.Position.Y);
                            TouchState = TouchLocationState.Released;
                            TouchPanelState.AddEvent(_touchPoint.TouchDevice?.Id ?? 0, TouchState, vec, false);
                        }
                    }
                }
            }
        }


        ~WPFGameWindow()
        {
            if (Host != null)
            {
                Host.SizeChanged -= OnResize;
                Host.Unloaded -= OnDeactivate;
                Host.Loaded -= OnActivated;

                //Host.MouseHorizontalWheel += OnMouseHorizontalScroll;
                Host.MouseEnter -= OnMouseEnter;
                Host.MouseLeave -= OnMouseLeave;
                //Host.KeyDown += OnKeyPress;

                //Touch.FrameReported -= new TouchFrameEventHandler(Touch_FrameReported);

            }

            Dispose(false);
            _timer?.Stop();
        }

        private void RegisterToAllWindows()
        {
            _allWindowsReaderWriterLockSlim.EnterWriteLock();

            try
            {
                _allWindows.Add(this);
            }
            finally
            {
                _allWindowsReaderWriterLockSlim.ExitWriteLock();
            }
        }

        private void UnregisterFromAllWindows()
        {
            _allWindowsReaderWriterLockSlim.EnterWriteLock();

            try
            {
                _allWindows.Remove(this);
            }
            finally
            {
                _allWindowsReaderWriterLockSlim.ExitWriteLock();
            }
        }

        private void OnActivated(object sender, EventArgs eventArgs)
        {
            if (_platform == null) return;
            if (Host == null) return;

            //if (_graphicsDevice == null && !Game.Initialized)
            //{
            //    InitializeGraphicsDevice(Game);
            //    InitializeImageSource();
            //}
            _resetBackBuffer = true;


            if (_platform != null)
                _platform.IsActive = true;
        }

        private void OnDeactivate(object sender, EventArgs eventArgs)
        {
            if (_platform != null)
                _platform.IsActive = false;

            StopRendering();
            UnitializeImageSource();
            UninitializeGraphicsDevice();
        }

        private void InitializeGraphicsDevice(Game game)
        {
            lock (_graphicsDeviceLock)
            {
                _referenceCount++;
                if (_referenceCount == 1)
                {
                    // Create Direct3D 11 device.
                    var presentationParameters = new PresentationParameters
                    {
                        // Do not associate graphics device with window.
                        DeviceWindowHandle = IntPtr.Zero,
                    };

                    //presentationParameters.IsFullScreen = true;
                    _graphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, presentationParameters);
                    if (game.Services.GetService(typeof(IGraphicsDeviceManager)) != null)
                    {
                        game.Services.RemoveService(typeof(IGraphicsDeviceManager));
                    }
                    _gfxManager = new GraphicsDeviceManager(game, _graphicsDevice);
                    _gfxManager.PreferMultiSampling = true;


                }
            }
        }

        private void UninitializeGraphicsDevice()
        {
            lock (_graphicsDeviceLock)
            {
                _referenceCount--;
                if (_referenceCount == 0)
                {
                    _graphicsDevice.Dispose();
                    _graphicsDevice = null;
                }
            }
        }

        private void OnIsFrontBufferAvailableChanged(object sender, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (_d3D11Image.IsFrontBufferAvailable)
            {
                StartRendering();
                _resetBackBuffer = true;
            }
            else
            {
                StopRendering();
            }
        }

        public void StartRendering()
        {
            if (_timer.IsRunning)
                return;

            CompositionTarget.Rendering += OnRendering;
            _timer.Start();
        }


        public void StopRendering()
        {

            lock (_graphicsDeviceLock)
            {
                CompositionTarget.Rendering -= OnRendering;

                if (_timer?.IsRunning == true)
                    _timer.Stop();
                _resetBackBuffer = false;
            }

        }

        private void OnRendering(object sender, EventArgs eventArgs)
        {
            if (!_timer.IsRunning)
                return;

            // Recreate back buffer if necessary.

            // CompositionTarget.Rendering event may be raised multiple times per frame
            // (e.g. during window resizing).
            lock (_graphicsDeviceLock)
            {

                if (_resetBackBuffer)
                {
                    CreateBackBuffer();
                }
                _d3D11Image?.Lock();
                var renderingEventArgs = (RenderingEventArgs)eventArgs;
                if (_lastRenderingTime != renderingEventArgs.RenderingTime || _resetBackBuffer)
                {
                    _lastRenderingTime = renderingEventArgs.RenderingTime;

                    if (_graphicsDevice != null)
                        _graphicsDevice.DefaultRenderTarget = _renderTarget;

                    _graphicsDevice?.SetRenderTarget(_renderTarget);
                    Render(_timer.Elapsed);

                    if (_renderTarget2 != null && _aaSpriteBatch != null) // AntiAlias
                    {
                        _graphicsDevice.SetRenderTarget(_renderTarget2);
                        _aaSpriteBatch.Begin();
                        _aaSpriteBatch.Draw(_renderTarget, new Rectangle(0, 0, _renderTarget2.Width, _renderTarget2.Height), Microsoft.Xna.Framework.Color.White);
                        _aaSpriteBatch.End();
                    }                 
                    _graphicsDevice?.Flush();
                }
                _d3D11Image?.Unlock();
                _resetBackBuffer = false;
            }
            _d3D11Image?.Invalidate(); // Always invalidate D3DImage to reduce flickering
                                      // during window resizing.
        }
        private void Render(TimeSpan time)
        {
            if (Game == null) return;

            if (Game.IsActive)
                Game.RunOneFrame();

        }

        private void InitializeImageSource()
        {
            _d3D11Image = new D3D11Image();
            _d3D11Image.IsFrontBufferAvailableChanged += OnIsFrontBufferAvailableChanged;
            CreateBackBuffer();
            Host.Source = _d3D11Image;
        }


        private void UnitializeImageSource()
        {
            if (_d3D11Image != null)
                _d3D11Image.IsFrontBufferAvailableChanged -= OnIsFrontBufferAvailableChanged;
            if (Host != null) Host.Source = null;

            if (_d3D11Image != null)
            {
                _d3D11Image.Dispose();
                _d3D11Image = null;
            }
            if (_renderTarget != null)
            {
                _renderTarget.Dispose();
                _renderTarget = null;
            }
            if (_renderTarget2 != null)
            {
                _renderTarget2.Dispose();
                _renderTarget2 = null;
            }
            if (_aaSpriteBatch != null)
            {
                _aaSpriteBatch.Dispose();
                _aaSpriteBatch = null;
            }
        }


        private void CreateBackBuffer()
        {
            try
            {
                //if (_d3D11Image != null)
                //    _d3D11Image.SetBackBuffer(null);
                if (_renderTarget != null)
                {
                    _renderTarget.Dispose();
                    _renderTarget = null;
                }
                if (_renderTarget2 != null)
                {
                    _renderTarget2.Dispose();
                    _renderTarget2 = null;
                }
                if (_aaSpriteBatch != null)
                {
                    _aaSpriteBatch.Dispose();
                    _aaSpriteBatch = null;
                }
                if (Host != null)
                {

                    int width = Math.Max((int)ClientBounds.Width, 1);
                    int height = Math.Max((int)ClientBounds.Height, 1);
                    int mcnt = 0;
                    if (_gfxManager.PreferMultiSampling)
                    {
                        mcnt = _graphicsDevice.GraphicsCapabilities.MaxMultiSampleCount;
                        _renderTarget = new RenderTarget2D(_graphicsDevice, width*2, height*2, false, SurfaceFormat.Bgr32, DepthFormat.Depth24Stencil8, mcnt, RenderTargetUsage.PreserveContents, true);
                        _renderTarget2 = new RenderTarget2D(_graphicsDevice, width, height, false, SurfaceFormat.Bgr32, DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.PreserveContents, true);
                        _aaSpriteBatch = new SpriteBatch(_graphicsDevice);
                        _d3D11Image.SetBackBuffer(_renderTarget2);
                    }
                    else
                    {
                        _aaSpriteBatch = null;
                        _renderTarget = new RenderTarget2D(_graphicsDevice, width, height, false, SurfaceFormat.Bgr32, DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.PreserveContents, true);
                        _renderTarget2 = null;
                        _d3D11Image.SetBackBuffer(_renderTarget);
                    }
                }
            }
            catch { }
        }



        private void OnMouseScroll(object sender, System.Windows.Input.MouseWheelEventArgs mouseEventArgs)
        {
            MouseState.ScrollWheelValue += mouseEventArgs.Delta;
        }

        private void OnMouseHorizontalScroll(object sender, HorizontalMouseWheelEventArgs mouseEventArgs)
        {
            MouseState.HorizontalScrollWheelValue += mouseEventArgs.Delta;
        }

        private void UpdateMouseState()
        {
            // If we call the form client functions before the form has
            // been made visible it will cause the wrong window size to
            // be applied at startup.
            if (Host == null) return;

            if (!Host.IsVisible)
                return;

            var pos = System.Windows.Input.Mouse.GetPosition(Host);
            //var clientPos = new System.Drawing.Point((int)pos.X, (int)pos.Y);
            Window parentWindow = Window.GetWindow(Host);
            var relativePoint = Host.TransformToAncestor(parentWindow).Transform(new System.Windows.Point(0, 0));
            Rectangle r = new Rectangle((int)relativePoint.X, (int)relativePoint.Y, (int)Host.ActualWidth, (int)Host.ActualHeight);
            var withinClient = r.Contains((int)pos.X, (int)pos.Y);
            var buttons = Control.MouseButtons;

            var previousState = MouseState.LeftButton;
            var prevX = MouseState.X;
            var prevY = MouseState.Y;

            MouseState.X = (int)pos.X;
            MouseState.Y = (int)pos.Y;
            MouseState.LeftButton = (buttons & MouseButtons.Left) == MouseButtons.Left ? ButtonState.Pressed : ButtonState.Released;
            MouseState.MiddleButton = (buttons & MouseButtons.Middle) == MouseButtons.Middle ? ButtonState.Pressed : ButtonState.Released;
            MouseState.RightButton = (buttons & MouseButtons.Right) == MouseButtons.Right ? ButtonState.Pressed : ButtonState.Released;
            MouseState.XButton1 = (buttons & MouseButtons.XButton1) == MouseButtons.XButton1 ? ButtonState.Pressed : ButtonState.Released;
            MouseState.XButton2 = (buttons & MouseButtons.XButton2) == MouseButtons.XButton2 ? ButtonState.Pressed : ButtonState.Released;


            //if (MouseState.LeftButton == ButtonState.Pressed) Debug.WriteLine("MOUSE: DOWN");
            //// Don't process touch state if we're not active 
            //// and the mouse is within the client area.
            if (!_platform.IsActive || !withinClient)
            {
                //if (MouseState.LeftButton == ButtonState.Pressed)
                //{
                //    if (previousState == ButtonState.Pressed && MouseState.X != prevX && MouseState.Y != prevY)
                //    {
                //        // Moved
                //        Debug.WriteLine("MOVED");
                //        TouchPanelState.AddEvent(0, TouchLocationState.Moved, new Vector2(MouseState.X, MouseState.Y), false);
                //    }
                //    else if (previousState != ButtonState.Pressed)
                //    {
                //        Debug.WriteLine("PRESSED");
                //        TouchPanelState.AddEvent(0, TouchLocationState.Pressed, new Vector2(MouseState.X, MouseState.Y), false);
                //    }
                //}
                //else if (previousState != ButtonState.Released)
                //{
                //        Debug.WriteLine("RELEASED");
                //    TouchPanelState.AddEvent(0, TouchLocationState.Released, new Vector2(MouseState.X, MouseState.Y), false);
                //}
                //    if (MouseState.LeftButton == ButtonState.Pressed)
                //    {
                //        // Release mouse TouchLocation
                //        var touchX = MathHelper.Clamp(MouseState.X, 0, (int)Host.ActualWidth - 1);
                //        var touchY = MathHelper.Clamp(MouseState.Y, 0, (int)Host.ActualHeight - 1);
                //        TouchPanelState.AddEvent(0, TouchLocationState.Released, new Vector2(touchX, touchY), true);
                //    }
                //    return;
            }

            //TouchLocationState? touchState = null;
            //if (MouseState.LeftButton == ButtonState.Pressed)
            //    if (previousState == ButtonState.Released)
            //        touchState = TouchLocationState.Pressed;
            //    else
            //        touchState = TouchLocationState.Moved;
            //else if (previousState != ButtonState.Released)
            //    touchState = TouchLocationState.Released;

            //if (touchState.HasValue)
            //{
            //    Debug.WriteLine($"TOUCH: {touchState}");
            //    TouchPanelState.AddEvent(0, touchState.Value, new Vector2(MouseState.X, MouseState.Y), true);
            //}
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            _isMouseInBounds = true;
            if (!_platform.IsMouseVisible && !_isMouseHidden)
            {
                _isMouseHidden = true;

                //System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.None;
            }
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            _isMouseInBounds = false;
            if (_isMouseHidden)
            {
                _isMouseHidden = false;
                //System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        [DllImport("user32.dll")]
        private static extern short VkKeyScanEx(char ch, IntPtr dwhkl);

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            var key = (Keys)(VkKeyScanEx(e.KeyChar, InputLanguage.CurrentInputLanguage.Handle) & 0xff);
            OnTextInput(sender, new TextInputEventArgs(e.KeyChar, key));
        }

        internal void Initialize(int width, int height)
        {
            Console.WriteLine("Initialize");
            //ChangeClientSize(new Size(width, height));
        }

        internal void Initialize(PresentationParameters pp)
        {
            Console.WriteLine("Initialize");
            //ChangeClientSize(new Size(pp.BackBufferWidth, pp.BackBufferHeight));

            if (pp.IsFullScreen)
            {
                EnterFullScreen(pp);
                if (!pp.HardwareModeSwitch)
                    _platform.Game.GraphicsDevice.OnPresentationChanged();
            }
        }

        private void EnterFullScreen(PresentationParameters pp)
        {
            _switchingFullScreen = true;

            // store the location of the window so we can restore it later
            //if (!IsFullScreen)
            //    _locationBeforeFullScreen = Form.Location;

            _platform.Game.GraphicsDevice.SetHardwareFullscreen();

            //if (!pp.HardwareModeSwitch)
            //{
            //    // FIXME: setting the WindowState to Maximized when the form is not shown will not update the ClientBounds
            //    // this causes the back buffer to be the wrong size when initializing in soft full screen
            //    // we show the form to bypass the issue
            //    //Form.Show();
            //    IsBorderless = true;
            //    //Form.WindowState = FormWindowState.Maximized;
            //    //_lastFormState = FormWindowState.Maximized;
            //}

            IsFullScreen = true;
            HardwareModeSwitch = pp.HardwareModeSwitch;

            _switchingFullScreen = false;
        }

        //private FormWindowState _lastFormState;

        private void OnResize(object sender, SizeChangedEventArgs eventArgs)
        {
            //if (_switchingFullScreen || Form.IsResizing)
            //    return;

            _resetBackBuffer = true;
            OnClientSizeChanged();
        }

        //private void OnResizeEnd(object sender, EventArgs eventArgs)
        //{
        //    _wasMoved = true;
        //    if (Game.Window == this)
        //    {
        //        UpdateBackBufferSize();
        //        RefreshAdapter();
        //    }

        //    OnClientSizeChanged();
        //}

        private void RefreshAdapter()
        {
            // the display that the window is on might have changed, so we need to
            // check and possibly update the Adapter of the GraphicsDevice
            if (Game.GraphicsDevice != null)
                Game.GraphicsDevice.RefreshAdapter();
        }


        internal void UpdateWindows()
        {
            _allWindowsReaderWriterLockSlim.EnterReadLock();

            try
            {
                // Update the mouse state for each window.
                foreach (var window in _allWindows)
                    if (window.Game == Game)
                        window.UpdateMouseState();
            }
            finally
            {
                _allWindowsReaderWriterLockSlim.ExitReadLock();
            }
        }

        //private const uint WM_QUIT = 0x12;

        //[StructLayout(LayoutKind.Sequential)]
        //public struct NativeMessage
        //{
        //    public IntPtr handle;
        //    public uint msg;
        //    public IntPtr wParam;
        //    public IntPtr lParam;
        //    public uint time;
        //    public System.Drawing.Point p;
        //}

        //internal void ChangeClientSize(System.Windows.Size clientBounds)
        //{
        //    var prevIsResizing = Form.IsResizing;
        //    // make sure we don't see the events from this as a user resize
        //    Form.IsResizing = true;

        //    if(this.Form.ClientSize != clientBounds)
        //        this.Form.ClientSize = clientBounds;

        //    // if the window wasn't moved manually and it's resized, it should be centered
        //    if (!_wasMoved)
        //        Form.CenterOnPrimaryMonitor();

        //    Form.IsResizing = prevIsResizing;
        //}

        //[System.Security.SuppressUnmanagedCodeSecurity] // We won't use this maliciously
        //[DllImport("User32.dll", CharSet = CharSet.Auto)]
        //private static extern bool PeekMessage(out NativeMessage msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (disposing)
            {
                Microsoft.Xna.Framework.Media.MediaPlayer.Stop();
                StopRendering();
                UnitializeImageSource();
                UninitializeGraphicsDevice();

                if (Host != null)
                {
                    UnregisterFromAllWindows();
                    Host = null;
                }
            }

            _platform = null;
            Game = null;
            //Mouse.WindowHandle = IntPtr.Zero;
        }

        public override void BeginScreenDeviceChange(bool willBeFullScreen)
        {
            if (Host != null)
            {
                UnitializeImageSource();
                UninitializeGraphicsDevice();

                Host.SizeChanged -= OnResize;
                Host.Unloaded -= OnDeactivate;
                Host.Loaded -= OnActivated;
                Host = null;
            }

            if (Game.HostControl != null)
            {

                Host = Game.HostControl;

                InitializeGraphicsDevice(Game);
                InitializeImageSource();


                Host.SizeChanged += OnResize;
                Host.Unloaded += OnDeactivate;
                Host.Loaded += OnActivated;
            }
        }

        public override void EndScreenDeviceChange(string screenDeviceName, int clientWidth, int clientHeight)
        {
        }

        protected override void SetTitle(string title)
        {
        }


        #endregion


        public void KeyUp(System.Windows.Input.KeyEventArgs e)
        {

        }


        public void KeyDown(System.Windows.Input.KeyEventArgs e)
        {

        }

        public void MouseDown(MouseButtonEventArgs e)
        {

        }

        public void MouseEnter(System.Windows.Input.MouseEventArgs e)
        {

        }

        public void MouseLeave(System.Windows.Input.MouseEventArgs e)
        {

        }

        public void MouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (Host != null)
            {
                MouseState.LeftButton = ButtonState.Pressed;
                var position = e.GetPosition(Host);
                MouseState.X = (int)position.X;
                MouseState.Y = (int)position.Y;
            }

        }

        public void MouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (Host != null)
            {
                MouseState.LeftButton = ButtonState.Released;
                var position = e.GetPosition(Host);
                MouseState.X = (int)position.X;
                MouseState.Y = (int)position.Y;
            }

        }

        public void MouseMove(System.Windows.Input.MouseEventArgs e)
        {
            if (Host != null)
            {
                var position = e.GetPosition(Host);
                MouseState.X = (int)position.X;
                MouseState.Y = (int)position.Y;
            }

        }

        public void MouseRightButtonDown(MouseButtonEventArgs e)
        {

        }

        public void MouseRightButtonUp(MouseButtonEventArgs e)
        {

        }

        public void MouseUp(MouseButtonEventArgs e)
        {

        }

        public void MouseWheel(MouseWheelEventArgs e)
        {

        }

        public void TouchDown(object sender, TouchEventArgs e)
        {
            if (Host != null)
            {
                var position = e.GetTouchPoint(Host);
                var vec = new Vector2((float)position.Position.X, (float)position.Position.Y);
                TouchState = TouchLocationState.Pressed;
                TouchPanelState.AddEvent(e.TouchDevice?.Id??0, TouchState, vec, false);
            }
        }
        
        public void TouchMove(object sender, TouchEventArgs e)
        {
            if (Host != null && TouchState != TouchLocationState.Released && TouchState != TouchLocationState.Invalid)
            {
                var position = e.GetTouchPoint(Host);
                var vec = new Vector2((float)position.Position.X, (float)position.Position.Y);
                TouchState = TouchLocationState.Moved;
                TouchPanelState.AddEvent(e.TouchDevice?.Id??0, TouchState, vec, false);
            }
        }

        public void TouchUp(object sender, TouchEventArgs e)
        {
            if (Host != null)
            {
                var position = e.GetTouchPoint(Host);
                var vec = new Vector2((float)position.Position.X, (float)position.Position.Y);
                TouchState = TouchLocationState.Released;
                TouchPanelState.AddEvent(e.TouchDevice?.Id??0, TouchState, vec, false);
            }

        }
    }
}

