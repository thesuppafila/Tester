using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tester.Model
{
    [Serializable]
    public class Answer : INotifyPropertyChanged
    {
        private string body;

        private bool isRight;

        public string Body
        {
            get { return body; }
            set
            {
                body = value;
                OnPropertyChanged("Body");
            }
        }

        public bool IsRight
        {
            get { return isRight; }
            set
            {
                isRight = value;
                OnPropertyChanged("IsRight");
            }
        }


        public Answer()
        {

        }

        public Answer(Answer answer)
        {
            Body = answer.Body;
            IsRight = answer.IsRight;
        }

        public Answer(string bone)
        {
            if (bone[0] == '$')
            {
                IsRight = true;
                bone = bone.Substring(1);
            }
            body = bone;
        }

        public override string ToString()
        {
            return "\t" + body;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
