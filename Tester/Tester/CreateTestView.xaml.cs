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
using System.Xml.Serialization;
using Tester.Model;

namespace Tester
{
    /// <summary>
    /// Логика взаимодействия для CreateTestView.xaml
    /// </summary>
    public partial class CreateTestView : Window
    {
        public Test test;

        public List<Model.Group> groups;

        public CreateTestView()
        {
            InitializeComponent();
            test = new Test();
            groups = new List<Model.Group>();
        }

        private void addQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            AddQuestionView addQuestionView = new AddQuestionView();
            if (addQuestionView.ShowDialog() == true)
                this.test.AddQuestion(addQuestionView.GetQuestion());
            RefreshQuestions();
        }

        private void addQuestionFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                if (fileInfo.Extension.ToLower() == ".txt")
                {
                    test = new Test();
                    var questionBones = Regex.Matches(File.ReadAllText("Data\\questions.txt"), @"(?<=\?)(.)*\n(\#.*\n)*", RegexOptions.Multiline);
                    foreach (var bone in questionBones)
                        test.AddQuestion(new Question(bone.ToString()));
                }
                if (fileInfo.Extension.ToLower() == ".xml")
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(Test));

                    using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.OpenOrCreate))
                    {
                        test = (Test)formatter.Deserialize(fs);
                    }
                }
                RefreshQuestions();
            }
        }

        private void deleteSelectedQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (questionsListBox.SelectedItem != null)
                test.RemoveQuestion((Question)questionsListBox.SelectedItem);
            RefreshQuestions();
        }

        private void questionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshAnswers();
        }

        private void addGroupFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            //сделать парсинг из файла
        }

        private void deleteGroupButton_Click(object sender, RoutedEventArgs e)
        {
            if (groupListBox.SelectedItem != null)
                groups.Remove((Model.Group)groupListBox.SelectedItem);
            RefreshGroups();
        }

        private void addGroupButton_Click(object sender, RoutedEventArgs e)
        {
            if (newGroupIdTextBox.Text == String.Empty)
                MessageBox.Show("Введите номер группы.");
            else
                groups.Add(new Model.Group(newGroupIdTextBox.Text));
            RefreshGroups();
        }

        private void groupListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshStudents();
        }

        private void RefreshQuestions()
        {
            questionsListBox.ItemsSource = test.GetQuestions();
            questionsListBox.Items.Refresh();
            RefreshAnswers();
        }

        private void RefreshAnswers()
        {
            trueAnswerTextBox.Clear();
            if (questionsListBox.SelectedItem != null)
            {
                questionTextBlock.Text = questionsListBox.SelectedItem.ToString();
                Question selectedQuestion = (Question)questionsListBox.SelectedItem;
                answersListBox.ItemsSource = selectedQuestion.GetAnswers();
                if (selectedQuestion.IsMultiple)
                {
                    string trueAnswer = string.Empty;
                    foreach (Answer answer in selectedQuestion.GetAnswers())
                        if (answer.Right)
                            trueAnswerTextBox.Text += answer + "\n";
                }
                else
                    trueAnswerTextBox.Text = selectedQuestion.GetAnswers().Where(a => a.Right == true).Single().ToString();
                answersListBox.Items.Refresh();
            }
            else
            {
                questionTextBlock.Text = null;
                answersListBox.ItemsSource = null;
                answersListBox.Items.Refresh();
            }
        }

        private void addStudentButton_Click(object sender, RoutedEventArgs e)
        {
            if (studentNameTextBox.Text == string.Empty)
                MessageBox.Show("Введите ФИО студента.");
            else
            {
                Model.Group selectedGroup = (Model.Group)groupListBox.SelectedItem;
                selectedGroup.AddStudent(new Student(studentNameTextBox.Text));
            }
            RefreshStudents();
        }

        private void deleteStudentButton_Click(object sender, RoutedEventArgs e)
        {
            Model.Group group = (Model.Group)groupListBox.SelectedItem;
            if (studentsListBox.SelectedItem != null)
                group.DeleteStudent((Student)studentsListBox.SelectedItem);
            RefreshStudents();
        }

        private void RefreshGroups()
        {
            groupListBox.ItemsSource = groups;
            groupListBox.Items.Refresh();
            RefreshStudents();
        }

        private void RefreshStudents()
        {
            if (groupListBox.SelectedItem != null)
            {
                groupIdTextBox.Text = groupListBox.SelectedItem.ToString();
                Model.Group selectedGroup = (Model.Group)groupListBox.SelectedItem;
                studentsListBox.ItemsSource = selectedGroup.GetStudents();
                studentsListBox.Items.Refresh();
            }
            else
            {
                groupIdTextBox.Text = null;
                studentsListBox.ItemsSource = null;
                studentsListBox.Items.Refresh();
            }
        }

        private void saveTestButton_Click(object sender, RoutedEventArgs e)
        {
            if (testNameTextBox.Text == string.Empty)
                MessageBox.Show("Введите название теста");

            if (test.questions.Count == 0)
                MessageBox.Show("Тест должен содержать хотя бы один вопрос.");
            else
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Test));

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = testNameTextBox.Text;
                saveFileDialog.Filter = "XML-FILE | *.xml";
                if (saveFileDialog.ShowDialog() == true)
                {
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate))
                    {
                        formatter.Serialize(fs, test);
                    }
                }
            }
        }

        public Test GetTest()
        {
            return test;
        }
    }
}
