using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app2.Models;

namespace app2.Services
{
    public interface ILocationService
    {
        Task<List<LocationResult>> SearchLocationsAsync(string query);
    }
}
