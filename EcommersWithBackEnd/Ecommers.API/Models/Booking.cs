using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommers.API.Models
{
    public class Booking
    {
        public int Id {get; set; }

        public string UserName {get; set; } = "";

        public int Status {get; set; }
    }
}