﻿using Project.Controllers;
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
    /// Interaction logic for EditQuestionWindow.xaml
    /// </summary>
    public partial class EditQuestionWindow : Window
    {
        private string course;
        IList list;
        List<string> data;

        public EditQuestionWindow()
        {
            InitializeComponent();
            list = QuestionsListBox.Items;

        }

        private void CoursesComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            data = new List<string>();
            data.Add("Geography");
            data.Add("Language");
            data.Add("Math");

            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = data;
            comboBox.SelectedIndex = 0;
            course = data[0];
        }

        private void CoursesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            this.course = comboBox.SelectedItem as string;

            SwitchContent();
        }

        private void SwitchContent()
        {
            switch (course)
            {
                case "Geography":
                    QuestionsListBox.Items.Clear();
                    List<string[]> geoQuestions = DB.GetDB(ProjectConfig.QuestionsFileGeo);
                    for (int i = 0; i < geoQuestions.Count; i++)
                    {
                        string text = null;
                        string[] line = geoQuestions.ElementAt(i);
                        for (int j = 0; j < line.Length; j++)
                        {
                            text = text + line[j] + " | ";
                        }
                        QuestionsListBox.Items.Add(text);
                    }
                    break;
                case "Language":
                    QuestionsListBox.Items.Clear();
                    List<string[]> lanQuestions = DB.GetDB(ProjectConfig.QuestionsFileLang);
                    for (int i = 0; i < lanQuestions.Count; i++)
                    {
                        string text = null;
                        string[] line = lanQuestions.ElementAt(i);
                        for (int j = 0; j < line.Length; j++)
                        {
                            text = text + line[j] + " | ";
                        }
                        QuestionsListBox.Items.Add(text);
                    }
                    break;
                case "Math":
                    QuestionsListBox.Items.Clear();
                    List<string[]> mathQuestions = DB.GetDB(ProjectConfig.QuestionsFileMath);
                    for (int i = 0; i < mathQuestions.Count; i++)
                    {
                        string text = null;
                        string[] line = mathQuestions.ElementAt(i);
                        for (int j = 0; j < line.Length; j++)
                        {
                            text = text + line[j] + " | ";
                        }
                        QuestionsListBox.Items.Add(text);
                    }
                    break;
            }
        }

        private void QuestionsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string textToChange = Convert.ToString(QuestionsListBox.SelectedItem);
            ChangeWindow editwindow = new ChangeWindow(textToChange, course.ToString());
            this.Close();
            editwindow.Show();
        }
    }
}
