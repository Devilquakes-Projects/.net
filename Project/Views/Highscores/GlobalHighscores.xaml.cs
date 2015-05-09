// Joris Meylaers
// Date: 08/05/2015
using Project.Controllers;
using Project.Exceptions;
using System;
using System.Collections;
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
using System.Windows.Shapes;

namespace Project.Views
{
    /// <summary>
    /// Interaction logic for GlobalHighscores.xaml
    /// </summary>
    public partial class GlobalHighscores : Window
    {
        private string userHighscore;
        private IList list;
        private List<string> data;
        private string selectedGame;
        private List<string[]> fullDB;
        private string[,] all;

        public GlobalHighscores()
        {
            InitializeComponent();
            list = highscoresListBox.Items;
        }

        private void gamesComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            data = new List<string>();
            data.Add("Snake");
            data.Add("Ballgame");

            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = data;
            comboBox.SelectedIndex = 0;
            selectedGame = data[0];
            fullDB = DB.GetDB(ProjectConfig.SnakeFile);
        }

        private void gamesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            this.selectedGame = comboBox.SelectedItem as string;
            if (selectedGame.Equals("Snake"))
            {
                highscoresListBox.Items.Clear();
                fullDB = DB.GetDB(ProjectConfig.SnakeFile);
            }
            else
            {
                highscoresListBox.Items.Clear();
                fullDB = DB.GetDB(ProjectConfig.BallFile);
            }
        }

        private void showButton_Click(object sender, RoutedEventArgs e)
        {
            all = new string[fullDB.Count, 2];
            try
            {
                for (int i = 0; i < fullDB.Count; i++)
                {
                    string[] row = fullDB[i];
                    string username = DB.FindFirst(ProjectConfig.UserFile, "id", row[1])[3];
                    userHighscore = row[0];
                    all[i, 0] = username;
                    all[i, 1] = userHighscore;
                }
            }
            catch (NoRecordFoundException)
            {
                MessageBox.Show("Unable to download highscores");
            }
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newView = new MainWindow();
            newView.Show();
            this.Close();
        }
    }
}
