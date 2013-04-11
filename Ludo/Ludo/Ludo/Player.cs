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
        int numberOfThrows = 3;

        public enum Color
        {
            Yellow, Green, Blue, Red
        }
        public enum Type
        {
            human, ai
        }

        public Player(Color color, Type type)
        {
            int[] path = new int[63]; 
            this.color = color;
            this.type = type;
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

        public PieceSet[] getPieceLoactions()
        {
            return pieces;
        }

        public int getPiecePath(int pos)
        {
            return pieces[pos].getPosition();
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



        public bool isAI()
        {
            return type == Type.ai;
        }

        public Color getColor()
        {
            return color;
        }
    }
}
