using System;
using System.Collections.Generic;
using System.Text;
using Model.Data;

namespace Model
{
    class Player
    {

        public string Name { get; }
        public Ship[] Ships { get; }
        private int[,] _myTerritory;
        private int[,] _enenmyTerritory;
        private Vector LastShot;

        //private const int NUMBER_OF_SHIPS = 5;

        public Player(string name, Ship[] newShips)
        {
            Name = name;
            Ships = newShips;
            _enenmyTerritory = new int[10, 10];
            PlaceMyShips();

        }

        void PlaceMyShips()
        {

        }



    }
}
