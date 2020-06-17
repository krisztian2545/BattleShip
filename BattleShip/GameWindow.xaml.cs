using Model;
using Model.Data;
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
using System.Windows.Threading;

namespace BattleShip
{
    // Palyer vs Computer

    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Game _game;
        private DispatcherTimer _timer;
        private long _secondsElapsed = 0;

        private List<Rectangle[]> _leftSideShips;
        private List<Rectangle[]> _rightSideShips;

        SolidColorBrush LimeBrush = new SolidColorBrush(Colors.Lime);
        SolidColorBrush BlackBrush = new SolidColorBrush(Colors.Black);
        SolidColorBrush BlueBrush = new SolidColorBrush(Color.FromRgb(0, 0, 50));
        SolidColorBrush RedBrush = new SolidColorBrush(Color.FromRgb(50, 0, 0));

        public GameWindow()
        {
            InitializeComponent();

            Logger.Log("Game window init...");

            ShipPlacement window = new ShipPlacement();
            window.Show();
            window.Closed += InitData;
        }

        private void InitData(object sender, EventArgs e)
        {
            // init players
            Player[] _players = new Player[2];
            _players[0] = new Bot("Enigma");
            _players[1] = ((ShipPlacement)sender).GetInitializedPlayer();
            
            // random player starts
            int index = ((new Random()).NextDouble() >= 0.5) ? 1 : 0;
            _game = new Game(_players[index], _players[1-index]);

            // adding events
            _game.OnInitGame += InitGame;
            _game.OnChange += Update;
            _game.OnGameOver += GameOver;

            // init name labels
            LeftName.Content = $"{_players[index].Name}'s fleet:";
            RightName.Content = $"{_players[1-index].Name}'s fleet:";

            TimeLabel.Content = $"Time:\n{TimeSpan.FromSeconds(_secondsElapsed++)}";

            // init side ships
            _leftSideShips = new List<Rectangle[]>();
            for(int i = 0; i < 5; i++)
            {
                Rectangle[] rectShip = new Rectangle[Player._shipLengths[i]];
                for (int j = 0; j < rectShip.Length; j++)
                {
                    rectShip[j] = NewCustomRect(j, 0, LimeBrush);
                    switch(i)
                    {
                        case 0:
                            LeftGrid1.Children.Add(rectShip[j]);
                            break;
                        case 1:
                            LeftGrid2.Children.Add(rectShip[j]);
                            break;
                        case 2:
                            LeftGrid3.Children.Add(rectShip[j]);
                            break;
                        case 3:
                            LeftGrid4.Children.Add(rectShip[j]);
                            break;
                        case 4:
                            LeftGrid5.Children.Add(rectShip[j]);
                            break;
                    }
                }

                _leftSideShips.Add(rectShip);
            }

            _rightSideShips = new List<Rectangle[]>();
            for (int i = 0; i < 5; i++)
            {
                Rectangle[] rectShip = new Rectangle[Player._shipLengths[i]];
                for (int j = 0; j < rectShip.Length; j++)
                {
                    rectShip[j] = NewCustomRect(j, 0, LimeBrush);
                    switch (i)
                    {
                        case 0:
                            RightGrid1.Children.Add(rectShip[j]);
                            break;
                        case 1:
                            RightGrid2.Children.Add(rectShip[j]);
                            break;
                        case 2:
                            RightGrid3.Children.Add(rectShip[j]);
                            break;
                        case 3:
                            RightGrid4.Children.Add(rectShip[j]);
                            break;
                        case 4:
                            RightGrid5.Children.Add(rectShip[j]);
                            break;
                    }
                }

                _rightSideShips.Add(rectShip);
            }


            this.Show();
            _game.InitGame();
        }

        private Rectangle NewCustomRect(int x, int y, SolidColorBrush brush)
        {
            Rectangle rect = new Rectangle();
            rect.Stroke = LimeBrush;
            rect.Fill = brush;

            Grid.SetColumn(rect, x);
            Grid.SetRow(rect, y);

            return rect;
        }

        private void UpdateStats()
        {
            // update round
            RoundLabel.Content = $"Round: {_game.GetRound()}";

            // update side ships
            DrawSideShips();

        }

        private void DrawSideShips()
        {
            // left player
            Ship[] ships = _game.Players[0]._myShips;
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < ships[i].Length; j++)
                {
                    _leftSideShips[i][j].Fill = ships[i].Hits[j] ? RedBrush : LimeBrush;
                }
            }

            // right player
            ships = _game.Players[1]._myShips;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < ships[i].Length; j++)
                {
                    _rightSideShips[i][j].Fill = ships[i].Hits[j] ? RedBrush : LimeBrush;
                }
            }
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            TimeLabel.Content = $"Time:\n{TimeSpan.FromSeconds(_secondsElapsed++)}";
        }

        private void InitGame(object sender, EventArgs e)
        {
            // init timer
            _secondsElapsed = 0;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += _timer_Tick;
            _timer.Start();

            UpdateStats();
        }

        private void Update(object sender, EventArgs e)
        {
            UpdateStats();
        }

        private void GameOver(object sender, EventArgs e)
        {
            _timer.Stop();
        }

    }
}
