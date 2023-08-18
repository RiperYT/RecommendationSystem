using TagsMath.Data.Abstractions;
using TagsMath.Data.Entities;

namespace TagsMath.Data.Repositories
{
    internal class User_stat_TagRepository : IUser_stat_TagRepository
    {
        private readonly DataContext _context;

        public User_stat_TagRepository(DataContext context)
        {
            _context = context;
        }
        //-----------------------------------------
        public User_stat_Tag? GetUser_stat_TagByIds(int user_id, int tag_id)
        {
            return _context.Set<User_stat_Tag>().Where(t => t.user_id == user_id && t.tag_id == tag_id).First();
        }

        public IQueryable<User_stat_Tag> GetAll()
        {
            return _context.Set<User_stat_Tag>().AsQueryable();
        }
        public IQueryable<User_stat_Tag> GetUser_stat_TagByUserId(int user_id)
        {
            return _context.Set<User_stat_Tag>().Where(t => t.user_id == user_id);
        }
        //-----------------------------------------
        public bool Add(User_stat_Tag newEntity)
        {
            var entity = _context.Set<User_stat_Tag>().Add(newEntity);
            SaveChanges();
            return true;
        }
        //-----------------------------------------
        public void Remove(User_stat_Tag entity)
        {
            _context.Set<User_stat_Tag>().Remove(entity);
            SaveChanges();
        }
        //-----------------------------------------
        public void Update(User_stat_Tag entity)
        {
            _context.Set<User_stat_Tag>().Update(entity);
            SaveChanges();
        }
        //-----------------------------------------
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
