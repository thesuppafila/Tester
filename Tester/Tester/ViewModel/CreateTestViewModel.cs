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
                if (name == null)
                    name = string.Empty;
                return name;
            }
            set
            {
                name = value;
                currentTest.Name = name;
                OnPropertyChanged("Name");
            }
        }

        private Test currentTest;
        public Test CurrentTest
        {
            get
            {
                if (currentTest == null)
                    currentTest = new Test();
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
                {
                    newQuestion = value;
                    Questions.Add(newQuestion);
                    currentTest.QuestionsList.Add(newQuestion);
                }
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
                if (questions == null)
                    questions = new ObservableCollection<Question>();
                return questions;
            }
            set
            {
                questions = value;
                currentTest.QuestionsList = questions;
                OnPropertyChanged("Questions");
            }
        }

        public CreateQuestionViewModel createQuestionViewModel;

        public CreateTestViewModel()
        {
            currentTest = new Test();
            questions = new ObservableCollection<Question>();
        }

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
                                NewQuestion = createQuestionViewModel.Question;
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
                        if (currentTest.IsValid())
                            currentTest.SaveToFile();
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
