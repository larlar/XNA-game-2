﻿using Microsoft.Xna.Framework.Graphics;

namespace Ludo
{
    class PieceSet
    {
        private int[] path;
        private int numPieces=1;
        private int pathPosition;
        private int unique;
        private int goalPosition = 62;

        public PieceSet(int[] path, int pathPosition)
        {
            this.path = path;
            this.pathPosition = pathPosition;
            this.unique = pathPosition;
        }

        public int getNumPieces()
        {
            return numPieces;
        }

        public void merge(PieceSet pieceSet)
        {
            this.numPieces += pieceSet.getNumPieces();
            pieceSet.setEmpty(); 
        }

        public int getPosition()
        {
            return pathPosition;
        }
        public void setPosition(int position)
        {
            this.pathPosition=position;
        }

        public void move(int delta, PieceSet[] set)
        {
            int newPos;
            if (pathPosition == goalPosition)
                return;
            else if (pathPosition < 4)
            {
                if (delta == 6)
                    newPos = 4;
                else
                    return;
            }
            else if (pathPosition + delta > goalPosition)
                newPos = 2 * goalPosition - pathPosition - delta;
            else
                newPos = pathPosition + delta;
           
            // check if merge pieces
            bool merged=false;
            for (int i = 0; i < set.Length; i++)
            {
                PieceSet s = set[i];
                if (!(s.getNumPieces() == 0 || s.getUniqueValue() == unique))
                {  // no empty and not itself
                    if (newPos == s.getPosition())
                    {
                        s.merge(this);
                        merged = true;
                        i = 4;
                    }
                }
            }
            if (!merged)
                pathPosition = newPos;
        }
 
        public void makeSingle()
        {
            numPieces = 1;
        }
        public void setEmpty()
        {
            this.numPieces = 0;
        }
 
        public int getBoardIndex()
        {
            return path[pathPosition];
        }
        public int getUniqueValue()
        {
            return unique;
        }
    }
}
