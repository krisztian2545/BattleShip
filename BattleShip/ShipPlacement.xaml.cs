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

namespace BattleShip
{
    /// <summary>
    /// Interaction logic for ShipPlacement.xaml
    /// </summary>
    public partial class ShipPlacement : Window
    {

        private Rectangle[,] _grid;
        private bool dragging;

        public ShipPlacement()
        {
            InitializeComponent();

            dragging = false;
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
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Logger.Log("dragging true...");
            dragging = true;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Logger.Log("dragging false...");
            dragging = false;
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if(dragging)
            {
                Logger.Log($"mouse x: {Grid.GetColumn((Rectangle)sender)}");
                Logger.Log($"mouse y: {Grid.GetRow((Rectangle)sender)}");
            }
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Logger.Log("dragging false...");
            dragging = false;
        }

    }
}
