using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCreator
{
    class Ticket
    {
        private List<Question> Body;

        private List<Question> GetTicket()
        {
            return Body;
        }
    }
}
