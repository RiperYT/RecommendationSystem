using TagsMath.Data.Abstractions;
using TagsMath.Data.Entities;

namespace TagsMath.Data.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        //-----------------------------------------
        public User? GetUserById(int id)
        {
            return _context.Set<User>().Where(t => t.id == id).FirstOrDefault();
        }
        public IQueryable<User> GetAll()
        {
            return _context.Set<User>().AsQueryable();
        }
        //-----------------------------------------
        public bool Add(User newEntity)
        {
            var entity = _context.Set<User>().Add(newEntity);
            return true;
        }
        //-----------------------------------------
        public void Remove(User entity)
        {
            _context.Set<User>().Remove(entity);
        }
        //-----------------------------------------
        public void Update(User entity)
        {
            _context.Set<User>().Update(entity);
        }
        //-----------------------------------------
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
