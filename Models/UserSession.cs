using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app2.Models
{
    public static class UserSession
    {
        public static int UserId { get; set; }
        public static List<PrimaryContact> PrimaryContacts { get; set; } = new();
    }
}
