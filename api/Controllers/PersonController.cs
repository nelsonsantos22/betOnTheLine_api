using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Models;


namespace api.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        public PersonController(AppDb db)
        {
            Db = db;
        }

        // GET api/person
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new PersonQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/person/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new PersonQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/person
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Person body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT api/person/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]Person body)
        {
            await Db.Connection.OpenAsync();
            var query = new PersonQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.firstName = body.firstName;
            result.lastName = body.lastName;
            result.username = body.username;
            result.email = body.email;
            result.password = body.password;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/person/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new PersonQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }

        // DELETE api/person
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await Db.Connection.OpenAsync();
            var query = new PersonQuery(Db);
            await query.DeleteAllAsync();
            return new OkResult();
        }


        // GET api/person/5/tip
        [HttpGet("{id}/tip")]
        public async Task<IActionResult> GetUserTip(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new TipQuery(Db);
            var result = await query.FindUserTipAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        public AppDb Db { get; }
    }
}