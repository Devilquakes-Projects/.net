// Author: Joris Meylaers
// Date: 28/04/2015
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
            questionTextBox.Text = null;
            solutionTextBox1.Text = null;
            solutionTextBox2.Text = null;
            solutionTextBox3.Text = null;
        }

        private void setLabels()
        {
            switch (course)
            {
                case "Aardrijkskunde":
                    solutionLabel1.Content = "Correct solution:";
                    solutionLabel2.Visibility = Visibility.Visible;
                    solutionLabel2.Content = "Wrong solution:";
                    solutionTextBox2.Visibility = Visibility.Visible;
                    solutionLabel3.Visibility = Visibility.Hidden;
                    solutionTextBox3.Visibility = Visibility.Hidden;
                    break;
                case "Nederlands":
                    solutionLabel1.Content = "Solution TT:";
                    solutionLabel2.Visibility = Visibility.Visible;
                    solutionLabel2.Content = "Solution VT:";
                    solutionTextBox2.Visibility = Visibility.Visible;
                    solutionLabel3.Visibility = Visibility.Visible;
                    solutionLabel3.Content = "Solution VD:";
                    solutionTextBox3.Visibility = Visibility.Visible;
                    break;
                case "Wiskunde":
                    solutionLabel1.Content = "Solution:";
                    solutionLabel2.Visibility = Visibility.Hidden;
                    solutionTextBox2.Visibility = Visibility.Hidden;
                    solutionLabel3.Visibility = Visibility.Hidden;
                    solutionTextBox3.Visibility = Visibility.Hidden;
                    break;

            }
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(questionTextBox.Text))
            {
                dataIncomplete();
            }
            else
                if (String.IsNullOrEmpty(solutionTextBox1.Text))
                {
                    dataIncomplete();
                }
                else
                {
                    switch (course)
                    {
                        case "Aardrijkskunde":
                            if (String.IsNullOrEmpty(solutionTextBox2.Text))
                            {
                                dataIncomplete();
                            }
                            else
                            {
                                string[] recordsGeography = { questionTextBox.Text, solutionTextBox1.Text, solutionTextBox2.Text };
                                DB.ChangeRecord(ProjectConfig.QuestionsFileGeo, id, recordsGeography);
                                dataSuccesfull();
                            }
                            break;
                        case "Nederlands":
                            if (String.IsNullOrEmpty(solutionTextBox2.Text))
                            {
                                dataIncomplete();
                            }
                            else
                                if (String.IsNullOrEmpty(solutionTextBox3.Text))
                                {
                                    dataIncomplete();
                                }
                                else
                                {
                                    string[] recordsLanguage = { questionTextBox.Text, solutionTextBox1.Text, solutionTextBox2.Text, solutionTextBox3.Text };
                                    DB.ChangeRecord(ProjectConfig.QuestionsFileLang, id, recordsLanguage);
                                    dataSuccesfull();
                                }
                            break;
                        case "Wiskunde":
                            string[] recordsMath = { questionTextBox.Text, solutionTextBox1.Text };
                            DB.ChangeRecord(ProjectConfig.QuestionsFileMath, id, recordsMath);
                            dataSuccesfull();
                            break;
                    }
                }

        }
        private void dataSuccesfull()
        {
            MessageBox.Show("Vraag succesvol gewijzigd");
            EditQuestionWindow questionWindow = new EditQuestionWindow();
            questionWindow.Show();
            this.Close();
        }

        private void dataIncomplete()
        {
            confirmLabel.Content = "Gelieve alle velden in te vullen";
            confirmLabel.Visibility = Visibility.Visible;
        }
    }
}
