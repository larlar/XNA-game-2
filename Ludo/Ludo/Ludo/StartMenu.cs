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
    class StartMenu
    {
        public StartMenu(){} 

        Color colour = new Color(255, 255, 255, 255);
        Texture2D[] buttonArray = new Texture2D[6];
        Rectangle[] rectArray = new Rectangle[6];
        Texture2D menuBackground;
        MouseState mouse;
        Rectangle mousePos = new Rectangle(0, 0, 20, 20);

        public void LoadContent(ContentManager content)
        {
            buttonArray[0] = content.Load<Texture2D>("one-player-three-ai");
            buttonArray[1] = content.Load<Texture2D>("two-players-two-ai");
            buttonArray[2] = content.Load<Texture2D>("three-players-one-ai");
            buttonArray[3] = content.Load<Texture2D>("four-players-zero-ai");
            buttonArray[4] = content.Load<Texture2D>("zero-players-four-ai");
            buttonArray[5] = content.Load<Texture2D>("exit");
            menuBackground = content.Load<Texture2D>("background");
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            int heightSpacing = 50;
            int screenWidth = graphicsDevice.Viewport.Width;
            int screenHeight = graphicsDevice.Viewport.Height;

            //Draws background
            spriteBatch.Draw(menuBackground, new Rectangle(0, 0, screenWidth, screenHeight), colour);

            //Draws Menu Background
            for (int i = 0; i < buttonArray.Length; i++)
            {
                rectArray[i] = new Rectangle(screenWidth / 2 - 98, (screenHeight / 3 + (heightSpacing * i)) + 70, 197, 34); ; ;
                spriteBatch.Draw(buttonArray[i], rectArray[i], colour);
            }   
        }


        /*
         * Return correct instance of Game Model, based on which menu-item you select. 
         * Game Model instance = 1v4, 2v2 etc. 
         */
        public GameModel update(SpriteBatch spriteBatch)
        {
            mouse = Mouse.GetState();
            mousePos.X = mouse.X;
            mousePos.Y = mouse.Y;

            if (Keyboard.GetState().IsKeyDown(Keys.D1) || mousePos.Intersects(rectArray[0]) && mouse.LeftButton == ButtonState.Pressed)
                return new GameModel();
            else if (Keyboard.GetState().IsKeyDown(Keys.D2) || mousePos.Intersects(rectArray[1]) && mouse.LeftButton == ButtonState.Pressed)
                return new GameModel();
            else if (Keyboard.GetState().IsKeyDown(Keys.D3) || mousePos.Intersects(rectArray[2]) && mouse.LeftButton == ButtonState.Pressed)
                return new GameModel();
            else if (Keyboard.GetState().IsKeyDown(Keys.D4) || mousePos.Intersects(rectArray[3]) && mouse.LeftButton == ButtonState.Pressed)
                return new GameModel();
            else if (Keyboard.GetState().IsKeyDown(Keys.D5) || mousePos.Intersects(rectArray[4]) && mouse.LeftButton == ButtonState.Pressed)
                return new GameModel();
            else if (Keyboard.GetState().IsKeyDown(Keys.LeftControl) && Keyboard.GetState().IsKeyDown(Keys.Q) || mousePos.Intersects(rectArray[5]) && mouse.LeftButton == ButtonState.Pressed)
                return new ExitLudo();
            else
                return null;
        }
    }
}


