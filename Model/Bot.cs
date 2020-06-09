using System;
using System.Collections.Generic;
using System.Text;
using Model.Data;

namespace Model
{
    public class Bot : Player
    {

        private static readonly Random _random = new Random();
        private bool _targetProbablyHorizontal; // reset to true on destroyed ship;
        private List<Vector> _unresolvedShots;

        public Bot(string name) : base($"Bot {name}", GenerateShips())
        {
            _targetProbablyHorizontal = true;
            _unresolvedShots = new List<Vector>();
        }

        public static Ship[] GenerateShips()
        {
            Ship[] newShips = new Ship[_numberOfShips];

            int i = 0;
            bool again;
            do
            {
                again = false;

                newShips[i] = new Ship(_shipLengths[i]);
                newShips[i].IsHorizontal = _random.NextDouble() >= 0.5;
                newShips[i].Replace(new Vector(_random.Next(10 - newShips[i].Length), _random.Next(10 - newShips[i].Length)));

                for (int j = 0; j < i; j++)
                {
                    if(Ship.CollisionDetection(newShips[i], newShips[j]))
                    {
                        again = true;
                        break;
                    }
                        
                }
                if (!again)
                    i++;

            } while (i < 5);

            return newShips;
        }

        public void AutoAim()
        {
            Logger.Log("Auto aiming...");
            Vector target = NO_TARGET;

            if(_unresolvedShots.Count == 0)
            {
                //random shot
                Logger.Log("Aiming at random location.");
                target = new Vector(_random.Next(10), _random.Next(10));
            } else
            {
                if (_targetProbablyHorizontal)
                {
                    // check right and left
                }
                else
                {
                    // check up and down
                }
            }

            AimAt(target);
        }

        public override void Shoot(Player enemy)
        {
            if (TargetCoordinates == NO_TARGET)
            {
                AutoAim();
            }
            
            base.Shoot(enemy);
        }

    }
}
