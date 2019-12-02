using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework
{
    public class GameView : Xamarin.Forms.ContentView
    {
        public Game Game { get; set; }
        public bool AutoStart { get; set; }
        public float AspectRatio { get; set; }

    }
}
