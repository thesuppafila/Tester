using System;
using System.Collections.ObjectModel;
using System.Windows;
using Tester.Model;

namespace Tester.ViewModel
{
    public class CreateTestViewModel : NotifyPropertyChanged
    {
        private bool? dialogResult;
        public bool? DialogResult
        {
            get => dialogResult;
            set
            {
                dialogResult = value;
                OnPropertyChanged("DialogResult");
            }
        }

        private Test currentTest;
        public Test CurrentTest
        {
            get => currentTest;
            set
            {
                currentTest = value;
                OnPropertyChanged("CurrentTest");
            }
        }

        private IQuestion selectedQuestion;
        public IQuestion SelectedQuestion
        {
            get => selectedQuestion;
            set
            {
                if (value is Question)
                    IsGroupQuestion = false;
                else
                {
                    SelectedGroupQuestion = value;

                }
                selectedQuestion = value;
                OnPropertyChanged("SelectedQuestion");
            }
        }

        private IQuestion selectedGroupQuestion;
        public IQuestion SelectedGroupQuestion
        {
            get => selectedGroupQuestion;
            set
            {
                selectedGroupQuestion = value;
                OnPropertyChanged("SelectedGroupQuestion");
            }
        }

        private bool isGroupQuestion;
        public bool IsGroupQuestion
        {
            get => isGroupQuestion;
            set
            {
                if (SelectedQuestion is QuestionGroup)
                    isGroupQuestion = true;
                else isGroupQuestion = false;
                OnPropertyChanged("IsGroupQuestion");
            }
        }

        private Group selectedGroup;
        public Group SelectedGroup
        {
            get => selectedGroup;
            set
            {
                selectedGroup = value;
                OnPropertyChanged("SelectedGroup");
            }
        }

        public string Name
        {
            get => CurrentTest.Name;
            set
            {
                CurrentTest.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public int QuestionCount
        {
            get => CurrentTest.QuestionCount;
            set
            {
                CurrentTest.QuestionCount = value;
                OnPropertyChanged("QuestionCount");
            }
        }

        public ObservableCollection<IQuestion> Questions
        {
            get => CurrentTest.Questions;
            set
            {
                CurrentTest.Questions = value;
                OnPropertyChanged("Questions");
            }
        }

        public CreateTestViewModel(Test currentTest = null)
        {
            CurrentTest = currentTest ?? new Test();
        }

        private RelayCommand addNewQuestionCommand;
        public RelayCommand AddNewQuestionCommand
        {
            get
            {
                return addNewQuestionCommand ??
                    (addNewQuestionCommand = new RelayCommand(obj =>
                    {
                        var createQuestionViewModel = new CreateQuestionViewModel() { CurrentQuestion = new Question() };
                        CreateQuestionView createQuestionView = new CreateQuestionView(createQuestionViewModel);
                        if (createQuestionView.ShowDialog() == true)
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
                            var createQuestionViewModel = new CreateQuestionViewModel { CurrentQuestion = (Question)SelectedQuestion };
                            CreateQuestionView createQuestionView = new CreateQuestionView(createQuestionViewModel);
                            if (createQuestionView.ShowDialog() == true)
                            {
                                SelectedQuestion = createQuestionViewModel.CurrentQuestion;
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
                        else
                        {
                            MessageBox.Show("Введите все параметры.");
                        }
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
    }
}
