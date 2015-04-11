// Auther: Joren Martens
// Date: 07/04/2015 14:46

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
using Project.Controllers;

namespace Project.Views
{
    /// <summary>
    /// Interaction logic for AddQuestion.xaml
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
            string [] cources = Course.AllCourses();

            for (int i = 0; i < cources.Length; i++)
            {
                data.Add(cources[i]);
            }
            
            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = data;
            comboBox.SelectedIndex = 0;
            this.cource = cources[0];
        }

        // Get selected cource
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            this.cource = comboBox.SelectedItem as string;
        }

        // Add question
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            string question = questionTextBox.Text;
            string solution = solutionTextBox.Text;

            if (Question.Add(cource, question, solution))
            {
                MessageBox.Show("added");
            }
            else
            {
                MessageBox.Show("Oeps er is iets mis gegaan");
            }
        }
    }
}
