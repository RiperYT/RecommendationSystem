using TagStartupMath.Data.Abstractions;
using TagStartupMath.Data.Entities;

namespace TagStartupMath.Data.Repositories
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
            return _context.Set<User_stat_Tag>().Where(t => t.user_id == user_id && t.tag_id == tag_id).FirstOrDefault();
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
            var entity = _context.User_stat_Tag.Add(newEntity);
            Console.WriteLine(entity.Entity.ToString());
            return true;
        }
        //-----------------------------------------
        public void Remove(User_stat_Tag entity)
        {
            _context.Set<User_stat_Tag>().Remove(entity);
        }
        //-----------------------------------------
        public void Update(User_stat_Tag entity)
        {
            _context.User_stat_Tag.Update(entity);
        }
        //-----------------------------------------
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
