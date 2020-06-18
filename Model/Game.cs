using Model.Data;
using System;

namespace Model
{
    public class Game
    {

        private int _turn;
        public Player[] Players { get; private set; }
        public Player Winner { get; private set; }

        public event EventHandler OnInitGame;
        public event EventHandler<GameOverEventArgs> OnChange;
        public event EventHandler OnGameOver;

        public Game(Player player1, Player player2)
        {
            Players = new Player[] { player1, player2 };
        }

        public void InitGame()
        {
            _turn = 0;
            Winner = null;

            OnInitGame?.Invoke(this, EventArgs.Empty);
        }

        public void Next()
        {

            Logger.Log($"Start of {GetCurrentPlayer().Name}'s turn.");
            GetCurrentPlayer().Shoot( GetTheOtherPlayer() );

            Logger.Log($"End of {GetCurrentPlayer().Name}'s turn.");
            _turn++;

            bool isGameOver = CheckGameOver();
            OnChange?.Invoke(this, new GameOverEventArgs(isGameOver));
            if (isGameOver)
                GameOver();
        }

        public bool CheckGameOver()
        {
            Logger.Log("Checking if the game is over...");
            foreach (Ship ship in GetCurrentPlayer()._myShips)
                if (!ship.IsDestroyed())
                    return false;

            Winner = GetTheOtherPlayer();
            //GameOver();

            return true;
        }

        public void GameOver()
        {
            Logger.Log("-------------------------------------- GAME OVER! --------------------------------------");
            Logger.Log($"The winner is {Winner.Name}");
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }

        public Player GetCurrentPlayer()
        {
            int i = _turn % 2;
            Logger.Log($"The current player is {Players[i].Name}");
            return Players[i];
        }

        public Player GetTheOtherPlayer()
        {
            int i = 1 - (_turn % 2);
            Logger.Log($"The other player is {Players[i].Name}");
            return Players[i];
        }

        public int GetRound()
        {
            return (_turn / 2) + 1;
        }

    }
}
