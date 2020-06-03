using System;

namespace Model
{
    class Game
    {

        private int _halfRound;
        Player[] Players { get; }

        public Game()
        {

        }

        public void Next()
        {

            int index = _halfRound % 2;
            Logger.Log($"Start of {Players[index].Name}'s turn.");
            Players[index].Shoot( Players[1 - index] );

            Logger.Log($"End of {Players[index].Name}'s turn.");
            _halfRound++;
        }

        public Player GetCurrentPlayer()
        {
            int i = _halfRound % 2;
            Logger.Log($"The current player is {Players[i].Name}");
            return Players[i];
        }

    }
}
