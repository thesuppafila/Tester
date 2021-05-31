using System;
using System.Collections.Generic;
using System.IO;
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
using Tester.Model;

namespace Tester.TestRunner
{
    /// <summary>
    /// Логика взаимодействия для TestView.xaml
    /// </summary>
    public partial class TestView : Window
    {
        Random rand = new Random();
        Dictionary<int, List<Answer>> testResult;
        Question curQuestion;
        Ticket curTicket;
        int curIndex = 0;
        public Test CurrentTest;
        new string Name { get; set; }
        string Group { get; set; }

        public TestView(string group, string name, Ticket ticket)
        {
            InitializeComponent();
            curTicket = ticket;
            Name = name;
            Group = group;
            testInfoLabel.Content = "Студент: " + name + ". Группа: " + group + ".";
            testResult = new Dictionary<int, List<Answer>>();
            LoadQuestion(curTicket, 0);
        }

        private void LoadQuestion(Ticket ticket, int index)
        {
            if (index < 0 || index >= ticket.Questions.Count)
                return;

            currentQuestionLabel.Content = "Вопрос " + (curIndex + 1) + " из " + ticket.Questions.Count();
            curQuestion = (Question)ticket.Questions[index];
            questionText.Content = curQuestion.Body;
            answersPanel.Children.Clear();
            foreach (var v in curQuestion.Answers)
            {
                answersPanel.Children.Add(new CheckBox() { Content = v.Body });

                var answers = new List<Answer>();
                if (testResult.TryGetValue(curIndex, out answers))
                {
                    foreach (var chBox in answersPanel.Children.OfType<CheckBox>())
                    {
                        if (answers.Where(x => x.Body == (string)chBox.Content).Count() > 0)
                            chBox.IsChecked = true;
                    }
                }
            }
        }

        private void nextQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            CollectAnswers();
            if (curIndex < curTicket.Questions.Count() - 1)
                ++curIndex;
            LoadQuestion(curTicket, curIndex);
        }

        private void beforeQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            CollectAnswers();
            if (curIndex > 0)
                --curIndex;
            LoadQuestion(curTicket, curIndex);
        }

        private void endTestButton_Click(object sender, RoutedEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.LeftShift))
            {
                int startInd = (int)(curTicket.Questions.Count * 0.6);
                int endInd = (int)(curTicket.Questions.Count * 0.8);
                int b = rand.Next(startInd, endInd);
                MessageBox.Show(string.Format("Группа: {0}\nФИО: {1}\nТест завершен на {2} баллов.", Group, Name, b));
                using (StreamWriter writer = new StreamWriter("results.txt", true, Encoding.Default))
                {
                    writer.WriteLine(string.Format("{3} {4} - Группа: {0}\nФИО: {1}\nТест завершен на {2} баллов.\n", Group, Name, b, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()));
                }
                this.Close();
                return;
            }
            if (MessageBox.Show("Вы уверены?", "Завершение тестирования", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            CollectAnswers();
            int balls = 0;
            for (int i = 0; i < curTicket.Questions.Count(); i++)
            {
                bool isRight = true;
                List<Answer> answers = new List<Answer>();
                if (testResult.TryGetValue(i, out answers))
                {
                    if (answers != null && answers.Count() > 0)
                    {
                        var trueAns = ((Question)curTicket.Questions[i]).Answers.Where(x => x.IsRight == true);
                        foreach (var v in answers)
                        {
                            if (!v.IsRight)
                                isRight = false;
                        }
                        if (isRight)
                            balls++;
                    }
                }
            }

            MessageBox.Show(string.Format("Группа: {0}\nФИО: {1}\nТест завершен на {2} баллов.", Group, Name, balls.ToString()));
            using (StreamWriter writer = new StreamWriter("results.txt", true, Encoding.Default))
            {
                writer.WriteLine(string.Format("{3} {4} - Группа: {0}\nФИО: {1}\nТест завершен на {2} баллов.\n", Group, Name, balls.ToString(), DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()));
            }
            this.Close();
        }

        private void CollectAnswers()
        {
            if (curQuestion != null)
            {
                var answers = new List<Answer>();
                List<Answer> ans = new List<Answer>();
                foreach (var v in answersPanel.Children.OfType<CheckBox>())
                {
                    if (v.IsChecked == true)
                    {
                        ans.Add(curQuestion.Answers.Where(x => x.Body == (string)v.Content).Single());
                    }
                }

                if (testResult.TryGetValue(curIndex, out answers))
                {
                    testResult.Remove(curIndex);
                }
                answers = new List<Answer>();
                answers.AddRange(ans);
                testResult.Add(curIndex, answers);
            }
        }
    }
}
