using System;
using System.Collections.Generic;
using System.Text;
using Model.Data;

namespace Model
{
    public class Bot : Player
    {

        private static readonly Random _random = new Random();
        private bool _targetProbablyHorizontal;
        private bool _firstHit;
        private bool _targetShipDestroyed = false;
        private List<Vector> _unresolvedShots;

        public Bot(string name) : base($"Bot {name}", GenerateShips())
        {
            _targetProbablyHorizontal = true;
            _firstHit = true;
            _targetShipDestroyed = false;
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

                if(newShips[i].IsHorizontal)
                    newShips[i].Replace(new Vector(_random.Next(10 - newShips[i].Length), _random.Next(10)));
                else
                    newShips[i].Replace(new Vector(_random.Next(10), _random.Next(10 - newShips[i].Length)));

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

        public void AutoAim(int[,] territory)
        {
            Logger.Log("Auto aiming...");
            Logger.Log($"Number of unresolved shots: {_unresolvedShots.Count}");
            Logger.Log($"firstHit = {_firstHit}");

            if (_unresolvedShots.Count == 0)
            {
                //random shot
                Logger.Log("Aiming at random location.");
                Vector pos;
                do
                {
                    pos = new Vector(_random.Next(10), _random.Next(10));
                } while (territory[pos.X, pos.Y] > 0);

                AimAt(pos);
                return;
            } else
            {
                Vector lastHit = _unresolvedShots[_unresolvedShots.Count - 1];

                if (_targetProbablyHorizontal)
                {
                    // check right and left
                    if(lastHit.X < 9)
                    {
                        if (territory[lastHit.X + 1, lastHit.Y] == 0)
                        {
                            AimAt(new Vector(lastHit.X + 1, lastHit.Y));
                            //_firstHit = false;
                            return;
                        }
                    }

                    if (lastHit.X > 0)
                    {
                        if (territory[lastHit.X - 1, lastHit.Y] == 0)
                        {
                            AimAt(new Vector(lastHit.X - 1, lastHit.Y));
                            //_firstHit = false;
                            return;
                        }
                    }

                }
                else
                {
                    // check up and down
                    if (lastHit.Y < 9)
                    {
                        if (territory[lastHit.X, lastHit.Y + 1] == 0)
                        {
                            AimAt(new Vector(lastHit.X, lastHit.Y + 1));
                            //_firstHit = false;
                            return;
                        }
                    }

                    if (lastHit.Y > 0)
                    {
                        if (territory[lastHit.X, lastHit.Y - 1] == 0)
                        {
                            AimAt(new Vector(lastHit.X, lastHit.Y - 1));
                            //_firstHit = false;
                            return;
                        }
                    }

                }
                
                if (_firstHit)
                {
                    Logger.Log("Changing targetProbablyHorizontal...");
                    _targetProbablyHorizontal = !_targetProbablyHorizontal;
                    _firstHit = false;
                }
                else
                {
                    Logger.Log("Removing last hit from unresolved shots.");
                    _unresolvedShots.Remove(lastHit);
                }

            }

            AutoAim(territory);
        }



        public override void Shoot(Player enemy)
        {
            if (TargetCoordinates == NO_TARGET)
            {
                AutoAim(enemy.GetTerritory());
            }

            Vector temp = TargetCoordinates;
            base.Shoot(enemy);

            if(_targetShipDestroyed)
            {
                Logger.Log($"targetShipDestroyed = {_targetShipDestroyed}");
                _firstHit = true;
                _targetShipDestroyed = false;
                return;
            }

            if (enemy.GetTerritory()[temp.X, temp.Y] == 3)
            {
                _unresolvedShots.Add(temp);
                if (_unresolvedShots.Count > 1)
                {
                    _firstHit = false;
                    Logger.Log($"firstHit = {_firstHit}; because second hit is made.");
                }
            }
        }

        public override void OnEnemyShipDestroyed()
        {
            base.OnEnemyShipDestroyed();
            _unresolvedShots.Clear();
            _targetShipDestroyed = true;
        }

    }
}
