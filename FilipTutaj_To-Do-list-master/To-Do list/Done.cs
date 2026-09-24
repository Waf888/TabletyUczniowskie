using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_list
{
    public class Done
    {
        public string? D_Name { get; set; }
        public string? D_Desc { get; set; }
        public string? D_Number { get; set; }
        public DateTime D_Start { get; set; }
        public DateTime D_End { get; set; }

        public string D_StartText => D_Start.ToString("dd.MM.yyyy HH:mm");
        public string D_EndText => D_End.ToString("dd.MM.yyyy HH:mm");
    }
}