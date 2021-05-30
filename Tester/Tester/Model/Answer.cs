using System;

namespace Tester.Model
{
    [Serializable]
    public class Answer : NotifyPropertyChanged
    {
        private string _body;
        public string Body
        {
            get { return _body; }
            set
            {
                _body = value;
                OnPropertyChanged("Body");
            }
        }

        private bool _isRight;
        public bool IsRight
        {
            get { return _isRight; }
            set
            {
                _isRight = value;
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
            Body = bone;
        }

        public override string ToString()
        {
            return "\t" + Body;
        }
    }
}
