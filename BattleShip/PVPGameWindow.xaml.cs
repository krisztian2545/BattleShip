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
    /// <summary>
    /// Interaction logic for PVPGameWindow.xaml
    /// </summary>
    public partial class PVPGameWindow : Window
    {

        private Game _game;
        private Player _humanPlayer;
        private Player[] _players;
        private DispatcherTimer _timer;
        private long _secondsElapsed = 0;


        private bool _canLeftPlayerInteract;
        private bool _canRightPlayerInteract;

        private List<Rectangle[]> _leftSideShips;
        private List<Rectangle[]> _rightSideShips;

        private Rectangle[,] _leftMainGrid;
        private Rectangle[,] _rightMainGrid;

        SolidColorBrush LimeBrush = new SolidColorBrush(Colors.Lime); // ship
        SolidColorBrush BlackBrush = new SolidColorBrush(Colors.Black); // blank space
        SolidColorBrush BlueBrush = new SolidColorBrush(Color.FromRgb(0, 0, 100)); // missed shot
        SolidColorBrush RedBrush = new SolidColorBrush(Color.FromRgb(100, 0, 0)); // hit

        public PVPGameWindow()
        {
            InitializeComponent();

            Logger.Log("Game window init...");
            _players = new Player[2];
            _canLeftPlayerInteract = false;
            _canRightPlayerInteract = false;

            ShipPlacement window = new ShipPlacement();
            window.Show();
            window.Closed += InitFirstPlayer;
        }

        private void InitFirstPlayer(object sender, EventArgs e)
        {
            _players[0] = ((ShipPlacement)sender).GetInitializedPlayer();

            ShipPlacement window = new ShipPlacement();
            window.Show();
            window.Closed += InitSecondPlayer;
        }

        private void InitSecondPlayer(object sender, EventArgs e)
        {
            _players[1] = ((ShipPlacement)sender).GetInitializedPlayer();

            InitData();
        }

        private void InitData()
        {
            // random player starts
            int index = ((new Random()).NextDouble() >= 0.5) ? 1 : 0;
            _game = new Game(_players[index], _players[1 - index]);

            // adding events
            _game.OnInitGame += InitGame;
            _game.OnChange += Update;
            _game.OnGameOver += GameOver;

            // init name labels
            LeftName.Content = $"{_players[index].Name}'s fleet:";
            RightName.Content = $"{_players[1 - index].Name}'s fleet:";

            TimeLabel.Content = $"Time:\n{TimeSpan.FromSeconds(_secondsElapsed++)}";

            // init side ships
            _leftSideShips = new List<Rectangle[]>();
            for (int i = 0; i < 5; i++)
            {
                Rectangle[] rectShip = new Rectangle[Player._shipLengths[i]];
                for (int j = 0; j < rectShip.Length; j++)
                {
                    rectShip[j] = NewCustomRect(j, 0, LimeBrush);
                    switch (i)
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

            // init main grids
            _leftMainGrid = new Rectangle[10, 10];
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    Rectangle rect = NewCustomRect(j, i, BlackBrush);
                    rect.MouseLeftButtonDown += LeftMainGridRect_MouseLeftButtonDown;

                    _leftMainGrid[j, i] = rect;
                    LeftMainGrid.Children.Add(rect);
                }

            _rightMainGrid = new Rectangle[10, 10];
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    Rectangle rect = NewCustomRect(j, i, BlackBrush);
                    rect.MouseLeftButtonDown += RightMainGridRect_MouseLeftButtonDown;

                    _rightMainGrid[j, i] = rect;
                    RightMainGrid.Children.Add(rect);
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

            // update informational labels
            if (_game.Players[0].Name == _game.GetCurrentPlayer().Name)
            {
                // left player's turn
                LeftInformativeLabel.Content = "Your turn!";
                RightInformative_Label.Content = "";
            }
            else
            {
                // right player's turn
                LeftInformativeLabel.Content = "";
                RightInformative_Label.Content = "Your turn!";
            }
        }

        private void DrawSideShips()
        {
            // left player
            Ship[] ships = _game.Players[0]._myShips;
            for (int i = 0; i < 5; i++)
            {
                if (ships[i].IsDestroyed())
                {
                    for (int j = 0; j < ships[i].Length; j++)
                    {
                        _leftSideShips[i][j].Fill = RedBrush;
                    }
                }
            }

            // right player
            ships = _game.Players[1]._myShips;
            for (int i = 0; i < 5; i++)
            {
                if (ships[i].IsDestroyed())
                {
                    for (int j = 0; j < ships[i].Length; j++)
                    {
                        _rightSideShips[i][j].Fill = RedBrush;
                    }
                }
            }
        }

        private void DrawMainGrids()
        {
            // left player
            var grid = _game.Players[0].GetTerritory();
            for (int y = 0; y < 10; y++)
                for (int x = 0; x < 10; x++)
                    switch (grid[x, y])
                    {
                        case 0:
                            _leftMainGrid[x, y].Fill = BlackBrush;
                            break;
                        case 1:
                            _leftMainGrid[x, y].Fill = BlueBrush;
                            break;
                        case 3:
                            _leftMainGrid[x, y].Fill = RedBrush;
                            break;
                    }

            // right player
            grid = _game.Players[1].GetTerritory();
            for (int y = 0; y < 10; y++)
                for (int x = 0; x < 10; x++)
                    switch (grid[x, y])
                    {
                        case 0:
                            _rightMainGrid[x, y].Fill = BlackBrush;
                            break;
                        case 1:
                            _rightMainGrid[x, y].Fill = BlueBrush;
                            break;
                        case 3:
                            _rightMainGrid[x, y].Fill = RedBrush;
                            break;
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

            //UpdateStats();
            //DrawMainGrids();
            Update(null, new GameOverEventArgs(false));
        }

        private void Update(object sender, GameOverEventArgs e)
        {
            // update ui
            UpdateStats();
            DrawMainGrids();

            // game loop
            if (!e.IsGameOver)
            {
                if (_game.GetCurrentPlayer().Name == _game.Players[0].Name)
                {
                    // enable interaction
                    _canLeftPlayerInteract = true;
                }
                else
                {
                    // the bot's turn
                    _canRightPlayerInteract = true;
                }
            }

        }

        private void GameOver(object sender, EventArgs e)
        {
            _timer.Stop();

            string winnerMessage = $"The winner is: {_game.Winner.Name}";
            LeftInformativeLabel.Content = winnerMessage;
            RightInformative_Label.Content = winnerMessage;

            // show high scores window
            var window = new HighScores(new Summary(_game.Winner.Name, _game.GetCurrentPlayer().Name));
            window.Show();
            window.Closed += CloseWindow;
            // if the highscores window is closed, go back to main menu
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LeftMainGridRect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Logger.Log($"you clicked {((Grid)((Rectangle)sender).Parent).Name}");
            var pos = new Model.Data.Vector(Grid.GetColumn((Rectangle)sender), Grid.GetRow((Rectangle)sender));

            if (_canRightPlayerInteract && (_game.GetTheOtherPlayer().GetTerritory()[pos.X, pos.Y] == 0))
            {
                _canRightPlayerInteract = false;
                _game.GetCurrentPlayer().AimAt(pos);
                _game.Next();
            }
        }

        private void RightMainGridRect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Logger.Log($"you clicked {((Grid)((Rectangle)sender).Parent).Name}");
            var pos = new Model.Data.Vector(Grid.GetColumn((Rectangle)sender), Grid.GetRow((Rectangle)sender));

            if (_canLeftPlayerInteract && (_game.GetTheOtherPlayer().GetTerritory()[pos.X, pos.Y] == 0))
            {
                _canLeftPlayerInteract = false;
                _game.GetCurrentPlayer().AimAt(pos);
                _game.Next();
            }
        }

    }
}
