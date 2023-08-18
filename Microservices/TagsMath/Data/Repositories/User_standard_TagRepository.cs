using TagsMath.Data.Abstractions;
using TagsMath.Data.Entities;

namespace TagsMath.Data.Repositories
{
    internal class User_standard_TagRepository : IUser_standard_TagRepository
    {
        private readonly DataContext _context;

        public User_standard_TagRepository(DataContext context)
        {
            _context = context;
        }
        //-----------------------------------------
        public User_standard_Tag? GetUser_standard_TagByIds(int user_id, int tag_id)
        {
            return _context.Set<User_standard_Tag>().Where(t => t.user_id == user_id && t.tag_id == tag_id).FirstOrDefault();
        }
        public IQueryable<User_standard_Tag> GetAll()
        {
            return _context.Set<User_standard_Tag>().AsQueryable();
        }
        public IQueryable<User_standard_Tag> GetUser_standard_TagByUserId(int user_id)
        {
            return _context.Set<User_standard_Tag>().Where(t => t.user_id == user_id);
        }
        //-----------------------------------------
        public bool Add(User_standard_Tag newEntity)
        {
            var entity = _context.Set<User_standard_Tag>().Add(newEntity);
            return true;
        }
        //-----------------------------------------
        public void Remove(User_standard_Tag entity)
        {
            _context.Set<User_standard_Tag>().Remove(entity);
        }
        //-----------------------------------------
        public void Update(User_standard_Tag entity)
        {
            _context.Set<User_standard_Tag>().Update(entity);
        }
        //-----------------------------------------
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
