using MonoGame.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Microsoft.Xna.Framework.WPFForms
{
    /// <summary>
    /// Interaction logic for WPFMonoGameContainer.xaml
    /// </summary>
    public partial class WPFMonoGameContainer : UserControl
    {
        public WPFMonoGameContainer()
        {
            InitializeComponent();
        }


        public Game Game
        {
            get { return (Game)GetValue(GameProperty); }
            set { SetValue(GameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Game.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GameProperty =
            DependencyProperty.Register("Game", typeof(Game), typeof(WPFMonoGameContainer), new PropertyMetadata(null, OnGameChanged));

        private static void OnGameChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            WPFMonoGameContainer instance = obj as WPFMonoGameContainer;
            if (instance != null)
            {
                instance.Game = e.NewValue as Game;
                if (instance.Game != null)
                    instance.Game.HostControl = instance.HostPanel;
            }
        }


        public Image Host
        {
            get
            {
                return HostPanel;
            }
        }

        private void HostPanel_KeyDown(object sender, KeyEventArgs e)
        {
            //WPFFormsGameWindow window = Game?.Window as WPFFormsGameWindow;
            //if (window != null)
            //{
            //    // Forward the event
            //    window.KeyDown(e);
            //}
        }

        private void HostPanel_KeyUp(object sender, KeyEventArgs e)
        {
            //WPFFormsGameWindow window = Game?.Window as WPFFormsGameWindow;
            //if (window != null)
            //{
            //    // Forward the event
            //    window.KeyUp(e);
            //}

        }

        private void HostPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //WPFFormsGameWindow window = Game?.Window as WPFFormsGameWindow;
            //if (window != null)
            //{
            //    // Forward the event
            //    window.MouseDown(e);
            //}

        }

        private void HostPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            //WPFFormsGameWindow window = Game?.Window as WPFFormsGameWindow;
            //if (window != null)
            //{
            //    // Forward the event
            //    window.MouseEnter(e);
            //}

        }

        private void HostPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            //WPFFormsGameWindow window = Game?.Window as WPFFormsGameWindow;
            //if (window != null)
            //{
            //    // Forward the event
            //    window.MouseLeave(e);
            //}

        }

        private void HostPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //WPFFormsGameWindow window = Game?.Window as WPFFormsGameWindow;
            //if (window != null)
            //{
            //    // Forward the event
            //    window.MouseLeftButtonDown(e);
            //}

        }

        private void HostPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //WPFFormsGameWindow window = Game?.Window as WPFFormsGameWindow;
            //if (window != null)
            //{
            //    // Forward the event
            //    window.MouseLeftButtonUp(e);
            //}

        }

        private void HostPanel_MouseMove(object sender, MouseEventArgs e)
        {
            //WPFFormsGameWindow window = Game?.Window as WPFFormsGameWindow;
            //if (window != null)
            //{
            //    // Forward the event
            //    window.MouseMove(e);
            //}

        }

        private void HostPanel_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //WPFFormsGameWindow window = Game?.Window as WPFFormsGameWindow;
            //if (window != null)
            //{
            //    // Forward the event
            //    window.MouseRightButtonDown(e);
            //}

        }

        private void HostPanel_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            //WPFFormsGameWindow window = Game?.Window as WPFFormsGameWindow;
            //if (window != null)
            //{
            //    // Forward the event
            //    window.MouseRightButtonUp(e);
            //}

        }

        private void HostPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //WPFFormsGameWindow window = Game?.Window as WPFFormsGameWindow;
            //if (window != null)
            //{
            //    // Forward the event
            //    window.MouseUp(e);
            //}

        }

        private void HostPanel_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //WPFFormsGameWindow window = Game?.Window as WPFFormsGameWindow;
            //if (window != null)
            //{
            //    // Forward the event
            //    window.MouseWheel(e);
            //}

        }

        private void HostPanel_TouchDown(object sender, TouchEventArgs e)
        {
            WPFFormsGameWindow window = Game?.Window as WPFFormsGameWindow;
            if (window != null)
            {
                // Forward the event
                window.TouchDown(sender, e);
            }
        }

        private void HostPanel_TouchMove(object sender, TouchEventArgs e)
        {

            WPFFormsGameWindow window = Game?.Window as WPFFormsGameWindow;
            if (window != null)
            {
                // Forward the event
                window.TouchMove(sender, e);
            }
        }

        private void HostPanel_TouchUp(object sender, TouchEventArgs e)
        {
            WPFFormsGameWindow window = Game?.Window as WPFFormsGameWindow;
            if (window != null)
            {
                // Forward the event
                window.TouchUp(sender, e);
            }
        }

        private void HostPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Console.WriteLine($"Size: {e.NewSize.Width}:{e.NewSize.Height}");
        }
    }
}
