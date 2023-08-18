using Npgsql;
using Dapper;
using Recomendation.Common;
using Recomendation.Data.Abstractions;

namespace Recomendation.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NpgsqlConnection connection;
        public UserRepository()
        {
            var connectionString = ConnectionRules.PostgreConncection;
            connection = new NpgsqlConnection(connectionString);
        }

        public List<int> GetStandardTagsOfUser(int user_id)
        {
            var sql = @"SELECT tag_id FROM ""User_standard_Tag"" WHERE user_id = @user_id";

            return connection.Query<int>(sql, new { user_id = user_id }).ToList();
        }

        public Dictionary<int, int> GetStatTagsOfUser(int user_id)
        {
            var tags = new Dictionary<int, int>();

            connection.Open();
            using var command = new NpgsqlCommand("SELECT tag_id, tag_count FROM \"User_stat_Tag\" WHERE user_id = @user_id", connection);
            command.Parameters.AddWithValue("user_id", user_id);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                tags.Add(reader.GetInt32(0), reader.GetInt32(1));
            }
            connection.Close();

            return tags;
        }
    }
}
