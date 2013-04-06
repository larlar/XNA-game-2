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
    class OneVsThree: GameModel
    {

        public OneVsThree() { }
       
        public override GameModel update()
        {
            Console.WriteLine("OneVsThree!");
            return this;
        }


        /* 
         * Class specific logic goes here. 
         * Mostly how many AI's. 
         */ 
        
    }
}
