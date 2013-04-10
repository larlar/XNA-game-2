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
    class GameModel
    {
        Player yellowPlayer,redPlayer,greenPlayer,bluePlayer;
        Player currentPlayer;
        MouseState previousMouse;
        Rectangle mousePos;
       
        
        public GameModel()
        {
            yellowPlayer = new Player(Player.Color.Yellow, Player.type.human);
            redPlayer = new Player(Player.Color.Red, Player.type.ai);
            greenPlayer = new Player(Player.Color.Green, Player.type.ai);
            bluePlayer = new Player(Player.Color.Blue, Player.type.ai);
            currentPlayer = yellowPlayer;
            mousePos = new Rectangle(0, 0, 1, 1);
            previousMouse = Mouse.GetState();
        }
        public void update(GameTime gameTime)
        {
            MouseState mstate = Mouse.GetState();
            if (previousMouse.LeftButton == ButtonState.Released
                        && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                mousePos.X = mstate.X;
                mousePos.Y = mstate.Y;

                PieceSet[] set = currentPlayer.getPieceLoactions();
                for (int i = 0; i < set.Length; i++)
                {
                    Rectangle rect = BoardHelper.getFields()[set[i].getBoardIndex()].getRectangle();
                    if (mousePos.Intersects(rect))
                    {
                        set[i].move(1);
                        setNextPlayer();
                    }

                    previousMouse = mstate;
                    
                    
                }
            } else {
                if (mstate.LeftButton == ButtonState.Released)
                    previousMouse = mstate;
            }

            /*
             * currentPlayer.move();
             * dice.isNewRoll();
             * 
             */
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
        private void setNextPlayer(){
            if (currentPlayer.getColor() == Player.Color.Yellow)
                currentPlayer = greenPlayer;
            else if (currentPlayer.getColor() == Player.Color.Green)
                currentPlayer = bluePlayer;
            else if (currentPlayer.getColor() == Player.Color.Blue)
                currentPlayer = redPlayer;
            else if (currentPlayer.getColor() == Player.Color.Red)
                currentPlayer = yellowPlayer;
        }       
    }
}
