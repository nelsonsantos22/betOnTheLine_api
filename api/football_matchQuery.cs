using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;
using api.Models;

namespace api
{
    public class football_matchQuery
    {
        public AppDb Db { get; }

        public football_matchQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<football_match> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT  `Id`, `home_team`, `away_team` FROM `football_match` WHERE `Id` = @id";
            //cmd.CommandText = @"SELECT `Id`, `Title`, `Content` FROM `person` WHERE `Id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<football_match>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `home_team`, `away_team`  FROM `football_match` ORDER BY `Id` DESC LIMIT 10;";
            //cmd.CommandText = @"SELECT `Id`, `Title`, `Content` FROM `person` ORDER BY `Id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `football_match`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<football_match>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<football_match>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new football_match(Db)
                    {
                        Id = reader.GetInt32(0),
                        home_team = reader.GetString(1),
                        away_team = reader.GetString(2),
                        
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}