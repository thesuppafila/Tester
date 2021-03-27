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
            question.AddAnswer(new Answer(answerTextBox.Text));
            Refresh();
        }

        private void deleteAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            question.GetAnswers().Remove((Answer)answerListBox.SelectedItem);
            Refresh();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            question.SetBody(questionTextBox.Text);
            this.DialogResult = true;
        }

        private void answerListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {            
            Answer answer = question.GetAnswers().Where(a => a.Equals((Answer)answerListBox.SelectedItem)).Single();
            if (!answer.IsRight())
                answer.SetRight();
            else answer.SetUnright();
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

            foreach (Answer answer in answerListBox.Items)
                if (answer.IsRight())
                    answerLabel.Content = String.Format("Правильный ответ: {0}.", answer.ToString());
        }
    }
}
