using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Models;


namespace api.Controllers
{
    [Route("api/[controller]")]
    public class Football_matchController : ControllerBase
    {
        public Football_matchController(AppDb db)
        {
            Db = db;
        }

        // GET api/football_match
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new football_matchQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/football_match/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new football_matchQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/football_match
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]football_match body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT api/football_match/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]football_match body)
        {
            await Db.Connection.OpenAsync();
            var query = new football_matchQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                 return new NotFoundResult();
            result.home_team = body.home_team;
            result.away_team = body.away_team;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/football_match/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new football_matchQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }

        // DELETE api/football_match
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await Db.Connection.OpenAsync();
            var query = new football_matchQuery(Db);
            await query.DeleteAllAsync();
            return new OkResult();
        }


        // GET api/football_match/5/tip
        [HttpGet("{id}/tip")]
        public async Task<IActionResult> GetUserTip(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new TipQuery(Db);
            var result = await query.FindFootballMatchTipAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        public AppDb Db { get; }
    }
}