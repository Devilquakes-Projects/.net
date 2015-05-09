// Joris Meylaers
// Date 08/05/2015
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
    /// Interaction logic for HighscorePerUser.xaml
    /// </summary>
    public partial class HighscorePerUser : Window
    {
        private string nameToCheck;
        private string userHighscore;
        private IList list;
        private List<string> data;
        private string selectedGame;
        private List<string[]> fullDB;

        public HighscorePerUser()
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

        private void checkButton_Click(object sender, RoutedEventArgs e)
        {

            nameToCheck = nameTextBox.Text;
            highscoresListBox.Items.Clear();
            try
            {
                for (int i = 0; i < fullDB.Count; i++)
                {
                    string[] row = fullDB[i];
                    string[] userArray = DB.FindFirst(ProjectConfig.UserFile, "username", nameToCheck);
                    userHighscore = row[5];

                    if (userArray[0].Equals(row[2]))
                    {
                        list.Add(userArray[2] + "\t-\t" + userHighscore);
                    }
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
