using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Tester.Model;

namespace Tester.ViewModel
{
    public class CreateQuestionViewModel : INotifyPropertyChanged
    {
        private bool? dialogResult;
        public bool? DialogResult
        {
            get
            {
                return dialogResult;
            }
            set
            {
                if (value != this.dialogResult)
                {
                    dialogResult = value;
                    OnPropertyChanged("DialogResult");
                }
            }
        }

        private Question question;
        public Question Question
        {
            get
            {
                return question;
            }
            set
            {
                question = value;
                OnPropertyChanged("Question");
            }
        }

        private string body;
        public string Body
        {
            get
            {
                return body;
            }
            set
            {
                body = value;
                OnPropertyChanged("Body");
            }
        }

        private bool isMultiple;
        public bool IsMultiple
        {
            get
            {
                return isMultiple;
            }
            set
            {
                isMultiple = value;
                if (!isMultiple && TrueAnswers != null)
                    TrueAnswers.Clear();
                OnPropertyChanged("IsMultiple");
            }
        }

        private string newAnswerBody;
        public string NewAnswerBody
        {
            get
            {
                return newAnswerBody;
            }
            set
            {
                newAnswerBody = value;
                OnPropertyChanged("NewAnswerBody");
            }

        }

        private Answer selectedAnswer;
        public Answer SelectedAnswer
        {
            get
            {
                return selectedAnswer;
            }
            set
            {
                selectedAnswer = value;
                OnPropertyChanged("SelectedAnswer");
            }
        }

        private Answer selectedTrueAnswer;
        public Answer SelectedTrueAnswer
        {
            get
            {
                return selectedTrueAnswer;
            }
            set
            {
                selectedTrueAnswer = value;
                OnPropertyChanged("SelectedTrueAnswer");
            }
        }

        private ObservableCollection<Answer> answers;
        public ObservableCollection<Answer> Answers
        {
            get
            {
                return answers;
            }
            set
            {
                answers = value;
                OnPropertyChanged("Answers");
            }
        }

        private ObservableCollection<Answer> trueAnswers;
        public ObservableCollection<Answer> TrueAnswers
        {
            get
            {
                return trueAnswers;
            }
            set
            {
                trueAnswers = value;
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
                            TrueAnswers.Clear();
                        if (SelectedAnswer != null && !TrueAnswers.Contains(SelectedAnswer))
                            TrueAnswers.Add(SelectedAnswer);
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
        public RelayCommand OKCommand
        {
            get
            {
                return okCommand ??
                    (okCommand = new RelayCommand(obj =>
                    {
                        this.Question = new Question();

                        if (Body != string.Empty && Answers != null && TrueAnswers != null)
                        {
                            Question.Body = Body;
                            Question.Answers = Answers;
                            Question.TrueAnswers = TrueAnswers;
                        }
                        DialogResult = true;
                    }));
            }
        }


        public bool IsValidQuestion()
        {
            return Question.IsValid();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
