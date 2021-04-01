using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using Tester.Model;
using Tester.ViewModel;

namespace Tester.ViewModel
{
    public class CreateTestViewModel : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {                
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private Test currentTest;
        public Test CurrentTest
        {
            get
            {
                return currentTest;
            }
            set
            {
                currentTest = value;
                OnPropertyChanged("CurrentTest");
            }
        }

        private Question selectedQuestion;
        public Question SelectedQuestion
        {
            get
            {
                return selectedQuestion;
            }
            set
            {
                selectedQuestion = value;
                OnPropertyChanged("SelectedQuestion");
            }
        }

        private Question newQuestion;
        public Question NewQuestion
        {
            get { return newQuestion; }
            set
            {
                if (value != null)
                    newQuestion = value;
                OnPropertyChanged("NewQuestion");
            }
        }

        private Model.Group selectedGroup;
        public Model.Group SelectedGroup
        {
            get
            {
                return selectedGroup;
            }
            set
            {
                selectedGroup = value;
                OnPropertyChanged("SelectedGroup");
            }
        }

        private ObservableCollection<Question> questions;
        public ObservableCollection<Question> Questions
        {
            get
            {
                return questions;
            }
            set
            {
                questions = value;
                OnPropertyChanged("Questions");
            }
        }

        public CreateQuestionViewModel createQuestionViewModel;

        private RelayCommand addNewQuestion;
        public RelayCommand AddNewQuestion
        {
            get
            {
                return addNewQuestion ??
                    (addNewQuestion = new RelayCommand(obj =>
                    {
                        CreateQuestionView createQuestionView = new CreateQuestionView();
                        createQuestionViewModel = new CreateQuestionViewModel();
                        createQuestionView.DataContext = createQuestionViewModel; //мне не очень нравится этот подход, нарушает MVVM
                        if (createQuestionView.ShowDialog() == true)
                            if (createQuestionViewModel.IsValidQuestion())
                                newQuestion = createQuestionViewModel.Question;
                        if (Questions == null)
                            Questions = new ObservableCollection<Question>();
                        if (NewQuestion != null)
                            Questions.Add(NewQuestion);
                    }));
            }
        }

        private RelayCommand addNewQuestionFromFile;
        public RelayCommand AddNewQuestionFromFile
        {
            get
            {
                return addNewQuestionFromFile ??
                    (addNewQuestionFromFile = new RelayCommand(obj =>
                    {
                        Test newTest = new Test();
                        newTest.LoadFromFile();
                        if (newTest != null)
                            Questions = newTest.QuestionsList;
                    }));
            }
        }

        private RelayCommand removeQuestion;
        public RelayCommand RemoveQuestion
        {
            get
            {
                return removeQuestion ??
                    (removeQuestion = new RelayCommand(obj =>
                    {
                        Question question = obj as Question;
                        if (question != null)
                            Questions.Remove(question);
                    }));
            }
        }

        private RelayCommand saveTest;
        public RelayCommand SaveTest
        {
            get
            {
                return saveTest ??
                    (saveTest = new RelayCommand(obj =>
                    {
                        if (currentTest != null && currentTest.IsValid())
                            currentTest.SaveToFile();
                    }));
            }
        }

        public CreateTestViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
