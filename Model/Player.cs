using System;
using System.Collections.Generic;
using System.Text;
using Model.Data;

namespace Model
{
    public class Player
    {

        public string Name { get; }

        /**
         * 0 - blank 
         * 1 - missed shot
         * 2 - ship
         * 3 - hit
         */
        private int[,] _myTerritory;
        //public int[,] _enenmyTerritory { get; private set; }
        public Vector TargetCoordinates { get; private set; }

        public const int _numberOfShips = 5; // const is also static
        public static readonly int[] _shipLengths = new int[] { 2, 3, 3, 4, 5 };
        public static readonly Vector NO_TARGET = new Vector(-1, -1);

        public Player(string name, Ship[] newShips)
        {
            Name = name;
            //_enenmyTerritory = new int[10, 10];
            _myTerritory = new int[10, 10];
            PlaceMyShips(newShips);
            TargetCoordinates = NO_TARGET;

        }

        void PlaceMyShips(Ship[] ships)
        {
            foreach(Ship ship in ships)
            {
                foreach(Vector v in ship.Coordinates)
                {
                    _myTerritory[v.X, v.Y] = 2;
                }
            }
        }

        public virtual void Shoot(Player enemy)
        {
            Logger.Log($"Shooting {enemy.Name}'s ship at {TargetCoordinates.ToString()}");
            
            // check if it hit the enemy
            if(enemy.IsHit(TargetCoordinates))
            {
                Logger.Log("Hit!");
                //_enenmyTerritory[TargetCoordinates.X, TargetCoordinates.Y] = 3;
            } else
            {
                Logger.Log("Missed.");
                //_enenmyTerritory[TargetCoordinates.X, TargetCoordinates.Y] = 1;
            }

            TargetCoordinates = NO_TARGET;
        }

        public void AimAt(Vector target)
        {
            // do a check here or before this to dont shoot twice at a target
            TargetCoordinates = target;
            Logger.Log(Name + "is aiming at " + TargetCoordinates.ToString());
        }

        public bool IsHit(Vector coordinate)
        {
            bool hit = _myTerritory[coordinate.X, coordinate.Y] == 2;
            _myTerritory[coordinate.X, coordinate.Y] = hit ? 3 : 1;
            return hit;
        }

        public int[,] GetTerritory(bool showHidden = false)
        {
            int[,] output = (int[,])_myTerritory.Clone();
            if(!showHidden)
            {
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 10; j++)
                        if (output[i, j] == 2)
                            output[i, j] = 0;
            }

            return output;
        }

    }
}
