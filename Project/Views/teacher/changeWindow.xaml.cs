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
    /// Interaction logic for changeWindow.xaml
    /// </summary>
    public partial class ChangeWindow : Window
    {

        private string course;
        private int id;

        public ChangeWindow(string content, string subject)
        {
            InitializeComponent();
            oldQuestionLabel.Content = content;
            course = subject;
            setLabels();
            id = Convert.ToInt32(content.Split(ProjectConfig.DBSeparator)[0]);

        }

        private void setLabels()
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
                    switch (course)
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
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
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
                    switch (course)
                    {
                        case "Geography":
                            if (String.IsNullOrEmpty(solutionTextBox2.Text))
                            {
                                // is null
                            }
                            else
                            {
                                string[] recordsGeography = { questionTextBox.Text, solutionTextBox1.Text, solutionTextBox2.Text };
                                DB.ChangeRecord(ProjectConfig.QuestionsFileGeo,id,recordsGeography);
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
                                    DB.ChangeRecord(ProjectConfig.QuestionsFileLang, id, recordsLanguage);
                                }
                            break;
                        case "Math":
                            string[] recordsMath = { questionTextBox.Text, solutionTextBox1.Text };
                            DB.ChangeRecord(ProjectConfig.QuestionsFileMath, id, recordsMath);
                            break;
                    }
                }
            EditQuestionWindow questionWindow = new EditQuestionWindow();
            questionWindow.Show();
            this.Close();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            switch (course)
            {
                case "Geography":
                    DB.HideRecord(ProjectConfig.QuestionsFileGeo, id);
                    break;
                case "Language":
                    DB.HideRecord(ProjectConfig.QuestionsFileLang, id);
                    break;
                case "Math":
                    DB.HideRecord(ProjectConfig.QuestionsFileMath, id);
                    break;
            }
        }
    }
}
