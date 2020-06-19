using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip
{
    public class Summary
    {
        public string Winner { get; set; }
        public string Loser { get; set; }

        public Summary(string winner, string loser)
        {
            Winner = winner;
            Loser = loser;
        }
    }
}
