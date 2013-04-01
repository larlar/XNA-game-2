using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Ludo
{
    class FourVsZero : GameLogic
    {

        public FourVsZero() { }

        public override GameLogic update()
        {
            Console.WriteLine("FourVsZero!");
            return this;
        }
        /* 
         * Class specific logic goes here.
         * Mostly how many AI's
         */

    }
}
