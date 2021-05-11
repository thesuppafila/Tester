using System;
using System.Windows;
using System.Windows.Input;
using Tester.ViewModel;

namespace Tester
{
    /// <summary>
    /// Логика взаимодействия для CreateTestView.xaml
    /// </summary>
    public partial class CreateTestView : Window
    {
        CreateTestViewModel CreateTestViewModel;

        public CreateTestView(CreateTestViewModel createTestViewModel)
        {
            InitializeComponent();
            CreateTestViewModel = createTestViewModel;
            this.DataContext = CreateTestViewModel;
        }

        private void QuestionsCountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
