using TagsMath.Data.Abstractions;
using TagsMath.Data.Entities;

namespace TagsMath.Data.Repositories
{
    internal class Post_standard_TagRepository : IPost_standard_TagRepository
    {
        private readonly DataContext _context;

        public Post_standard_TagRepository(DataContext context)
        {
            _context = context;
        }
        //-----------------------------------------
        public Post_standard_Tag? GetPost_standard_TagByIds(int post_id, int tag_id)
        {
            return _context.Set<Post_standard_Tag>().Where(t => t.post_id == post_id && t.tag_id == tag_id).FirstOrDefault();
        }
        public IQueryable<Post_standard_Tag> GetAll()
        {
            return _context.Set<Post_standard_Tag>().AsQueryable();
        }
        public IQueryable<Post_standard_Tag >GetPost_standard_TagByPostId(int post_id)
        {
            return _context.Set<Post_standard_Tag>().Where(t => t.post_id == post_id);
        }
        //-----------------------------------------
        public bool Add(Post_standard_Tag newEntity)
        {
            var entity = _context.Set<Post_standard_Tag>().Add(newEntity);
            return true;
        }
        //-----------------------------------------
        public void Remove(Post_standard_Tag entity)
        {
            _context.Set<Post_standard_Tag>().Remove(entity);
        }
        //-----------------------------------------
        public void Update(Post_standard_Tag entity)
        {
            _context.Set<Post_standard_Tag>().Update(entity);
        }
        //-----------------------------------------
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
