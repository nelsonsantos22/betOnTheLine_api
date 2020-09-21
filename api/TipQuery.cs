using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;
using api.Models;

namespace api
{
    public class TipQuery
    {
        public AppDb Db { get; }

        public TipQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<Tip> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT  `Id`, `tip`, `message`, `userId`, `gameId` WHERE `Id` = @id";
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

        public async Task<List<Tip>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `tip`, `message`,`userId`, `gameId`  FROM `tip` ORDER BY `Id` DESC LIMIT 10;";
            //cmd.CommandText = @"SELECT `Id`, `Title`, `Content` FROM `person` ORDER BY `Id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `tip`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }


        //return all tips from a specific user
        public async Task<List<Tip>> FindUserTipAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT  * FROM `tip` WHERE `userId` = @userId";
            //cmd.CommandText = @"SELECT `Id`, `Title`, `Content` FROM `person` WHERE `Id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@userId",
                DbType = DbType.Int32,
                Value = id,
            });

            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result;
        }

        private async Task<List<Tip>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Tip>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Tip(Db)
                    {
                        Id = reader.GetInt32(0),
                        tip = reader.GetInt32(1),
                        message = reader.GetString(2),
                        gameId = reader.GetInt32(3),
                        userId = reader.GetInt32(4),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}