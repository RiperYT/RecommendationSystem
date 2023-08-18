using TagLikeMath.Data.Entities;

namespace TagLikeMath.Data.Abstractions
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
