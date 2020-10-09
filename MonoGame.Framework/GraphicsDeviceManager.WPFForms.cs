using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if WINDOWS_SWAPCHAIN
using Windows.UI.Xaml.Controls;
#endif

namespace Microsoft.Xna.Framework
{
    partial class GraphicsDeviceManager
    {
        //[CLSCompliant(false)]

        //partial void PlatformPreparePresentationParameters(PresentationParameters presentationParameters)
        //{
        //    presentationParameters.BackBufferWidth = (int)(_game?.HostControl?.ActualWidth??800);
        //    presentationParameters.BackBufferHeight = (int)(_game?.HostControl?.ActualHeight ?? 480);
        //}
#if WINDOWS_SWAPCHAIN
        [CLSCompliant(false)]
        public SwapChainPanel SwapChainPanel { get; set; }

        partial void PlatformPreparePresentationParameters(PresentationParameters presentationParameters)
        {

            // The graphics device can use a XAML panel or a window
            // to created the default swapchain target.
            if (SwapChainPanel != null)
            {
                presentationParameters.DeviceWindowHandle = IntPtr.Zero;
                presentationParameters.SwapChainPanel = this.SwapChainPanel;
            }
            else
            {
                presentationParameters.DeviceWindowHandle = _game.Window.Handle;
                presentationParameters.SwapChainPanel = null;
            }
            presentationParameters.MultiSampleCount = 0; // Not supported in WinRT?
        }
#endif
    }
}
