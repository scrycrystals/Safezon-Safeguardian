using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app2.Models
{
    public class PrimaryContact
    { 
            public int Id { get; set; }
            public int UserId { get; set; }  // Foreign key from Registration table
            public string ContactName { get; set; }
            public string ContactNumber { get; set; }
            public bool SingleTap { get; set; }
            public bool DoubleTap { get; set; }
            public bool Always { get; set; }

    }
}
