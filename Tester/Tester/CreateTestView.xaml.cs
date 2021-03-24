using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tester.Model;

namespace Tester
{
    /// <summary>
    /// Логика взаимодействия для CreateTestView.xaml
    /// </summary>
    public partial class CreateTestView : Window
    {
        public CreateTestView()
        {
            InitializeComponent();
        }

        private void addQuestionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addQuestionFromFileButton_Click(object sender, RoutedEventArgs e)
        {

            List<Question> questions = new List<Question>();
            var questionBones = Regex.Matches(File.ReadAllText("Data\\questions.txt"), @"(?<=\?)(.)*\n(\#.*\n)*", RegexOptions.Multiline);
            foreach (var bone in questionBones)
                questions.Add(new Question(bone.ToString()));
            questionsListBox.ItemsSource = questions;
        }

        private void deleteSelectedQuestionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void questionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            questionTextBlock.Text = questionsListBox.SelectedItem.ToString();
            Question selectedQuestion = (Question)questionsListBox.SelectedItem;
            answersListBox.ItemsSource = selectedQuestion.Answers;
        }

        private void addGroupFromFileButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteGroupButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addGroupButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void groupListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
