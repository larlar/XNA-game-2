﻿using System;
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
        Dice dice;
        KeyboardState key;
        KeyboardState previousKey;

        MouseState previousMouse;
        MouseState mstate;
        Random rand = new Random();
        bool hasRolledDice;
        int lastBoardIndex = -1;

        public GameModel()
        {
            yellowPlayer = new Player(Player.Color.Yellow, Player.Type.human);
            redPlayer = new Player(Player.Color.Red, Player.Type.human);
            greenPlayer = new Player(Player.Color.Green, Player.Type.human);
            bluePlayer = new Player(Player.Color.Blue, Player.Type.human);
            currentPlayer = redPlayer;
            mousePos = new Rectangle(0, 0, 1, 1);
            previousMouse = Mouse.GetState();
            dice = new Dice();
        }
        public void update(GameTime gameTime)

        {
            //checks if a player has won. if a player has won, his turn will be skipped all the time.
            if (currentPlayer.hasPlayerWon())
            {
                setNextPlayer();
            }

            if (currentPlayer.isAI())
            {
                bestAiMove();
                return;
            }

            previousKey = key;
            mstate = Mouse.GetState();
            key = Keyboard.GetState();                 

            if (key.IsKeyDown(Keys.Space) && currentPlayer.getThrowsLeft() > 0) //roll animation
            {
                dice.roll();
                hasRolledDice = true;
                //return;
            }

            if (key.IsKeyUp(Keys.Space) && previousKey.IsKeyDown(Keys.Space) && currentPlayer.getThrowsLeft() > 0)
            {
                currentPlayer.minusThrows();
                //Console.WriteLine(currentPlayer.getThrowsLeft());
            }

            
            if (previousMouse.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {

                mousePos.X = mstate.X;
                mousePos.Y = mstate.Y;
                PieceSet[] pieceSet = currentPlayer.getPieceLoactions();
                Rectangle goal = BoardHelper.getFields()[62].getRectangle();

                for (int i = 0; i < pieceSet.Length; i++)
                {
                    Rectangle rect = BoardHelper.getFields()[pieceSet[i].getBoardIndex()].getRectangle();
                    if (mousePos.Intersects(rect) && hasRolledDice == true)
                    {
                        Console.WriteLine("loop 1");
                        lastBoardIndex = pieceSet[i].move(dice.getValue(), pieceSet);
                        checkPieceInterception();

                        if (dice.getValue() == 6) //one extra throw
                        {
                            currentPlayer.setThrows(1);
                        }

                        hasRolledDice = false;
                    }

                    
                    if (mousePos.Intersects(rect) && currentPlayer.getThrowsLeft() == 0 && !mousePos.Intersects(goal))
                    {
                        Console.WriteLine("loop 2");
                        currentPlayer.resetAmountOfThrows();
                        hasRolledDice = false;
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
        }

        private void checkPieceInterception()
        {
            if (lastBoardIndex == -1)
                return; // no move to check
            if (currentPlayer != yellowPlayer)
                yellowPlayer.checkAndHitBack(lastBoardIndex);
            if (currentPlayer != greenPlayer)
                greenPlayer.checkAndHitBack(lastBoardIndex);
            if (currentPlayer != bluePlayer)
                bluePlayer.checkAndHitBack(lastBoardIndex);
            if (currentPlayer != redPlayer)
                redPlayer.checkAndHitBack(lastBoardIndex);
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

        public Player getRedPlayer()
        {
            return redPlayer;
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

        public void bestAiMove()
        {
            PieceSet[] set = currentPlayer.getPieceLoactions();
            dice.roll();
            set[rand.Next(0, 4)].move(dice.getValue(), set);
            setNextPlayer();
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
