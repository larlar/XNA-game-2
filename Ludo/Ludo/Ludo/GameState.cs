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
    abstract class GameLogic
    {
        public abstract GameLogic update();
        
        /* Mutual logic goes here.
         * Such as rolling a dice
         * Logic for moving a piece,
         * which direction the piece is going etc.
         */
    }
}
