using System.Data;
using System.Threading.Tasks;
using MySqlConnector;


namespace api.Models
{
    public class Tip
    {

         public int Id { get; set; }

        public int tip { get; set; }

        public string message { get; set; }

        public int userId { get; set; }

        public int gameId { get; set; }

        //public Person person { get; set; }

        //public football_match football_Match { get; set; }

        internal AppDb Db { get; set; }

        public Tip()
        {
        }

        internal Tip(AppDb db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `tip` (`tip`, `message`,`userId`, `gameId`) VALUES (@tip, @message,@userId,@gameId);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int) cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `tip` SET `tip` = @tip, `message` = @message, `userId` = @userId, `gameId` = @gameId WHERE `Id` = @Id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `tip` WHERE `Id` = @Id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@tip",
                DbType = DbType.Int32,
                Value = tip,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@message",
                DbType = DbType.String,
                Value = message,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@userId",
                DbType = DbType.Int32,
                Value = userId,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@gameId",
                DbType = DbType.Int32,
                Value = gameId,
            });
        }
    }
}