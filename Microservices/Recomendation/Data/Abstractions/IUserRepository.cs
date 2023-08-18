using Npgsql;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Recomendation.Data.Abstractions
{
    public interface IUserRepository
    {
        public List<int> GetStandardTagsOfUser(int user_id);
        public Dictionary<int, int> GetStatTagsOfUser(int user_id);
    }
}
