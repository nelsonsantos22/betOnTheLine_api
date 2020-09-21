using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;
using api.Models;

namespace api
{
    public class PersonQuery
    {
        public AppDb Db { get; }

        public PersonQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<Person> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT  `Id`, `firstname`, `lastname`,`username`,`email`, `password` FROM `person` WHERE `Id` = @id";
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

        public async Task<List<Person>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `firstname`, `lastname`,`username`,`email`, `password`   FROM `person` ORDER BY `Id` DESC LIMIT 10;";
            //cmd.CommandText = @"SELECT `Id`, `Title`, `Content` FROM `person` ORDER BY `Id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `person`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<Person>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Person>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Person(Db)
                    {
                        Id = reader.GetInt32(0),
                        firstName = reader.GetString(1),
                        lastName = reader.GetString(2),
                        username = reader.GetString(3),
                        email = reader.GetString(4),
                        password = reader.GetString(5),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}