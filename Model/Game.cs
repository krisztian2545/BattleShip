﻿using System;

namespace Model
{
    public class Game
    {

        private int _halfRound;
        Player[] Players { get; }

        public Game(Player player1, Player player2)
        {
            Players = new Player[] { player1, player2 };
        }

        public void InitGame()
        {
            _halfRound = 0;
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