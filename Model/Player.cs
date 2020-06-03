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
        public int[,] _enenmyTerritory { get; private set; }
        private Vector TargetCoordinates;

        public const int NUMBER_OF_SHIPS = 5; // this is also static

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

        public void Shoot(Player enemy)
        {

        }

        public void AimAt(Vector target)
        {

        }

    }
}
