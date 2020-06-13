using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BattleShip
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Computer_Click(object sender, RoutedEventArgs e)
        {
            // start a player vs computer game
            ShipPlacement window = new ShipPlacement();
            App.Current.MainWindow = window;
            this.Close();
            window.Show();
        }

        private void Player_Click(object sender, RoutedEventArgs e)
        {
            // start a player vs player game

        }

        private void Highscores_Click(object sender, RoutedEventArgs e)
        {
            // show the highscores window

        }
    }
}
