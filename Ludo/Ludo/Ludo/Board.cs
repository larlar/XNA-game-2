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
    class Board
    {
        private Field[] fields;
        private GameModel model;
        
        Texture2D[] yellowTextures = new Texture2D[4];
        Texture2D[] greenTextures = new Texture2D[4];
        Texture2D[] blueTextures = new Texture2D[4];
        Texture2D[] redTextures = new Texture2D[4];
        Texture2D[] diceTextures = new Texture2D[6];
        SpriteFont sideMenuFont;

        public Board()
        {
            fields = BoardHelper.getFields();
        }

        public void setModel(GameModel model)
        {
            this.model = model;
        }
        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            int screenWidth = graphicsDevice.Viewport.Width;
            int screenHeight = graphicsDevice.Viewport.Height;

            drawPieces(model.getYelloPositions(), yellowTextures, spriteBatch);
            drawPieces(model.getGreenPositions(), greenTextures, spriteBatch);
            drawPieces(model.getBluePositions(), blueTextures, spriteBatch);
            drawPieces(model.getRedPositions(), redTextures, spriteBatch);

            //sideboard
            spriteBatch.Draw(diceTextures[model.getDice().getValue() - 1], new Rectangle(screenWidth - 170, 120, 80, 80), Color.White); //dice
            spriteBatch.DrawString(sideMenuFont, "Current turn: ", new Vector2(screenWidth-250, 250), Color.Black); //current turn: text
            drawCurrentPlayer(spriteBatch, graphicsDevice); // curent turn icon
        }

        private void drawPieces(PieceSet[] set, Texture2D[] textures, SpriteBatch spriteBatch) 
        {
            for (int i = 0; i < set.Length; i++)
            {
                if (set[i].getNumPieces() >0)
                    fields[set[i].getBoardIndex()].draw(spriteBatch, textures[set[i].getNumPieces()-1]); 
            }
        }


        public void LoadContent(ContentManager Content)
        {
            yellowTextures[0] = Content.Load<Texture2D>("yellow-brick-one");
            yellowTextures[1] = Content.Load<Texture2D>("yellow-brick-two");
            yellowTextures[2] = Content.Load<Texture2D>("yellow-brick-three");
            yellowTextures[3] = Content.Load<Texture2D>("yellow-brick-four");
            greenTextures[0] = Content.Load<Texture2D>("green-brick-one");
            greenTextures[1] = Content.Load<Texture2D>("green-brick-two");
            greenTextures[2] = Content.Load<Texture2D>("green-brick-three");
            greenTextures[3] = Content.Load<Texture2D>("green-brick-four");
            blueTextures[0] = Content.Load<Texture2D>("blue-brick-one");
            blueTextures[1] = Content.Load<Texture2D>("blue-brick-two");
            blueTextures[2] = Content.Load<Texture2D>("blue-brick-three");
            blueTextures[3] = Content.Load<Texture2D>("blue-brick-four");
            redTextures[0] = Content.Load<Texture2D>("red-brick-one");
            redTextures[1] = Content.Load<Texture2D>("red-brick-two");
            redTextures[2] = Content.Load<Texture2D>("red-brick-three");
            redTextures[3] = Content.Load<Texture2D>("red-brick-four");
            sideMenuFont = Content.Load<SpriteFont>("sideMenuFont");
            diceTextures[0] = Content.Load<Texture2D>("Die_1");
            diceTextures[1] = Content.Load<Texture2D>("Die_2");
            diceTextures[2] = Content.Load<Texture2D>("Die_3");
            diceTextures[3] = Content.Load<Texture2D>("Die_4");
            diceTextures[4] = Content.Load<Texture2D>("Die_5");
            diceTextures[5] = Content.Load<Texture2D>("Die_6");
        }

        private void drawCurrentPlayer(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            int screenWidth = graphicsDevice.Viewport.Width;
            int screenHeight = graphicsDevice.Viewport.Height;
            Rectangle rect = new Rectangle(screenWidth - 80, 238, 45, 45);

            if (model.getCurrentPlayer().getColor() == Player.Color.Yellow)
                spriteBatch.Draw(yellowTextures[0], rect, Color.White);
            else if (model.getCurrentPlayer().getColor() == Player.Color.Green)
                spriteBatch.Draw(greenTextures[0], rect, Color.White);
            else if (model.getCurrentPlayer().getColor() == Player.Color.Blue)
                spriteBatch.Draw(blueTextures[0], rect, Color.White);
            else if (model.getCurrentPlayer().getColor() == Player.Color.Red)
                spriteBatch.Draw(redTextures[0], rect, Color.White);
        }


    }
}