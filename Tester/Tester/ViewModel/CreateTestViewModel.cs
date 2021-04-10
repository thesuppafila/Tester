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
using Tester.TestCreator;
using Tester.ViewModel;

namespace Tester.ViewModel
{
    public class CreateTestViewModel : INotifyPropertyChanged
    {
        public CreateQuestionViewModel createQuestionViewModel = new CreateQuestionViewModel();
        public CreateQuestionGroupViewModel createQuestionGroupViewModel = new CreateQuestionGroupViewModel();

        public CreateTestViewModel()
        {
            CurrentTest = new Test();
        }

        public CreateTestViewModel(Test currentTest)
        {
            CurrentTest = currentTest;
        }

        private bool? dialogResult;
        public bool? DialogResult
        {
            get
            {
                return dialogResult;
            }
            set
            {
                dialogResult = value;
                OnPropertyChanged("DialogResult");
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

        public string Name
        {
            get
            {
                return CurrentTest.Name;
            }
            set
            {
                CurrentTest.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public ObservableCollection<IQuestion> Questions
        {
            get
            {
                return CurrentTest.Questions;
            }
            set
            {
                CurrentTest.Questions = value;
                OnPropertyChanged("Questions");
            }
        }

        private IQuestion selectedQuestion;
        public IQuestion SelectedQuestion
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

        private RelayCommand addNewQuestionCommand;
        public RelayCommand AddNewQuestionCommand
        {
            get
            {
                return addNewQuestionCommand ??
                    (addNewQuestionCommand = new RelayCommand(obj =>
                    {
                        CreateQuestionView createQuestionView = new CreateQuestionView(createQuestionViewModel);
                        if (createQuestionView.DialogResult == true)
                        {
                            Questions.Add(createQuestionViewModel.CurrentQuestion);
                        }
                    }));
            }
        }

        private RelayCommand removeQuestionCommand;
        public RelayCommand RemoveQuestionCommand
        {
            get
            {
                return removeQuestionCommand ??
                    (removeQuestionCommand = new RelayCommand(obj =>
                    {
                        if (SelectedQuestion != null)
                            Questions.Remove(SelectedQuestion);
                    }));
            }
        }

        private RelayCommand editQuestionCommand;
        public RelayCommand EditQuestionCommand
        {
            get
            {
                return editQuestionCommand ??
                    (editQuestionCommand = new RelayCommand(obj =>
                    {
                        if (SelectedQuestion != null)
                        {
                            if (SelectedQuestion is Question)
                            {
                                createQuestionViewModel = new CreateQuestionViewModel { CurrentQuestion = SelectedQuestion };
                                CreateQuestionView createQuestionView = new CreateQuestionView(createQuestionViewModel);
                                if (createQuestionView.ShowDialog() == true)
                                {
                                    SelectedQuestion = createQuestionViewModel.CurrentQuestion;
                                }
                            }
                            else if (SelectedQuestion is QuestionGroup)
                            {
                                createQuestionGroupViewModel = new CreateQuestionGroupViewModel { CurrentQuestion = SelectedQuestion };

                                CreateQuestionGroupView createQuestionGroupView = new CreateQuestionGroupView(createQuestionGroupViewModel);
                                if (createQuestionGroupView.ShowDialog() == true)
                                {
                                    SelectedQuestion = createQuestionGroupViewModel.CurrentQuestion;
                                }
                            }
                        }
                    }));
            }
        }

        private RelayCommand loadQuestionFromFileCommand;
        public RelayCommand LoadQuestionFromFileCommand
        {
            get
            {
                return loadQuestionFromFileCommand ??
                    (loadQuestionFromFileCommand = new RelayCommand(obj =>
                    {
                        CurrentTest.LoadFromFile();
                        if (CurrentTest.Name != null)
                            Name = CurrentTest.Name;
                    }));
            }
        }

        private RelayCommand okCommand;
        public RelayCommand OkCommand
        {
            get
            {
                return okCommand ?? (
                    okCommand = new RelayCommand(obj =>
                    {
                        if (currentTest.IsValid())
                            DialogResult = true;
                    }));
            }
        }

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ??
                    (cancelCommand = new RelayCommand(obj =>
                    {
                        DialogResult = false;
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
