using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Microsoft.Xna.Framework
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameView : ContentView
    {
        public static readonly BindableProperty GameProperty = BindableProperty.Create("Game", typeof(Game), typeof(GameView), null, BindingMode.TwoWay, null,
                (bindable, oldvalue, newvalue) =>
                {
                    ((GameView)bindable).Game = newvalue as Game;
                });


        public Game Game
        {
            get
            {
                return (Game)GetValue(GameProperty);
            }
            set
            {
                SetValue(GameProperty, value);
            }
        }

        public static readonly BindableProperty AutoStartProperty = BindableProperty.Create("AutoStart", typeof(bool), typeof(GameView), null, BindingMode.TwoWay, null,
                (bindable, oldvalue, newvalue) =>
                {
                    ((GameView)bindable).AutoStart = (bool)newvalue;
                });


        public bool AutoStart
        {
            get
            {
                return (bool)GetValue(AutoStartProperty);
            }
            set
            {
                SetValue(AutoStartProperty, value);
            }
        }


        public static readonly BindableProperty AspectRatioProperty = BindableProperty.Create("AspectRatio", typeof(float), typeof(GameView), 0.0f, BindingMode.TwoWay, null,
                (bindable, oldvalue, newvalue) =>
                {
                    ((GameView)bindable).AspectRatio = (float)newvalue;
                });


        public float AspectRatio
        {
            get
            {
                return (float)GetValue(AspectRatioProperty);
            }
            set
            {
                SetValue(AspectRatioProperty, value);
            }
        }

        public GameView()
        {
            InitializeComponent();

        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
        }
    }
}
