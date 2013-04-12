using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ludo
{
    class MoronPlayer : Player, IAi
    {
        public MoronPlayer(Color color) : base(color) { }

        public int getAiMove(GameModel model)
        {
            rollDice();
            Random rand = new Random();      

            if (getDiceValue() == 6 && pieces[0].getPosition() == 0)
            {
                return pieces[0].move(getDiceValue(), pieces);
            }
            else if (getDiceValue() == 6 && pieces[0].getPosition() != 0 && pieces[1].getPosition() == 1)
            {
                return pieces[1].move(getDiceValue(), pieces);
            }
            else if (getDiceValue() == 6 && pieces[0].getPosition() != 0 && pieces[1].getPosition() != 1 && pieces[2].getPosition() == 2)
            {
                return pieces[2].move(getDiceValue(), pieces);
            }
            else if (getDiceValue() == 6 && pieces[0].getPosition() != 0 && pieces[1].getPosition() != 1 && pieces[2].getPosition() != 2 && pieces[3].getPosition() == 3)
            {
                return pieces[3].move(getDiceValue(), pieces);
            }
            else
            {
                int roll = rand.Next(0,4);
                return pieces[roll].move(getDiceValue(), pieces);
            }
        }
    }
}