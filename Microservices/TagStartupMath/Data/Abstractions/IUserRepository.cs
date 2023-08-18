using TagStartupMath.Data.Entities;

namespace TagStartupMath.Data.Abstractions
{
    internal interface IUserRepository
    {
        //-----------------------------------------
        public User? GetUserById(int id);
        public IQueryable<User> GetAll();
        //-----------------------------------------
        public bool Add(User newEntity);
        //-----------------------------------------
        public void Remove(User entity);
        //-----------------------------------------
        public void Update(User entity);
        //-----------------------------------------
        public int SaveChanges();
    }
}
