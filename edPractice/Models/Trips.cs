using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edPractice.Models
{
    public partial class Trips
    {
        public System.DateTime Trip_start { get; set; }
        public string dates
        {
            get
            {
                return Trip_start.ToShortDateString() + " - ";
            }

        }
        public System.DateTime Trip_end { get; set; }
        public string date
        {
            get
            {
                return Trip_end.ToShortDateString(); ;
            }

        }
    }
}

    
