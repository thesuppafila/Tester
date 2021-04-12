using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.Model
{
    public interface IQuestion
    {
        object Clone();
               
        bool IsValid();
    }
}
