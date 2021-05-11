using System.Collections.ObjectModel;
using System.Windows;
using Tester.Model;

namespace Tester.ViewModel
{
    public class CreateQuestionViewModel : NotifyPropertyChanged
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

        private Question currentQuestion;
        public Question CurrentQuestion
        {
            get => currentQuestion;
            set
            {
                currentQuestion = value;
                OnPropertyChanged("CurrentQuestion");
            }
        }

        private string newAnswerBody;
        public string NewAnswerBody
        {
            get => newAnswerBody;
            set
            {
                newAnswerBody = value;
                OnPropertyChanged("NewAnswerBody");
            }
        }

        private Answer selectedAnswer;
        public Answer SelectedAnswer
        {
            get => selectedAnswer;
            set
            {
                selectedAnswer = value;
                OnPropertyChanged("SelectedAnswer");
            }
        }

        private Answer selectedTrueAnswer;
        public Answer SelectedTrueAnswer
        {
            get => selectedTrueAnswer;
            set
            {
                selectedTrueAnswer = value;
                OnPropertyChanged("SelectedTrueAnswer");
            }
        }

        public string Body
        {
            get => CurrentQuestion.Body;
            set
            {
                CurrentQuestion.Body = value;
                OnPropertyChanged("Body");
            }
        }

        public bool IsMultiple
        {
            get => CurrentQuestion.IsMultiple;
            set
            {
                CurrentQuestion.IsMultiple = value;
                if (!CurrentQuestion.IsMultiple && TrueAnswers != null)
                    TrueAnswers.Clear();
                OnPropertyChanged("IsMultiple");
            }
        }

        public ObservableCollection<Answer> Answers
        {
            get => CurrentQuestion.Answers;
            set
            {
                CurrentQuestion.Answers = value;
                OnPropertyChanged("Answers");
            }
        }

        public ObservableCollection<Answer> TrueAnswers
        {
            get => CurrentQuestion.TrueAnswers;
            set
            {
                CurrentQuestion.TrueAnswers = value;
                OnPropertyChanged("TrueAnswers");
            }
        }

        private RelayCommand addAnswerCommand;
        public RelayCommand AddAnswerCommand
        {
            get
            {
                return addAnswerCommand ??
                    (addAnswerCommand = new RelayCommand(obj =>
                    {
                        if (Answers == null)
                            Answers = new ObservableCollection<Answer>();
                        if (NewAnswerBody != null)
                            Answers.Add(new Answer(NewAnswerBody));
                    }));
            }
        }

        private RelayCommand removeAnswerCommand;
        public RelayCommand RemoveAnswerCommand
        {
            get
            {
                return removeAnswerCommand ??
                    (removeAnswerCommand = new RelayCommand(obj =>
                    {
                        if (SelectedAnswer != null)
                            Answers.Remove(SelectedAnswer);
                    }));
            }
        }

        private RelayCommand addTrueAnswerCommand;
        public RelayCommand AddTrueAnswerCommand
        {
            get
            {
                return addTrueAnswerCommand ??
                    (addTrueAnswerCommand = new RelayCommand(obj =>
                    {
                        if (TrueAnswers == null)
                            TrueAnswers = new ObservableCollection<Answer>();
                        if (!IsMultiple)
                        {
                            foreach (Answer answer in TrueAnswers)
                                answer.IsRight = false;
                            TrueAnswers.Clear();
                        }
                        if (SelectedAnswer != null && !TrueAnswers.Contains(SelectedAnswer))
                        {
                            SelectedAnswer.IsRight = true;
                            TrueAnswers.Add(SelectedAnswer);
                        }
                    }));
            }
        }

        private RelayCommand removeTrueAnswerCommand;
        public RelayCommand RemoveTrueAnswerCommand
        {
            get
            {
                return removeTrueAnswerCommand ??
                    (removeTrueAnswerCommand = new RelayCommand(obj =>
                    {
                        if (TrueAnswers != null)
                            if (TrueAnswers.Contains(SelectedTrueAnswer))
                                TrueAnswers.Remove(SelectedTrueAnswer);
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

        private RelayCommand okCommand;
        public RelayCommand OkCommand
        {
            get
            {
                return okCommand ??
                    (okCommand = new RelayCommand(obj =>
                    { 
                        if (CurrentQuestion.IsValid())
                            DialogResult = true;
                        else
                        {
                            MessageBox.Show("Вопрос некорректен.");
                        }
                    }));
            }
        }

        public bool IsValidQuestion()
        {
            return CurrentQuestion.IsValid();
        }
    }
}
