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
using Tester.Model;

namespace Tester.TestRunner
{
    /// <summary>
    /// Логика взаимодействия для TestView.xaml
    /// </summary>
    public partial class TestView : Window
    {
        Dictionary<int, List<Answer>> testResult;
        Question curQuestion;
        Ticket curTicket;
        int curIndex = 0;
        public Test CurrentTest;

        public TestView(string group, string name, Ticket ticket)
        {
            InitializeComponent();
            curTicket = ticket;
            testInfoLabel.Content = "Студент: " + name + ". Группа: " + group + ". Тип тестирования: проверочная работа.";
            testResult = new Dictionary<int, List<Answer>>();
        }

        private void LoadQuestion(Ticket ticket, int index)
        {
            currentQuestionLabel.Content = "Вопрос " + curIndex + " из " + ticket.Questions.Count();
            if (curQuestion != null)
            {
                var answers = new List<Answer>();
                foreach (var v in answersPanel.Children.OfType<CheckBox>())
                {
                    if (v.IsChecked == true)
                    {
                        var ans = curQuestion.Answers.Where(x => x.Body == (string)v.Content).Single();
                        if (testResult.TryGetValue(curIndex, out answers))
                        {
                            testResult.Remove(curIndex);
                        }
                        answers.Clear();
                        answers.Add(ans);
                        testResult.Add(curIndex, answers);
                    }
                }
            }
            curQuestion = ticket.Questions[index];
            questionText.Content = curQuestion.Body;
            answersPanel.Children.Clear();
            foreach (var v in curQuestion.Answers)
            {
                answersPanel.Children.Add(new CheckBox() { Content = v.Body });
            }
        }

        private void nextQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            LoadQuestion(curTicket, ++curIndex);
        }

        private void beforeQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            LoadQuestion(curTicket, --curIndex);
        }

        private void endTestButton_Click(object sender, RoutedEventArgs e)
        {
            int balls = 0;
            for (int i = 0; i < curTicket.Questions.Count(); i++)
            {
                bool isRight = true;
                List<Answer> answers = new List<Answer>();
                if (testResult.TryGetValue(i, out answers))
                {
                    foreach (var v in answers)
                    {
                        if (curTicket.Questions[i].Answers.Where(x => x.Body == v.Body).Single().IsRight == false)
                        {
                            isRight = false;
                        }
                    }
                    if (isRight)
                        balls++;
                }
            }

            MessageBox.Show(balls.ToString());
        }
    }
}
