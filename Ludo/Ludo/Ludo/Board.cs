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
        Texture2D winScreen;
        SpriteFont sideMenuFont;
        SpriteFont rulesFont;

        public Board()
        {
            fields = BoardHelper.getFields();
        }

        public void setModel(GameModel model)
        {
            this.model = model;
        }

        public void LoadContent(ContentManager Content)
        {
            winScreen = Content.Load<Texture2D>("winscreen");

            sideMenuFont = Content.Load<SpriteFont>("sideMenuFont");
            rulesFont = Content.Load<SpriteFont>("rulesFont");

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
            diceTextures[0] = Content.Load<Texture2D>("Die_1");
            diceTextures[1] = Content.Load<Texture2D>("Die_2");
            diceTextures[2] = Content.Load<Texture2D>("Die_3");
            diceTextures[3] = Content.Load<Texture2D>("Die_4");
            diceTextures[4] = Content.Load<Texture2D>("Die_5");
            diceTextures[5] = Content.Load<Texture2D>("Die_6");
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            int screenWidth = graphicsDevice.Viewport.Width;
            int screenHeight = graphicsDevice.Viewport.Height;

            //pieces
            drawPieces(model.getYelloPositions(), yellowTextures, spriteBatch);
            drawPieces(model.getGreenPositions(), greenTextures, spriteBatch);
            drawPieces(model.getBluePositions(), blueTextures, spriteBatch);
            drawPieces(model.getRedPositions(), redTextures, spriteBatch);

            //sideboard
            spriteBatch.Draw(diceTextures[model.getCurrentPlayer().getDiceValue() - 1], new Rectangle(screenWidth - 170, 120, 80, 80), Color.White); //dice
            spriteBatch.DrawString(sideMenuFont, "Current turn: ", new Vector2(screenWidth - 220, 270), Color.Black); //current turn: text
            drawCurrentPlayerInSideboard(spriteBatch, graphicsDevice); // curent turn icon
            spriteBatch.DrawString(rulesFont, "RULES: \n\n1. In order to move a piece out \nof your base, you need to \nroll a 6. \n\n 1.1 As long as all your pieces \n are inside your base \n or in goal, you have 3 tries \n\n2. If your piece lands on a piece \nof a different colour, the other \npiece is sent back to it's start.\n\n 2.1 This applies to stacks of\n pieces aswell \n\n3. If your piece lands on a pice \nof it's own colour the pieces \nwill get stacked. \n\n4. If you roll a 6, you have \nanother turn \n\n5. In order to move a piece to \ngoal, you need to roll the exact \ndistance. If the roll too high, \nthe piece will move to goal, and \nthen 'bounce' back.", new Vector2(680, 120), Color.Black);
            
            //victory-screen
            if (model.isGameOver())
            {
                spriteBatch.Draw(winScreen, new Rectangle(0, 0, screenWidth, screenHeight), Color.Beige);
                spriteBatch.DrawString(sideMenuFont, "WE HAVE A WINNER!", new Vector2(screenWidth / 2 - 80, 150), Color.Black);
                spriteBatch.DrawString(sideMenuFont, "..and the winner is:", new Vector2(screenWidth / 2 - 170, 373), Color.Black);
                spriteBatch.DrawString(sideMenuFont, "Thank you for playing! Press 'ESCAPE' to go back to the Main Menu", new Vector2(screenWidth / 2 - 360, 600), Color.Black);
                drawWinner(spriteBatch, graphicsDevice);
            }
        }

        //draws pieces
        private void drawPieces(PieceSet[] set, Texture2D[] textures, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < set.Length; i++)
            {
                if (set[i].getNumPieces() > 0)
                    fields[set[i].getBoardIndex()].draw(spriteBatch, textures[set[i].getNumPieces() - 1]);
            }
        }

        //draws the current player's piececolor. 
        private void drawCurrentPlayerInSideboard(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            int screenWidth = graphicsDevice.Viewport.Width;
            int screenHeight = graphicsDevice.Viewport.Height;
            Rectangle rect = new Rectangle(screenWidth - 70, 258, 45, 45);

            if (model.getCurrentPlayer().getColor() == Player.Color.Yellow)
                spriteBatch.Draw(yellowTextures[0], rect, Color.White);
            else if (model.getCurrentPlayer().getColor() == Player.Color.Green)
                spriteBatch.Draw(greenTextures[0], rect, Color.White);
            else if (model.getCurrentPlayer().getColor() == Player.Color.Blue)
                spriteBatch.Draw(blueTextures[0], rect, Color.White);
            else if (model.getCurrentPlayer().getColor() == Player.Color.Red)
                spriteBatch.Draw(redTextures[0], rect, Color.White);
        }

        //draws the winning player's piececolor
        private void drawWinner(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            int screenWidth = graphicsDevice.Viewport.Width;
            int screenHeight = graphicsDevice.Viewport.Height;
            Rectangle rect = new Rectangle(screenWidth / 2 +70, 330, 100, 100);

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