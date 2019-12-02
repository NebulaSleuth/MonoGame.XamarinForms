using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Microsoft.Xna.Framework.XamarinControls
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

        //protected override void InvalidateMeasure()
        //{
        //    base.InvalidateMeasure();
        //}

        //protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        //{
        //    if (AspectRatio != 0)
        //    {
        //        if (double.IsInfinity(heightConstraint) && double.IsInfinity(widthConstraint))
        //        {
        //            // fit to max
        //            if (AspectRatio < 1.0f)
        //                return new SizeRequest(new Size(double.MaxValue, double.MaxValue * AspectRatio));
        //            return new SizeRequest(new Size(double.MaxValue / AspectRatio, double.MaxValue));
        //        }
        //        else
        //        if (double.IsInfinity(heightConstraint))
        //        {
        //            // fit to width
        //            return new SizeRequest(new Size(widthConstraint, widthConstraint * AspectRatio));
        //        }
        //        else
        //        {
        //            double w = widthConstraint;
        //            double h = widthConstraint * AspectRatio;
        //            if (h > heightConstraint)
        //            {
        //                w = heightConstraint / AspectRatio;
        //                h = heightConstraint;
        //            }
        //            return new SizeRequest(new Size(w, h));
        //        }
        //    }
        //    return base.OnMeasure(widthConstraint, heightConstraint);
        //}

    }
}
