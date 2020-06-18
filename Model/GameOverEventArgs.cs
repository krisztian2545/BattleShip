using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class GameOverEventArgs : EventArgs
    {
        public bool IsGameOver { get; }

        public GameOverEventArgs(bool isGameOver)
        {
            IsGameOver = isGameOver;
        }
    }
}
