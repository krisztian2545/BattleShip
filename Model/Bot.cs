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
            return new int[10, 10];
        }

    }
}
