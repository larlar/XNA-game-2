using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ludo
{
    interface IAi
    {
        int getMove(int diceValue, PieceSet[] set); 
    }
}
