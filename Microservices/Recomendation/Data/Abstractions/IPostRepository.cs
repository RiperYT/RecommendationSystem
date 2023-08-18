using Npgsql;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Recomendation.Data.Abstractions
{
    public interface IPostRepository
    {
        public List<int> GetUserPostsIDsSubscrription(int user_id);
        public List<int> GetStandardTagsOfPost(int post_id);
        public Dictionary<int, int> GetStatTagsOfPost(int post_id);
        public int GetPercentInterestingInOwning(int post_id, int user_id);
    }
}
