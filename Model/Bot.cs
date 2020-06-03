using System;
using System.Collections.Generic;
using System.Text;
using Model.Data;

namespace Model
{
    class Bot : Player
    {

        public Bot(string name) : base(name, GenerateShips())
        {

        }

        public static Ship[] GenerateShips()
        {
            // randomization
            return new Ship[ NUMBER_OF_SHIPS ];
        }

    }
}
