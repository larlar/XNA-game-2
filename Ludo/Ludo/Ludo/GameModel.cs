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
        MouseState previousMouse;
        Rectangle mousePos;
        Dice dice;
        KeyboardState key;
        KeyboardState previousKey;

        public GameModel()
        {
            yellowPlayer = new Player(Player.Color.Yellow, Player.Type.human);
            redPlayer = new Player(Player.Color.Red, Player.Type.ai);
            greenPlayer = new Player(Player.Color.Green, Player.Type.ai);
            bluePlayer = new Player(Player.Color.Blue, Player.Type.ai);
            currentPlayer = yellowPlayer;
            mousePos = new Rectangle(0, 0, 1, 1);
            previousMouse = Mouse.GetState();
            dice = new Dice();
        }
        public void update(GameTime gameTime)
        {
            if (currentPlayer.isAI())
            {
                Console.WriteLine("This is an AI!");
                setNextPlayer();
                return;
            }

            previousKey = key;
            key = Keyboard.GetState();
            if (key.IsKeyDown(Keys.Space) && previousKey.IsKeyUp(Keys.Space))
            {
                dice.roll();
                return;
            }

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
                        set[i].move(dice.getValue(), set);
                        dice.roll();
                        Console.Out.WriteLine(dice.getValue());
                        setNextPlayer();
                    }

                    previousMouse = mstate;


                }
            }
            else
            {
                if (mstate.LeftButton == ButtonState.Released)
                    previousMouse = mstate;
            }

            /*
             * currentPlayer.move();
             * dice.isNewRoll();
             * 
             */
        }

        public void movePiece(PieceSet set, int diceValue)
        {

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
        }
        public Dice getDice()
        {
            return dice;
        }
    }

    public class Dice
    {
        private const int sides = 6;
        private static Random rand = new Random();
        private int value;

        // setting defaulted diceSide to number1
        public Dice()
        {
            roll();
        }

        // get method for the diceSide value
        public int getValue()
        {
            return value;
        }

        // The basic roll method that rolles the dice to a random side every thime the dice(s) are rolled.
        public void roll()
        {
            value = rand.Next(1, sides + 1);
        }
    }
}
