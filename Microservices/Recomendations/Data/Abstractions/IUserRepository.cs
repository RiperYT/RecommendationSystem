using Npgsql;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Recomendations.Data.Abstractions
{
    public interface IUserRepository
    {
        public List<int> GetStandardTagsOfUser(int user_id);
        public Dictionary<int, int> GetStatTagsOfUser(int user_id);
        public bool Authorize(int user_id, string password);
    }
}
