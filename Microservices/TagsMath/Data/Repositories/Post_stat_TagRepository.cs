using TagsMath.Data.Abstractions;
using TagsMath.Data.Entities;

namespace TagsMath.Data.Repositories
{
    internal class Post_stat_TagRepository : IPost_stat_TagRepository
    {
        private readonly DataContext _context;

        public Post_stat_TagRepository(DataContext context)
        {
            _context = context;
        }
        //-----------------------------------------
        public Post_stat_Tag? GetPost_stat_TagByIds(int post_id, int tag_id)
        {
            return _context.Set<Post_stat_Tag>().Where(t => t.post_id == post_id && t.tag_id == tag_id).First();
        }
        public IQueryable<Post_stat_Tag> GetAll()
        {
            return _context.Set<Post_stat_Tag>().AsQueryable();
        }
        public IQueryable<Post_stat_Tag> GetPost_stat_TagByPostId(int post_id)
        {
            return _context.Set<Post_stat_Tag>().Where(t => t.post_id == post_id);
        }
        //-----------------------------------------
        public bool Add(Post_stat_Tag newEntity)
        {
            var entity = _context.Set<Post_stat_Tag>().Add(newEntity);
            SaveChanges();
            return true;
        }
        //-----------------------------------------
        public void Remove(Post_stat_Tag entity)
        {
            _context.Set<Post_stat_Tag>().Remove(entity);
            SaveChanges();
        }
        //-----------------------------------------
        public void Update(Post_stat_Tag entity)
        {
            _context.Set<Post_stat_Tag>().Update(entity);
            SaveChanges();
        }
        //-----------------------------------------
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
