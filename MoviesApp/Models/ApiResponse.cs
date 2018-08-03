using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesApp.Models
{
    public class ApiResponse
    {
        public List<Hotel> hotels { get; set; }

        public Status Status { get; set; }
    }
}