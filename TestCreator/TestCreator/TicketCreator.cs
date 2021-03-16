using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCreator
{
    class TicketCreator
    {
        private List<Question> Questions;

        public TicketCreator(List<Question> questions)
        {
            Questions = questions;
        }

        public List<Question> CreateTicket(int countOfQuestion, int startIndex, int endIndex)
        {
            Random random = new Random();
            List<Question> ticket = new List<Question>();

            for (int i = 0; i < countOfQuestion; i++)
                ticket.Add(Questions[random.Next(startIndex, endIndex)]);
            return ticket;
        }
    }
}
