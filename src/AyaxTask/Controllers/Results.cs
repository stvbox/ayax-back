using AyaxTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyaxTask.Controllers
{
    public class GetListNav
    {
        public int fullSize { get; set; }
        public int page { get; set; }
        public int size { get; set; }
        public double pagesCount { get; set; }
    }

    public class GetListResponse
    {
        public GetListNav nav { get; set; }
        public IEnumerable<object> data { get; set; }
    }

    public class GetRealtorResponse
    {
        public GetListNav nav { get; set; }
        public Realtor data { get; set; }
    }

    public class PostRealtorResponse
    {
        public string state { get; set; }
        public Realtor data { get; set; }
        public string test { get; set; }
    }
}
