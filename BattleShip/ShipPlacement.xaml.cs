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
            SolidColorBrush ColorLime = new SolidColorBrush(Colors.Lime);
            SolidColorBrush ColorBlack = new SolidColorBrush(Colors.Black);

            // clear grid
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    _grid[j, i].Fill = ColorBlack;

            // paint ships
            foreach (Ship ship in _ships)
                foreach (Model.Data.Vector v in ship.Coordinates)
                    _grid[v.X, v.Y].Fill = ColorLime;
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Logger.Log("dragging true...");
            _dragging = true;
            var pos = new Model.Data.Vector(Grid.GetColumn((Rectangle)sender), Grid.GetRow((Rectangle)sender));

            foreach (Ship ship in _ships)
                foreach (Model.Data.Vector v in ship.Coordinates)
                    if(v == pos)
                    {
                        _draggedShip = ship;
                        _lastPosition = pos;
                        return;
                    }

            Logger.Log("Ship not found!!!");
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Logger.Log("dragging false...");
            _dragging = false;
            _draggedShip = null;
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if(_dragging && (_draggedShip != null))
            {
                
                Logger.Log($"mouse x: {Grid.GetColumn((Rectangle)sender)}");
                Logger.Log($"mouse y: {Grid.GetRow((Rectangle)sender)}");
                _draggedShip.Replace(new Model.Data.Vector(Grid.GetColumn((Rectangle)sender), Grid.GetRow((Rectangle)sender)));

                if (CollisionDetection(_draggedShip))
                {
                    Logger.Log("Collision detected...");
                    _draggedShip.Replace(_lastPosition);
                }

                _lastPosition = _draggedShip.Coordinates[0];
                DrawShips();
            }
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Logger.Log("dragging false...");
            _dragging = false;
            _draggedShip = null;
        }

        private bool CollisionDetection(Ship ship)
        {
            foreach (Ship s in _ships)
                if ((s != ship) && (Ship.CollisionDetection(s, ship)))
                    return true;

            return false;
        }

    }
}
