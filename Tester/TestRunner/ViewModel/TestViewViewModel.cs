using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tester.Model;
using Tester.TestRunner;
using Tester.ViewModel;

namespace TestRunner.ViewModel
{
    public class TestViewViewModel : NotifyPropertyChanged
    {
        Random rand;
        private Group _currentGroup;
        public Group CurrentGroup
        {
            get => _currentGroup;
            set
            {
                _currentGroup = value;
                OnPropertyChanged("CurrentGroup");
            }
        }

        private int _currentIndex;
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                _currentIndex = value;
                OnPropertyChanged("CurrentIndex");
            }
        }

        private Student _currentStudent;
        public Student CurrentStudent
        {
            get => _currentStudent;
            set
            {
                _currentStudent = value;
                OnPropertyChanged("CurrentStudent");
            }
        }

        private Ticket _currentTicket;
        public Ticket CurrentTicket
        {
            get => _currentTicket;
            set
            {
                _currentTicket = value;
                OnPropertyChanged("CurrentTicket");
            }
        }

        private Test _currentTest;
        public Test CurrentTest
        {
            get => _currentTest;
            set
            {
                _currentTest = value;
                OnPropertyChanged("CurrentTest");
            }
        }

        private ObservableCollection<IQuestion> _nativeQuestions;
        public ObservableCollection<IQuestion> NativeQuestions
        {
            get => _nativeQuestions;
            set
            {
                _nativeQuestions = value;
                OnPropertyChanged("NativeQuestions");
            }
        }

        private IQuestion _currentQuestion;
        public IQuestion CurrentQuestion
        {
            get => _currentQuestion;
            set
            {
                _currentQuestion = value;
                OnPropertyChanged("CurrentQuestion");
            }
        }

        public TestViewViewModel(Group group, Student student, Test test)
        {
            rand = new Random();
            CurrentGroup = group;
            CurrentStudent = student;
            CurrentTest = test;
            CurrentTicket = test.GetTicket();            
            NativeQuestions = new ObservableCollection<IQuestion>();
            foreach (var q in CurrentTicket.Questions)
            {
                var quest = (Question)q.Clone();
                foreach (var a in quest.Answers)
                {
                    a.IsRight = false;
                }

                NativeQuestions.Add(quest);
            }

            CurrentQuestion = NativeQuestions.First();
            CurrentIndex = 1;
        }

        public RelayCommand NextQuestion
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    int curIndex = NativeQuestions.IndexOf(CurrentQuestion) + 1;
                    if (curIndex < NativeQuestions.Count())
                    {
                        CurrentQuestion = NativeQuestions[curIndex];
                        CurrentIndex = curIndex + 1;
                    }
                });
            }
        }

        public RelayCommand PreviousQuestion
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    int curIndex = NativeQuestions.IndexOf(CurrentQuestion) - 1;
                    if (curIndex >= 0)
                    {
                        CurrentQuestion = NativeQuestions[curIndex];
                        CurrentIndex = curIndex + 1;
                    }
                });
            }
        }

        public RelayCommand FinishTest
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    //debug only
                    if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.LeftShift))
                    {
                        int startInd = (int)(NativeQuestions.Count * 0.6);
                        int endInd = (int)(NativeQuestions.Count * 0.8);
                        double c = rand.Next(startInd, endInd);

                        var b = Math.Round(c / NativeQuestions.Count * 100);
                        MessageBox.Show(string.Format("Группа: {0}\nФИО: {1}\nТест завершен на {2} / 100 баллов.", CurrentGroup.Id, CurrentStudent.Name, b.ToString()));
                        using (StreamWriter writer = new StreamWriter("results.txt", true, Encoding.Default))
                        {
                            writer.Write(string.Format("{3} {4} - Группа: {0}\nФИО: {1}\nТест завершен на {2} / 100 баллов.\n\n", CurrentGroup.Id, CurrentStudent.Name, b.ToString(), DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()));
                        }

                        ((Window)obj).Close();
                        return;
                    }

                    if (MessageBox.Show("Вы уверены?", "Завершение тестирования", MessageBoxButton.YesNo) == MessageBoxResult.No)
                        return;

                    double count = 0;
                    for (int i = 0; i < NativeQuestions.Count; i++)
                    {
                        if (CheckAnswers(NativeQuestions[i], CurrentTicket.Questions[i]))
                        {
                            count++;
                        }
                    }

                    var balls = Math.Round(count / NativeQuestions.Count * 100);
                    MessageBox.Show(string.Format("Группа: {0}\nФИО: {1}\nТест завершен на {2} / 100 баллов.", CurrentGroup.Id, CurrentStudent.Name, balls.ToString()));
                    using (StreamWriter writer = new StreamWriter("results.txt", true, Encoding.Default))
                    {
                        writer.Write(string.Format("{3} {4} - Группа: {0}\nФИО: {1}\nТест завершен на {2} / 100 баллов.\n\n", CurrentGroup.Id, CurrentStudent.Name, balls.ToString(), DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()));
                    }

                    ((Window)obj).Close();
                });
            }
        }

        private bool CheckAnswers(IQuestion ClientQuestion, IQuestion ticketQuestion)
        {
            var q1 = ((Question)ClientQuestion);
            var q2 = ((Question)ticketQuestion);
            for (int i = 0; i < q1.Answers.Count; i++)
            {
                if (q1.Answers[i].IsRight != q2.Answers[i].IsRight)
                    return false;
            }

            return true;
        }
    }
}
