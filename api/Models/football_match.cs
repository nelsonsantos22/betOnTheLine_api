using System.Data;
using System.Threading.Tasks;
using MySqlConnector;

namespace api.Models
{
    public class football_match
    {

         public int Id { get; set; }

        public string home_team { get; set; }

        public string away_team { get; set; }

        internal AppDb Db { get; set; }

        public football_match()
        {
        }

        internal football_match(AppDb db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `football_match` (`home_team`, `away_team`) VALUES (@home_team, @away_team);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int) cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `football_match` SET `home_team` = @home_team, `away_team` = @away_team  WHERE `Id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `football_match` WHERE `Id` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@home_team",
                DbType = DbType.String,
                Value = home_team,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@away_team",
                DbType = DbType.String,
                Value = away_team,
            });
        }
    }
}