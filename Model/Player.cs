using System;
using System.Collections.Generic;
using System.Text;
using Model.Data;

namespace Model
{
    public class Player
    {

        public string Name { get; }
        public Ship[] Ships { get; }
        private int[,] _myTerritory;
        public int[,] _enenmyTerritory { get; private set; }
        private Vector TargetCoordinates;

        public const int NUMBER_OF_SHIPS = 5; // const is also static
        public static readonly Vector NO_TARGET = new Vector(-1, -1);

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

        public virtual void Shoot(Player enemy)
        {
            // check if it hit the enemy

            // update upon feedback from the IsHit() function
        }

        public void AimAt(Vector target)
        {
            // do a check here or before this to dont shoot twice at a target
            TargetCoordinates = target;
            Logger.Log("Aiming at " + TargetCoordinates.ToString());
        }

        public bool IsHit(Vector coordinate)
        {

            return true;
        }

    }
}
