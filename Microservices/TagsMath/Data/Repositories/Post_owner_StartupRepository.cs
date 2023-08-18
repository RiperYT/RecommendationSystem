using TagsMath.Data.Abstractions;
using TagsMath.Data.Entities;

namespace TagsMath.Data.Repositories
{
    internal class Post_owner_StartupRepository : IPost_owner_StartupRepository
    {
        private readonly DataContext _context;

        public Post_owner_StartupRepository(DataContext context)
        {
            _context = context;
        }
        //-----------------------------------------
        public Post_owner_Startup? GetPost_owner_StartupByIds(int startup_id, int post_id)
        {
            return _context.Set<Post_owner_Startup>().Where(t => t.post_id == post_id && t.startup_id == startup_id).FirstOrDefault();
        }
        public IQueryable<Post_owner_Startup> GetAll()
        {
            return _context.Set<Post_owner_Startup>().AsQueryable();
        }
        public Post_owner_Startup? GetPost_owner_StartupByPostId(int post_id)
        {
            return _context.Set<Post_owner_Startup>().Where(t => t.post_id == post_id).First();
        }
        public IQueryable<Post_owner_Startup> GetPost_owner_StartupByStartupId(int startup_id)
        {
            return _context.Set<Post_owner_Startup>().Where(t => t.post_id == startup_id);
        }
        //-----------------------------------------
        public bool Add(Post_owner_Startup newEntity)
        {
            var entity = _context.Set<Post_owner_Startup>().Add(newEntity);
            return true;
        }
        //-----------------------------------------
        public void Remove(Post_owner_Startup entity)
        {
            _context.Set<Post_owner_Startup>().Remove(entity);
        }
        //-----------------------------------------
        public void Update(Post_owner_Startup entity)
        {
            _context.Set<Post_owner_Startup>().Update(entity);
        }
        //-----------------------------------------
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
