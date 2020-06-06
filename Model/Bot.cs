using System;
using System.Collections.Generic;
using System.Text;
using Model.Data;

namespace Model
{
    public class Bot : Player
    {

        public Bot(string name) : base($"Bot {name}", GenerateShips())
        {

        }

        public static Ship[] GenerateShips()
        {
            // randomization
            return Ship.CreateCrew(2, 3, 3, 4, 5);
        }

        public void AutoAim()
        {
            Logger.Log("Auto aiming...");
            AimAt(new Vector(7, 7));
        }

        public override void Shoot(Player enemy)
        {
            AutoAim();
            base.Shoot(enemy);
        }

    }
}
