using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AyaxTask.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Cors;
using System.Net.Http;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AyaxTask.Controllers
{
    [Route("api/[controller]")]
    public class RealtorsController : Controller
    {
        protected IDataRepository repository;

        public RealtorsController(IDataRepository repo, ILoggerFactory logger) {
            repository = repo;
        }

        int compareDates(DateTime d1, DateTime d2)
        {

            return d1.CompareTo(d2);
        }

        // GET: api/values
        [HttpGet]
        public GetListResponse Get()
        {
            var size = UInt16.Parse(HttpContext.Request.Query["size"]);
            var page = UInt16.Parse(HttpContext.Request.Query["page"]);
            string surn = HttpContext.Request.Query?["fsm"];
            string datefr = HttpContext.Request.Query?["datefr"];
            string dateto = HttpContext.Request.Query?["dateto"];

            var filteredResult = from realtor in repository.Realtors
                                 where (surn==null||surn.Equals("")||realtor.Surname.IndexOf(surn) > -1)
                                 select realtor;

            if (datefr != null) {
                DateTime dateFr = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddMilliseconds(double.Parse(datefr));
                dateFr = new DateTime(dateFr.Year, dateFr.Month, dateFr.Day, 0, 0, 0);
                DateTime dateTo = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddMilliseconds(double.Parse(dateto));
                dateTo = new DateTime(dateTo.Year, dateTo.Month, dateTo.Day, 23, 59, 59, 999);

                filteredResult = from realtor in filteredResult
                                 where compareDates(realtor.RegDate, dateFr) > 0 && compareDates(realtor.RegDate, dateTo) < 0
                                 select realtor;
            }



            var first = size * (page - 1);
            var fullSize = filteredResult.Count();
            var pagesCount = Math.Ceiling((float)fullSize / (float)size);

            var navObj = new GetListNav {
                fullSize = fullSize,
                pagesCount = pagesCount,
                page = page,
                size = size
            };

            var result = new GetListResponse {
                nav = navObj,
                data = filteredResult.Skip(first).Take(size)
            };

            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public GetRealtorResponse Get(int id)
        {
            Realtor realtor = null;
            if (id != null && id > 0)
                realtor = (from rlt in repository.Realtors where (rlt.id == id) select rlt).First();
            else
                realtor = new Realtor();

            return new GetRealtorResponse { data = realtor };
        }

        // POST api/values
        [HttpPost]
        public PostRealtorResponse Post([FromForm]Realtor value)
        {
            Realtor value1 = repository.saveRealtor(value);

            return new PostRealtorResponse {
                state = "ok",
                data = value1,
                test = $"id {value.id} Name {value.Name} depId {value.DepId}"
            };
        }


        [HttpOptions]
        public string Options() {
            this.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            this.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "*");
            this.HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");
            return "options";
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            repository.deleteRealtor(id);
        }

    }
}
