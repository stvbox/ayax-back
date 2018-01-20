using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyaxTask.Models
{

    public class FakeDataRepository : IDataRepository
    {
        /*IEnumerable<Realtor> dataRealtors;*/
        List<Realtor> dataRealtors;

        public FakeDataRepository()
        {
            this.dataRealtors = generateRealtorsList().ToList<Realtor>();
        }

        public IEnumerable<Department> Departments
        {
            get
            {
                var dateTime = DateTime.Today;
                for (var i = 1; i < 11; i++)
                {
                    var itemDate = dateTime.AddHours(-12 * i).AddMinutes(-18);
                    yield return new Department { id = i, Name = $"Подразделение {i}", RegDate = itemDate };
                }
            }
        }

        public Realtor saveRealtor(Realtor realtor)
        {
            // Регистрация нового реэлтора
            if (realtor.id == 0 || realtor.id == null)
            {
                var maxId = (from rlt in dataRealtors select rlt.id).Max();
                dataRealtors.Add(realtor);
                realtor.id = maxId + 1;
                realtor.RegDate = DateTime.Today;
                realtor.guid = Guid.NewGuid().ToString();

                return realtor;
            }

            // Сохранение изменений реэлтора
            var repoRealtor = GetRealtorByID(realtor.id);
            repoRealtor.Name = realtor.Name;
            repoRealtor.Surname = realtor.Surname;
            repoRealtor.DepId = realtor.DepId;

            return repoRealtor;
        }

        private Realtor GetRealtorByID(int id)
        {
            var item = (from rlt in dataRealtors
                       where rlt.id == id
                       select rlt).First();
            return item;
        }

        public IEnumerable<Realtor> Realtors
        {
            get
            {
                return this.dataRealtors;
            }
        }

        private IEnumerable<Realtor> generateRealtorsList()
        {
            var Names = new List<String> { "Вася", "Петя", "Гена", "Семён", "Григорий", "Виталий", "Костя", "Миша", "Сергей" };
            var Surnames = new List<String> { "Петров", "Иванов", "Сидоров", "Пупкин", "Дудкин", "Эникеев" };

            Random rnd = new Random();
            var dateTime = DateTime.Today;
            for (var i = 1; i < 30; i++)
            {
                string name = (from lname in Names select lname).OrderBy(x => Guid.NewGuid()).Take(1).First();
                string surname = (from lsurname in Surnames select lsurname).OrderBy(x => Guid.NewGuid()).Take(1).First();

                var depId = rnd.Next(1, 10);
                var itemDate = dateTime.AddHours(-12 * i).AddMinutes(-18);
                yield return new Realtor { id = i, guid = Guid.NewGuid().ToString(), Name = name, Surname = surname, DepId = depId, RegDate = itemDate };
            }
        }

        public void deleteRealtor(int id)
        {
            var realtor = (from rlt in dataRealtors where rlt.id == id select rlt).First();
            this.dataRealtors.Remove(realtor);
        }
    }
}
