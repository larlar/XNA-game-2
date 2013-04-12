using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ludo
{
    class MoronPlayer : Player, IAi
    {
        public MoronPlayer(Color color) :base(color){}

        public int getAiMove(GameModel model)
        {
			rollDice();
			
			Random rand = new Random();
			int pieceIndex = rand.Next(0, 4);

            return pieces[pieceIndex].move(getDiceValue(), pieces);
        
        }
    }
}
