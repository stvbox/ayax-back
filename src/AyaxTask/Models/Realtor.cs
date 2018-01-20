using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyaxTask.Models
{
    public class Realtor
    {
        public int id { get; set; }
        public string guid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int DepId { get; set; }
        public DateTime RegDate { get; set; }
    }
}
