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
using Model;
using Model.Data;

namespace BattleShip
{
    /// <summary>
    /// Interaction logic for ShipPlacement.xaml
    /// </summary>
    public partial class ShipPlacement : Window
    {

        private Rectangle[,] _grid;
        private bool _dragging;
        private Ship[] _ships;
        private Ship _draggedShip;
        private Model.Data.Vector _lastPosition;
        private Model.Data.Vector _difference;
        private bool _lastIsHorizontal;

        public ShipPlacement()
        {
            InitializeComponent();

            _dragging = false;
            _grid = new Rectangle[10, 10];
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    _grid[j, i] = new Rectangle();
                    _grid[j, i].Stroke = new SolidColorBrush(Colors.Lime);
                    _grid[j, i].Fill = new SolidColorBrush(Colors.Black);
                    //_grid[j, i].StrokeThickness = 1;
                    _grid[j, i].MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
                    _grid[j, i].MouseLeftButtonUp += Rectangle_MouseLeftButtonUp;
                    _grid[j, i].MouseMove += Rectangle_MouseMove;
                    Grid.SetRow(_grid[j, i], i);
                    Grid.SetColumn(_grid[j, i], j);
                    Gridd.Children.Add(_grid[j, i]);
                }
            Gridd.MouseLeave += Grid_MouseLeave;

            // initialize ships
            _ships = Bot.GenerateShips();

            DrawShips();
        }

        private void DrawShips()
        {
            SolidColorBrush LimeBrush = new SolidColorBrush(Colors.Lime);
            SolidColorBrush BlackBrush = new SolidColorBrush(Colors.Black);

            // clear grid
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    _grid[j, i].Fill = BlackBrush;

            // paint ships
            foreach (Ship ship in _ships)
                foreach (Model.Data.Vector v in ship.Coordinates)
                    _grid[v.X, v.Y].Fill = LimeBrush;
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.Focus(Gridd);
            Logger.Log("dragging true...");
            _dragging = true;
            var pos = new Model.Data.Vector(Grid.GetColumn((Rectangle)sender), Grid.GetRow((Rectangle)sender));

            foreach (Ship ship in _ships)
                foreach (Model.Data.Vector v in ship.Coordinates)
                    if(v == pos)
                    {
                        _draggedShip = ship;
                        _lastPosition = pos;
                        _difference = ship.Coordinates[0] - pos;
                        _lastIsHorizontal = ship.IsHorizontal;
                        return;
                    }

            Logger.Log("Ship not found!!!");
            _draggedShip = null;

            
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Logger.Log("dragging false...");
            _dragging = false;
            //_draggedShip = null;
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if(_dragging && (_draggedShip != null))
            {
                
                Logger.Log($"mouse x: {Grid.GetColumn((Rectangle)sender)}");
                Logger.Log($"mouse y: {Grid.GetRow((Rectangle)sender)}");

                MoveShipIfPossible(new Model.Data.Vector(Grid.GetColumn((Rectangle)sender), Grid.GetRow((Rectangle)sender)), false);

                DrawShips();
            }
        }

        private void MoveShipIfPossible(Model.Data.Vector pos, bool rotate)
        {
            if(rotate)
            {
                _draggedShip.IsHorizontal = !_lastIsHorizontal;
                _draggedShip.Replace(pos - _difference + _difference.Inverted());
                _difference = _difference.Inverted();
            } else
            {
                _draggedShip.Replace(pos + _difference);
            }

            if (CollisionDetection(_draggedShip) || (_draggedShip.Coordinates[0].X < 0) || (_draggedShip.Coordinates[0].Y < 0) || 
                (_draggedShip.Coordinates[_draggedShip.Length -1].X > 9) || (_draggedShip.Coordinates[_draggedShip.Length - 1].Y > 9))
            {
                Logger.Log("Collision detected...");

                _draggedShip.IsHorizontal = _lastIsHorizontal;
                _draggedShip.Replace(_lastPosition);
                if(rotate)
                    _difference = _difference.Inverted();
            }

            _lastIsHorizontal = _draggedShip.IsHorizontal;
            _lastPosition = _draggedShip.Coordinates[0];
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Logger.Log("dragging false...");
            _dragging = false;
            //_draggedShip = null;
        }

        private bool CollisionDetection(Ship ship)
        {
            foreach (Ship s in _ships)
                if ((s != ship) && (Ship.CollisionDetection(s, ship)) /*and inside the grid*/)
                    return true;

            return false;
        }

        private void Gridd_KeyDown(object sender, KeyEventArgs e)
        {
            Logger.Log($"Pressed key: {e.Key}");

            if((e.Key == Key.R) && (_draggedShip != null))
            {
                MoveShipIfPossible(_lastPosition, true);
                DrawShips();
            }
        }

        private void Ready_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text != "")
                this.Close();
            else
                TopLabel.Content = "Choose a name!";
        }

        public Player GetInitializedPlayer()
        {
            return new Player(NameBox.Text, _ships);
        }

    }
}
