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
    class MenuButton
    {
        public MenuButton(){} 

        Color colour = new Color(255, 255, 255, 255);
        Texture2D[] buttonArray = new Texture2D[6];

        public void LoadContent(ContentManager content)
        {
            buttonArray[0] = content.Load<Texture2D>("one-player-three-ai");
            buttonArray[1] = content.Load<Texture2D>("two-players-two-ai");
            buttonArray[2] = content.Load<Texture2D>("three-players-one-ai");
            buttonArray[3] = content.Load<Texture2D>("four-players-zero-ai");
            buttonArray[4] = content.Load<Texture2D>("zero-players-four-ai");
            buttonArray[5] = content.Load<Texture2D>("exit");
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            int heightSpacing = 50;
            int screenWidth = graphicsDevice.Viewport.Width;
            int screenHeight = graphicsDevice.Viewport.Height;
            for (int i = 0; i < buttonArray.Length; i++)
            {
                spriteBatch.Draw(buttonArray[i], new Rectangle(screenWidth / 2 - 98, (screenHeight/5 + (heightSpacing*i)) + 70, 197, 34), colour);
            }
        }         
    }
}


