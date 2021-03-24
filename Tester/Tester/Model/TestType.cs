using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.Model
{
    public enum TestType
    {
        [Description("Экзамен")]
        EXAM,
        [Description("Зачет")]
        OFFSET,
        [Description("Промежуточная аттестация")]
        INTERMEDIATE,
        [Description("Обычный")]
        SIMPLE
    }
}
