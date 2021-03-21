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

        public Ticket CreateTicket(int count, int startIndex, int endIndex)
        {
            return new Ticket(Questions, count, startIndex, endIndex);
        }


    }
}
