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

namespace Tester
{
    /// <summary>
    /// Логика взаимодействия для AddQuestionView.xaml
    /// </summary>
    public partial class AddQuestionView : Window
    {
        public Question question;

        public AddQuestionView()
        {
            InitializeComponent();
            question = new Question();
        }

        private void addAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (newAnswerTextBox.Text == string.Empty)
                MessageBox.Show("Ответ не может быть пустым.");
            else
            {
                question.AddAnswer(new Answer(newAnswerTextBox.Text));
                Refresh();
            }
        }

        private void deleteAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (answerListBox.SelectedItem != null)
            {
                question.GetAnswers().Remove((Answer)answerListBox.SelectedItem);
                Refresh();
            }
            else MessageBox.Show("Выберите ответ");
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsTrueQuestion())
            {
                question.SetBody(questionTextBox.Text);
                this.DialogResult = true;
            }
        }

        private void answerListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Answer answer = question.GetAnswers().Where(a => a.Equals((Answer)answerListBox.SelectedItem)).Single();
            if (multipleAnswerCheckBox.IsChecked == true)
                if (!answer.IsRight())
                    answer.SetRight();
                else answer.SetUnright();
            else
            {
                if (!answer.IsRight())
                {
                    foreach (Answer ans in question.GetAnswers())
                        ans.SetUnright();
                    answer.SetRight();
                }
                else answer.SetUnright();
            }
            Refresh();
        }


        public Question GetQuestion()
        {
            return question;
        }

        private void Refresh()
        {
            answerListBox.ItemsSource = question.GetAnswers();
            answerListBox.Items.Refresh();

            string answersString = string.Empty;
            if (multipleAnswerCheckBox.IsChecked == true)
            {
                foreach (Answer answer in answerListBox.Items)
                    if (answer.IsRight())
                        answersString += answer.ToString() + "\n";
                answerTextBox.Text = answersString;
            }
            else
            {
                foreach (Answer ans in question.GetAnswers())
                    if (ans.Right)
                        answersString = ans.ToString();
                answerTextBox.Text = answersString;
            }
        }

        private void multipleAnswerCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (multipleAnswerCheckBox.IsChecked == true)
            {
                foreach (Answer answer in question.GetAnswers())
                    answer.Right = false;
                question.IsMultiple = true;
            }
            else
            {
                foreach (Answer answer in question.GetAnswers())
                    answer.Right = false;
                question.IsMultiple = false;
            }
            Refresh();
        }

        private bool IsTrueQuestion()
        {
            if (questionTextBox.Text == string.Empty)
            {
                MessageBox.Show("Необходимо ввести вопрос");
                return false;
            }
            if (question.GetAnswers().Count == 0)
            {
                MessageBox.Show("В вопросе должны присутствовать ответы");
                return false;
            }
            foreach (Answer answer in question.GetAnswers())
                if (answer.Right)
                    return true;
                else
                    MessageBox.Show("Необходимо назначить правильный ответ.");
            return false;
        }
    }
}
