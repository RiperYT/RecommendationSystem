using Npgsql;
using Dapper;
using Recomendations.Common;
using Recomendations.Data.Abstractions;

namespace Recomendations.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly NpgsqlConnection connection;
        public PostRepository()
        {
            var connectionString = ConnectionRules.PostgreConncection;
            connection = new NpgsqlConnection(connectionString);
        }      

        public List<int> GetUserPostsIDsSubscrription(int user_id)
        {
            var sql = @"SELECT id
            FROM ""Post""
            WHERE (id IN (SELECT post_id
                          FROM ""Post_owner_User""
                          WHERE user_id IN (SELECT user_subscribe_id
                                            FROM ""Subscription_user""
                                            WHERE user_id = @user_id))
                  OR id IN (SELECT post_id
                            FROM ""Post_owner_Startup""
                            WHERE startup_id IN (SELECT startup_id
                                                 FROM ""Subscription_Startup""
                                                 WHERE user_id = @user_id)))
            AND id NOT IN (SELECT post_id
                           FROM ""User_history_Post""
                           WHERE user_id = @user_id)
            AND date_posting >= (CURRENT_DATE - INTERVAL '1 month')
            ORDER BY date_posting DESC";

            return connection.Query<int>(sql, new { user_id = user_id }).ToList();
        }

        public List<int> GetStandardTagsOfPost(int post_id)
        {
            var sql = @"SELECT tag_id FROM ""Post_standard_Tag"" WHERE post_id = @post_id";

            return connection.Query<int>(sql, new { post_id = post_id }).ToList();
        }

        public Dictionary<int, int> GetStatTagsOfPost(int post_id)
        {
            var tags = new Dictionary<int, int>();

            connection.Open();
            using var command = new NpgsqlCommand("SELECT tag_id, tag_count FROM \"Post_stat_Tag\" WHERE post_id = @post_id", connection);
            command.Parameters.AddWithValue("post_id", post_id);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                tags.Add(reader.GetInt32(0), reader.GetInt32(1));
            }
            connection.Close();

            return tags;
        }

        public int GetPercentInterestingInOwning(int post_id, int user_id)
        {
            var sql_shows = @"SELECT count(*)
                        FROM ""User_history_Post""
                        WHERE (post_id IN (SELECT post_id
                                          FROM ""Post_owner_Startup""
                                          WHERE startup_id = (SELECT startup_id
                                                              FROM ""Post_owner_Startup""
                                                              WHERE post_id = @post_id))
                                OR post_id IN (SELECT post_id
                                               FROM ""Post_owner_User""
                                               Where user_id = (SELECT user_id
                                                                FROM ""Post_owner_User""
                                                                WHERE post_id = @post_id)))
                                AND user_id = @user_id";

            var sql_likes = @"SELECT count(*)
                              FROM ""Like""
                              WHERE (post_id IN (SELECT post_id
                                                FROM ""Post_owner_Startup""
                                                WHERE startup_id = (SELECT startup_id
                                                                    FROM ""Post_owner_Startup""
                                                                    WHERE post_id = @post_id))
                                      OR post_id IN (SELECT post_id
                                                     FROM ""Post_owner_User""
                                                     Where user_id = (SELECT user_id
                                                                      FROM ""Post_owner_User""
                                                                      WHERE post_id = @post_id)))
                                      AND user_id = @user_id
                                      AND is_active = true";

            var shows = connection.QueryFirst<int>(sql_shows, new { post_id = post_id, user_id = user_id });
            var likes = connection.QueryFirst<int>(sql_likes, new { post_id = post_id, user_id = user_id });

            return (int)Math.Round((float)likes / shows * 100);
        }
    }
}
