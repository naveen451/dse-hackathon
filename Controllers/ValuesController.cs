using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSEHackatthon.services.mocks;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //Go365ChallengerService service = new Go365ChallengerService();
            //var userChallenges = service.GetUserChallenges("123");
            return new string[] { "value1", "value2","value3","value4" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
        }
    }
}
