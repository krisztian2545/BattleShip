using System;
using System.Collections.Generic;
using System.IO;
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
using Newtonsoft.Json;

namespace BattleShip
{
    /// <summary>
    /// Interaction logic for HighScores.xaml
    /// </summary>
    public partial class HighScores : Window
    {
        private List<Summary> _highscores;
        private const string _filePath = "save.json";

        public HighScores() : this(null)
        {

        }

        public HighScores(Summary summ)
        {
            InitializeComponent();

            if (summ != null)
                SaveNewData(summ);

            LoadHighScores();
            _highscores.Reverse();
            dataGrid.ItemsSource = _highscores;
        }

        private void SaveNewData(Summary data)
        {
            using (StreamWriter file =
                    new StreamWriter(_filePath, true))
            {
                file.WriteLine(JsonConvert.SerializeObject(data));
            }
        }

        private void LoadHighScores()
        {
            _highscores = new List<Summary>();
            using (StreamReader r = new StreamReader(_filePath))
            {
                string json = r.ReadLine();
                while (json != null)
                {
                    Logger.Log($"Reading line : {json}");
                    _highscores.Add(JsonConvert.DeserializeObject<Summary>(json));
                    json = r.ReadLine();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
