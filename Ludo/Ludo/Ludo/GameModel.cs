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
using System.Threading;
 
 
namespace Ludo
{
    class GameModel
    {
        Player yellowPlayer, redPlayer, greenPlayer, bluePlayer;
        Player currentPlayer;
 
        Rectangle mousePos;
        KeyboardState key;
        KeyboardState previousKey;
 
        MouseState previousMouse;
        MouseState mstate;
        Random rand = new Random();
        bool hasRolledDice;
        int lastBoardIndex;
        bool gameOver;
 
        public GameModel() {}
 
        public GameModel(Player y, Player g, Player b, Player r)
        {
            this.yellowPlayer = y;
            this.redPlayer = r;
            this.greenPlayer = g;
            this.bluePlayer = b;
 
            currentPlayer = redPlayer;
            mousePos = new Rectangle(0, 0, 1, 1);
            previousMouse = Mouse.GetState();
            lastBoardIndex = -1;
            gameOver = false;
        }
 
        public void update(GameTime gameTime)
        {
            if (currentPlayer.hasPlayerWon())
            {
                gameOver = true;
                return;
            }
 
            if(currentPlayer.isAI())
            {
               lastBoardIndex = ((IAi)currentPlayer).getAiMove(this);
               hasRolledDice = false;
               checkForKnockHome();
               Console.Out.WriteLine(currentPlayer.getColor() + " AI-player finished.");
               setNextPlayer();
            }
 
            previousKey = key;
            mstate = Mouse.GetState();
            key = Keyboard.GetState();                
 
            // Rolling dice while holding space
            if (key.IsKeyDown(Keys.Space) && currentPlayer.getThrowsLeft() > 0)
            {
                currentPlayer.rollDice();
                return;
            }
 
            //Stops rolling dice, when you release space. Finishes a throw, and subtracts from throwsLeft.
            if (key.IsKeyUp(Keys.Space) && previousKey.IsKeyDown(Keys.Space) && currentPlayer.getThrowsLeft() > 0)
            {
                currentPlayer.minusThrows();
                hasRolledDice = true;
                Console.WriteLine(currentPlayer.getColor() + " player has "+currentPlayer.getThrowsLeft() + " throws left.");
            }
            
            // Left mouse click
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released)
            {
                mousePos.X = mstate.X;
                mousePos.Y = mstate.Y;
                PieceSet[] pieces = currentPlayer.getPieceLoactions();
                for (int i = 0; i < pieces.Length; i++)
                {
 
                    Rectangle rect = BoardHelper.getFields()[pieces[i].getBoardIndex()].getRectangle();
                    Rectangle goal = BoardHelper.getFields()[62].getRectangle();

                    if (mousePos.Intersects(rect) && hasRolledDice == true && !mousePos.Intersects(goal)) // has clicked a piece object
                    {
                        lastBoardIndex = currentPlayer.move(i);
                        checkForKnockHome();
                        if (currentPlayer.hasExtraMove())
                            return;
                        hasRolledDice = false;
                        if (currentPlayer.hasPlayerWon())
                        {
                            gameOver = true;
                        }
                        setNextPlayer();
                    }
                }
                previousMouse = mstate;
            }
            else if (mstate.LeftButton == ButtonState.Released)
                    previousMouse = mstate;    
        }
 
        public PieceSet[] getYelloPositions()
        {
            return yellowPlayer.getPieceLoactions();
        }
 
        public PieceSet[] getRedPositions()
        {
            return redPlayer.getPieceLoactions();
        }
 
        public PieceSet[] getGreenPositions()
        {
            return greenPlayer.getPieceLoactions();
        }
 
        public PieceSet[] getBluePositions()
        {
            return bluePlayer.getPieceLoactions();
        }
 
        public Player getCurrentPlayer()
        {
            return currentPlayer;
        }
 
        public bool isGameOver()
        {
            return gameOver;
        }
 
        private void setNextPlayer()
        {
            if (currentPlayer.getColor() == Player.Color.Yellow)
                currentPlayer = greenPlayer;
            else if (currentPlayer.getColor() == Player.Color.Green)
                currentPlayer = bluePlayer;
            else if (currentPlayer.getColor() == Player.Color.Blue)
                currentPlayer = redPlayer;
            else if (currentPlayer.getColor() == Player.Color.Red)
                currentPlayer = yellowPlayer;
            Console.Out.WriteLine("Current player = "+currentPlayer.getColor());
        }

        // check if you land on another piece. if so; move other piece back to home.
        private void checkForKnockHome()
        {
            if (lastBoardIndex < 0) // did not move
                return;
            if (currentPlayer != yellowPlayer)
            {
                yellowPlayer.checkAndHitBack(lastBoardIndex);
                yellowPlayer.resetAmountOfThrows();
            }
            if (currentPlayer != greenPlayer)
            {
                greenPlayer.checkAndHitBack(lastBoardIndex);
                greenPlayer.resetAmountOfThrows();
            }
            if (currentPlayer != bluePlayer)
            {
                bluePlayer.checkAndHitBack(lastBoardIndex);
                bluePlayer.resetAmountOfThrows();
            }
            if (currentPlayer != redPlayer)
            {
                redPlayer.checkAndHitBack(lastBoardIndex);
                redPlayer.resetAmountOfThrows();
            }
        }

    }
}
