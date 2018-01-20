using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyaxTask.Models
{
    public interface IDataRepository
    {
        IEnumerable<Realtor> Realtors { get;  }
        IEnumerable<Department> Departments { get; }
        Realtor saveRealtor(Realtor value);
        void deleteRealtor(int id);
    }
}
