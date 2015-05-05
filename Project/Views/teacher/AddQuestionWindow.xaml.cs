// Auther: Joren Martens
// Date: 04/05/2015

using Project.Controllers;
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
using System.Windows.Shapes;

namespace Project.Views
{
    /// <summary>
    /// Interaction logic for AddQuestionWindow.xaml
    /// </summary>
    public partial class AddQuestionWindow : Window
    {
        private string cource;

        public AddQuestionWindow()
        {
            InitializeComponent();
        }
        // ComboBox with all the cources
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("Geography");
            data.Add("Language");
            data.Add("Math");

            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = data;
            comboBox.SelectedIndex = 0;
            this.cource = data[0];
        }

        // Get selected cource
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            this.cource = comboBox.SelectedItem as string;

            this.SwitchContent();
        }

        private void SwitchContent()
        {
            switch (cource)
            {
                case "Geography":
                    solutionLabel1.Content = "Correct solution:";

                    solutionLabel2.Visibility = Visibility.Visible;
                    solutionLabel2.Content = "Wrong solution:";
                    solutionTextBox2.Visibility = Visibility.Visible;

                    solutionLabel3.Visibility = Visibility.Hidden;
                    solutionTextBox3.Visibility = Visibility.Hidden;

                    break;
                case "Language":
                    solutionLabel1.Content = "Solution TT:";

                    solutionLabel2.Visibility = Visibility.Visible;
                    solutionLabel2.Content = "Solution VT:";
                    solutionTextBox2.Visibility = Visibility.Visible;

                    solutionLabel3.Visibility = Visibility.Visible;
                    solutionLabel3.Content = "Solution VD:";
                    solutionTextBox3.Visibility = Visibility.Visible;
                    break;
                case "Math":
                    solutionLabel1.Content = "Solution:";

                    solutionLabel2.Visibility = Visibility.Hidden;
                    solutionTextBox2.Visibility = Visibility.Hidden;

                    solutionLabel3.Visibility = Visibility.Hidden;
                    solutionTextBox3.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void AddQuestionButton(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(questionTextBox.Text))
            {
                // is null
            }
            else
                if (String.IsNullOrEmpty(solutionTextBox1.Text))
                {
                    // is null
                }
                else
                {
                    switch (cource)
                    {
                        case "Geography":
                            if (String.IsNullOrEmpty(solutionTextBox2.Text))
                            {
                                // is null
                            }
                            else
                            {
                                string[] recordsGeography = { questionTextBox.Text, solutionTextBox1.Text, solutionTextBox2.Text };
                                DB.AddRecord(ProjectConfig.QuestionsFileGeo, recordsGeography);
                            }
                            break;
                        case "Language":
                            if (String.IsNullOrEmpty(solutionTextBox2.Text))
                            {
                                // is null
                            }
                            else
                                if (String.IsNullOrEmpty(solutionTextBox3.Text))
                                {
                                    // is null
                                }
                                else
                                {
                                    string[] recordsLanguage = { questionTextBox.Text, solutionTextBox1.Text, solutionTextBox2.Text, solutionTextBox3.Text };
                                    DB.AddRecord(ProjectConfig.QuestionsFileLang, recordsLanguage);
                                }
                            break;
                        case "Math":
                            string[] recordsMath = { questionTextBox.Text, solutionTextBox1.Text };
                            DB.AddRecord(ProjectConfig.QuestionsFileMath, recordsMath);
                            break;
                    }
                }
        }
    }
}
