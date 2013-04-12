﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Ludo
{
    class Player
    {
        
        PieceSet[] pieces;
        Color color;
        Type type;
        int numberOfThrows;
        IAi brain;
        Dice dice;

        public enum Color
        {
            Yellow, Green, Blue, Red
        }
        public enum Type
        {
            human, ai
        }

        public Player(Color color, IAi brain)
        {
            init(color);
            type = Player.Type.ai;
            this.brain = brain;

        }
        public Player(Color color)
        {
            init(color);
            type = Player.Type.human;
        }

        private void init(Color color)
        {
            int[] path = new int[63];
            this.color = color;
            dice = new Dice();
            numberOfThrows = 3;
            switch (color)
            {
                case Color.Yellow:
                    path = BoardHelper.getYellowPath();
                    break;
                case Color.Green:
                    path = BoardHelper.getGreenPath();
                    break;
                case Color.Blue:
                    path = BoardHelper.getBluePath();
                    break;
                case Color.Red:
                    path = BoardHelper.getRedPath();
                    break;
            }
            pieces = new PieceSet[4];
            pieces[0] = new PieceSet(path, 0);
            pieces[1] = new PieceSet(path, 1);
            pieces[2] = new PieceSet(path, 2);
            pieces[3] = new PieceSet(path, 3);
        }

        public void checkAndHitBack(int boardIndex)
        {
            foreach (PieceSet s in pieces)
            {
                if (s.getBoardIndex() == boardIndex)
                {
                    s.hitBackToStart(pieces);
                    return;
                }
            }
        }

        public PieceSet[] getPieceLoactions()
        {
            return pieces;
        }

        public bool isAllPiecesInBaseOrGoal() 
        {
            if ((pieces[0].getPosition() < 4 || pieces[0].getPosition() == 62) && 
               (pieces[1].getPosition() < 4 || pieces[1].getPosition() == 62) && 
               (pieces[2].getPosition() < 4 || pieces[2].getPosition() == 62) && 
               (pieces[3].getPosition() < 4 || pieces[3].getPosition() == 62))
                return true;
            else
                return false;
        }

        public bool hasPlayerWon() 
        {
            int sum = 0;
            for(int i = 0; i < 4; i++){
                if (pieces[i].getPosition() == 62)
                    sum += pieces[i].getNumPieces();
            }
            if (sum == 4)
                return true;
            else
                return false;
        }

        public int getThrowsLeft()
        {
            return numberOfThrows;
        }

        public void minusThrows()
        {
            numberOfThrows -= 1;
        }

        public void setThrows(int turn)
        {
            numberOfThrows = turn;
        }

        public void resetAmountOfThrows()
        {
            if (isAllPiecesInBaseOrGoal())
                numberOfThrows = 3;

            else
                numberOfThrows = 1;
        }

        public Color getColor()
        {
            return color;
        }

        public int getDiceValue()
        {
            return dice.getValue();
        }

        public void rollDice()
        {
            dice.roll();
        }

        public int move(int pieceSetIndex)
        {
            int index = pieces[pieceSetIndex].move(getDiceValue(), pieces);
                resetAmountOfThrows();
            return index;
        }

        public bool isAI()
        {
            return type == Type.ai;
        }

        public void moveAiPiece()
        {
            rollDice();
            int pieceIndex = brain.getMove(getDiceValue(), pieces);
            pieces[pieceIndex].move(getDiceValue(), pieces);
        }

        private class Dice
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
 
}
