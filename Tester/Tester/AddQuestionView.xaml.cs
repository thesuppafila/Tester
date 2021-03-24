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

namespace Tester
{
    /// <summary>
    /// Логика взаимодействия для AddQuestionView.xaml
    /// </summary>
    public partial class AddQuestionView : Window
    {
        public AddQuestionView()
        {
            InitializeComponent();
        }

        private void addAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            answerListBox.Items.Add(answerTextBox.Text);
        }

        private void deleteAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            answerListBox.Items.Remove(answerListBox.SelectedItem);
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
