using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ludo
{
    class MoronAI : IAi
    {
        public int getMove(int diceValue, PieceSet[] set)
        {
            Random rand = new Random();
            return rand.Next(0, 4);
        
        }
    }
}
