using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BattleShip
{
    // Palyer vs Computer

    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Game _game;
        private Player[] _players;

        public GameWindow()
        {
            InitializeComponent();

            Logger.Log("Game window init...");
            _players = new Player[2];

            ShipPlacement window = new ShipPlacement();
            window.Show();
            window.Closed += GetInitData;
        }

        public void GetInitData(object sender, EventArgs e)
        {
            Logger.Log($"get init data from {sender.GetType()}");
            _players[0] = new Bot("Bot");
            _players[1] = ((ShipPlacement)sender).GetInitializedPlayer();
            
            int i = ((new Random()).NextDouble() >= 0.5) ? 1 : 0;
            _game = new Game(_players[i], _players[1-i]);

            _game.OnInitGame += InitGame;
            _game.OnChange += Update;
            _game.OnGameOver += GameOver;

            this.Show();
            _game.InitGame();
        }

        public void InitGame(object sender, EventArgs e)
        {

        }

        public void Update(object sender, EventArgs e)
        {

        }

        public void GameOver(object sender, EventArgs e)
        {

        }

    }
}
