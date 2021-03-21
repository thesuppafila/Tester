using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCreator
{
    class TicketCreator
    {
        static Random random = new Random();

        private List<Question> Questions;

        public TicketCreator(List<Question> questions)
        {
            Questions = questions;
        }

        public Ticket CreateTicket(int count, int startIndex, int endIndex) //возвращает билет
        {
            Ticket ticket = new Ticket();
            List<Question> currentQuestions = CreateQuestions(count, startIndex, endIndex);
            ticket.SetQuestions(currentQuestions);
            ticket.SetKey(CreateKey(currentQuestions));

            return ticket;
        }

        public List<Ticket> CreateTickets(int ticketCount, int questionCount, int startIndex, int endIndex) //возвращает коллекцию билетов
        {
            List<Ticket> tickets = new List<Ticket>();
            for (int i = 0; i < ticketCount; i++)
                tickets.Add(CreateTicket(questionCount, startIndex, endIndex));
            return tickets;
        }

        public List<Question> CreateQuestions(int count, int startIndex, int endIndex)
        {
            List<Question> questions = new List<Question>();
            List<int> usedQuestions = new List<int>();
            int index = 0;
            while (index < count)
            {
                int j = random.Next(startIndex, endIndex);
                if (!usedQuestions.Contains(j))
                {
                    usedQuestions.Add(j);

                    questions.Add(Questions[j]);
                    index++;
                }
                else continue;
            }

            foreach (Question q in questions)
                foreach (Answer a in q.GetAnswer())
                    a.SetCode(random.Next(100).ToString());

            return questions;
        }

        public List<string> CreateKey(List<Question> questions)
        {
            List<string> key = new List<string>();
            int index = 0;
            foreach (Question q in questions)
                key.Add(String.Format("{0}:{1}", (index+1).ToString(), q.GetTrueAnswer()));
            return key;
        }
    }
}
