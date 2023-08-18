using Dapper;
using Npgsql;
using Recomendations.Common;
using Recomendations.Data.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Recomendations.Data.Repositories
{
    public class StartupRepository : IStartupRepository
    {
        private readonly NpgsqlConnection connection;
        public StartupRepository()
        {
            var connectionString = ConnectionRules.PostgreConncection;
            connection = new NpgsqlConnection(connectionString);
        }
        public List<int> GetStartupIDsRecomandationByUserID(int user_id, int limit)
        {
            var sql = @"WITH user_stat_tags AS(
                            WITH user_tags AS (
                            SELECT tag_id, ROUND(tag_count * @notStandartValue) AS tag_count
                            FROM ""User_stat_Tag""
                            WHERE user_id = @user_id
                        ),
                        user_standard_tags AS (
                                SELECT tag_id, @standartValue AS tag_count
                                FROM ""User_standard_Tag""
                                WHERE user_id = @user_id
                            )
                            SELECT
                               COALESCE(user_tags.tag_id, user_standard_tags.tag_id) AS tag_id,
                               COALESCE(user_tags.tag_count, 0) + COALESCE(user_standard_tags.tag_count, 0) AS tag_count
                            FROM user_tags
                            FULL OUTER JOIN user_standard_tags ON user_tags.tag_id = user_standard_tags.tag_id
                        ),
                        startup_tags AS (
                            SELECT
                                s.id AS startup_id,
                                t.id AS tag_id,
                                COALESCE(st.tag_count, 0) AS tag_count
                            FROM ""Startup"" s
                            CROSS JOIN ""Tag"" t
                            LEFT JOIN ""Startup_stat_Tag"" st ON st.startup_id = s.id AND st.tag_id = t.id
                        ),
                        startup_standard_specialists AS (
                            SELECT startup_id, specialist_id
                            FROM ""Searching_Specialists""
                        ),
                        startup_standard1_specialists AS (
                            SELECT startup_id AS startup_id, sp.specialist_standard_id AS specialist_id
                            FROM startup_standard_specialists ss
                            JOIN ""Specialist"" AS sp ON sp.id = ss.specialist_id
                            WHERE sp.specialist_standard_id IS NOT NULL
                        ),
                        startup_standard2_specialists AS (
                            SELECT startup_id AS startup_id, sp.id AS specialist_id
                            FROM startup_standard1_specialists ss
                            JOIN ""Specialist"" AS sp ON sp.specialist_standard_id = ss.specialist_id
                            WHERE sp.specialist_standard_id IS NOT NULL
                        ),
                        startup_specialits AS (
                            SELECT startup_id, specialist_id
                            FROM startup_standard_specialists
                            UNION
                            (SELECT startup_id, specialist_id
                            FROM startup_standard1_specialists
                            UNION
                            SELECT startup_id, specialist_id
                            FROM startup_standard2_specialists)
                        ),
                        user_standard_specialists AS (
                            SELECT user_id, specialist_id
                            FROM ""User_Specialists""
                            WHERE user_id = @user_id
                        ),
                        user_standard1_specialists AS (
                            SELECT user_id AS user_id, sp.specialist_standard_id AS specialist_id
                            FROM user_standard_specialists ss
                            JOIN ""Specialist"" AS sp ON sp.id = ss.specialist_id
                            WHERE sp.specialist_standard_id IS NOT NULL
                        ),
                        user_standard2_specialists AS (
                            SELECT user_id AS user_id, sp.id AS specialist_id
                            FROM user_standard1_specialists ss
                            JOIN ""Specialist"" AS sp ON sp.specialist_standard_id = ss.specialist_id
                            WHERE sp.specialist_standard_id IS NOT NULL
                        ),
                        user_specialists AS (
                            SELECT user_id, specialist_id
                            FROM user_standard_specialists
                            UNION
                            (SELECT user_id, specialist_id
                            FROM user_standard1_specialists
                            UNION
                            SELECT user_id, specialist_id
                            FROM user_standard2_specialists)
                        ),
                        startup_language AS (
                            SELECT startup_id
                            FROM ""Startup_Languages"" ul
                            JOIN ""User_Languages"" sl ON sl.language_id = ul.language_id
                            WHERE user_id = @user_id
                        )
                    SELECT
                      st.startup_id
                    FROM startup_tags st
                    LEFT JOIN user_stat_tags ut ON ut.tag_id = st.tag_id
                    JOIN startup_language sl on sl.startup_id = st.startup_id
                    JOIN ""Startup"" sta ON st.startup_id = sta.id WHERE sta.can_apply = true AND st.startup_id IN (SELECT DISTINCT ss.startup_id
                                                                                                                  FROM user_specialists us
                                                                                                                  JOIN startup_specialits AS ss ON us.specialist_id = ss.specialist_id
                        )
                    GROUP BY st.startup_id
                    ORDER BY SUM(ABS(st.tag_count - ut.tag_count))
                    LIMIT @limit";

            return connection.Query<int>(sql, new { user_id = user_id,
                                                    limit = limit,
                                                    standartValue =  TagStartupMathRules.PercentOfStandartStartup,
                                                    notStandartValue = (float)TagStartupMathRules.PercentOfStandartStartup / 100}).ToList();
        }
        public List<int> GetAllUserIDsRecomandationByStartupID(int startup_id, int limit)
        {
            var sql = @"WITH user_stat_tags AS(
                                WITH user_tags AS (
                                    SELECT user_id, tag_id, ROUND(tag_count * @notStandartValue) AS tag_count
                                    FROM ""User_stat_Tag""
                                ),
                                user_standard_tags AS (
                                    SELECT user_id, tag_id, @standartValue AS tag_count
                                    FROM ""User_standard_Tag""
                                )
                                SELECT
                                    COALESCE(user_tags.user_id, user_standard_tags.user_id) AS user_id,
                                    COALESCE(user_tags.tag_id, user_standard_tags.tag_id) AS tag_id,
                                    COALESCE(user_tags.tag_count, 0) + COALESCE(user_standard_tags.tag_count, 0) AS tag_count
                                FROM user_tags
                                FULL OUTER JOIN user_standard_tags ON user_tags.tag_id = user_standard_tags.tag_id AND user_tags.user_id = user_standard_tags.user_id
                            ),
                            startup_tags AS (
                                SELECT
                                    s.id AS startup_id,
                                    t.id AS tag_id,
                                    COALESCE(st.tag_count, 0) AS tag_count
                                FROM ""Startup"" s
                                CROSS JOIN ""Tag"" t
                                LEFT JOIN ""Startup_stat_Tag"" st ON st.startup_id = s.id AND st.tag_id = t.id
                                WHERE startup_id = @startup_id
                            ),
                            user_standard_specialists AS (
                                SELECT user_id, specialist_id
                                FROM ""User_Specialists""
                            ),
                            user_standard1_specialists AS (
                                SELECT user_id AS user_id, sp.specialist_standard_id AS specialist_id
                                FROM user_standard_specialists ss
                                JOIN ""Specialist"" AS sp ON sp.id = ss.specialist_id
                                WHERE sp.specialist_standard_id IS NOT NULL
                            ),
                            user_standard2_specialists AS (
                                SELECT user_id AS user_id, sp.id AS specialist_id
                                FROM user_standard1_specialists ss
                                JOIN ""Specialist"" AS sp ON sp.specialist_standard_id = ss.specialist_id
                                WHERE sp.specialist_standard_id IS NOT NULL
                            ),
                            user_specialits AS (
                                SELECT user_id, specialist_id
                                FROM user_standard_specialists
                                UNION
                                (SELECT user_id, specialist_id
                                FROM user_standard1_specialists
                                UNION
                                SELECT user_id, specialist_id
                                FROM user_standard2_specialists)
                            ),
                            startup_specialists AS (
                                SELECT DISTINCT s.id AS specialist_id
                                FROM ""Specialist"" AS s
                                LEFT JOIN ""Searching_Specialists"" AS us ON s.id = us.specialist_id
                                WHERE us.startup_id = @startup_id OR s.id IN (
                                    SELECT s2.specialist_standard_id
                                    FROM ""User_Specialists"" AS us2
                                    JOIN ""Specialist"" AS s2 ON us2.specialist_id = s2.id
                                    WHERE us2.user_id = 3 AND s2.specialist_standard_id IS NOT NULL
                                )
                            ),
                        user_language AS (
                            SELECT user_id
                            FROM ""User_Languages"" ul
                            JOIN ""Startup_Languages"" sl ON sl.language_id = ul.language_id
                            WHERE startup_id = @startup_id
                        )
                        SELECT
                            ut.user_id
                        FROM user_stat_tags ut
                        LEFT JOIN startup_tags st ON ut.tag_id = st.tag_id
                        JOIN user_language ul on ul.user_id = ut.user_id
                        JOIN ""User"" sta ON ut.user_id = sta.id WHERE ut.user_id IN (SELECT DISTINCT ss.user_id
                                                                                    FROM startup_specialists us
                                                                                    JOIN user_specialits AS ss ON us.specialist_id = ss.specialist_id
                            )
                        GROUP BY ut.user_id
                        ORDER BY SUM(ABS(ut.tag_count - st.tag_count))
                        LIMIT @limit";

            return connection.Query<int>(sql, new
            {
                startup_id = startup_id,
                limit = limit,
                standartValue = TagStartupMathRules.PercentOfStandartStartup,
                notStandartValue = (float)TagStartupMathRules.PercentOfStandartStartup / 100
            }).ToList();
        }

        public List<int> GeUserIDsRecomandationByStartupIDSpecialIDs(int startup_id, int specialistID, int limit)
        {
            var sql = @"WITH user_stat_tags AS(
                                WITH user_tags AS (
                                    SELECT user_id, tag_id, ROUND(tag_count * @notStandartValue) AS tag_count
                                    FROM ""User_stat_Tag""
                                ),
                                user_standard_tags AS (
                                    SELECT user_id, tag_id, @standartValue AS tag_count
                                    FROM ""User_standard_Tag""
                                )
                                SELECT
                                    COALESCE(user_tags.user_id, user_standard_tags.user_id) AS user_id,
                                    COALESCE(user_tags.tag_id, user_standard_tags.tag_id) AS tag_id,
                                    COALESCE(user_tags.tag_count, 0) + COALESCE(user_standard_tags.tag_count, 0) AS tag_count
                                FROM user_tags
                                FULL OUTER JOIN user_standard_tags ON user_tags.tag_id = user_standard_tags.tag_id AND user_tags.user_id = user_standard_tags.user_id
                        ),
                        startup_tags AS (
                            SELECT
                                s.id AS startup_id,
                                t.id AS tag_id,
                                COALESCE(st.tag_count, 0) AS tag_count
                            FROM ""Startup"" s
                            CROSS JOIN ""Tag"" t
                            LEFT JOIN ""Startup_stat_Tag"" st ON st.startup_id = s.id AND st.tag_id = t.id
                            WHERE startup_id = @startup_id
                        ),
                        user_standard_specialists AS (
                            SELECT user_id
                            FROM ""User_Specialists""
                            WHERE specialist_id IN (SELECT id
                                                    FROM ""Specialist""
                                                    WHERE id = @specialistID OR specialist_standard_id = @specialistID
                                                    UNION
                                                    SELECT id
                                                    FROM ""Specialist""
                                                    WHERE specialist_standard_id IN (SELECT id
                                                                                    FROM ""Specialist""
                                                                                    WHERE id = @specialistID OR specialist_standard_id = @specialistID
                                                                                    ))
                        ),
                        user_language AS (
                            SELECT user_id
                            FROM ""User_Languages"" ul
                            JOIN ""Startup_Languages"" sl ON sl.language_id = ul.language_id
                            WHERE startup_id = 1
                        )
                        SELECT
                            ut.user_id
                        FROM user_stat_tags ut
                        LEFT JOIN startup_tags st ON ut.tag_id = st.tag_id
                        JOIN ""User"" sta ON ut.user_id = sta.id
                        JOIN user_standard_specialists uss on uss.user_id = ut.user_id
                        JOIN user_language ul on ul.user_id = ut.user_id
                        GROUP BY ut.user_id
                        ORDER BY SUM(ABS(ut.tag_count - st.tag_count))
                        LIMIT @limit";

            return connection.Query<int>(sql, new
            {
                startup_id = startup_id,
                limit = limit,
                specialistID = specialistID,
                standartValue = TagStartupMathRules.PercentOfStandartStartup,
                notStandartValue = (float)TagStartupMathRules.PercentOfStandartStartup / 100
            }).ToList();
        }
    }
}
