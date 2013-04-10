using Microsoft.Xna.Framework.Graphics;

namespace Ludo
{
    class PieceSet
    {
        private int[] path;
        private int numPieces=1;
        private int pathPosition;

        public PieceSet(int[] path,int pathPosition)
        {
            this.path = path;
            this.pathPosition = pathPosition;
        }

        public int getPieceCount()
        {
            return numPieces;
        }

        public void merge(PieceSet pieceSet)
        {
            this.numPieces += pieceSet.getPieceCount();
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

        public void move(int delta)
        {
            if (pathPosition < 4)
                pathPosition = 4;
            else if(pathPosition <62)
                this.pathPosition += delta;
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
    }
}
