﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Microsoft.Xna.Framework.XamarinForms
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    [CLSCompliant(false)]
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

        public GameView ()
		{
			InitializeComponent ();
		}

    }
}