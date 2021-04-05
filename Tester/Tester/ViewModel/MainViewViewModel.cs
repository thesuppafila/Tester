using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tester.ViewModel;
using Tester.TestRunner;
using Tester.Model;

namespace Tester.ViewModel
{
    public class MainViewViewModel : BaseViewModel
    {
        public Test test;

        public MainViewViewModel()
        {
            test = new Test();
            test.LoadFile();
        }

        private RelayCommand selectTestModeCommand;
        public RelayCommand SelectTestModeCommand
        {
            get
            {
                return selectTestModeCommand ??
                    (selectTestModeCommand = new RelayCommand(obj =>
                {
                    StartTestView startTestView = new StartTestView();
                    startTestView.currentTest = test;
                    startTestView.Show();
                }));
            }
        }
        private RelayCommand selectCreateModeCommand;
        public RelayCommand SelectCreateModeCommand
        {
            get
            {
                return selectCreateModeCommand ??
                    (selectCreateModeCommand = new RelayCommand(obj =>
                    {
                        CreateTestView createTestView = new CreateTestView();
                        createTestView.Show();
                    }));
            }
        }
    }
}
