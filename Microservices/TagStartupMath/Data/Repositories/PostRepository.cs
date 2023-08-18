using TagStartupMath.Data.Abstractions;
using TagStartupMath.Data.Entities;

namespace TagStartupMath.Data.Repositories
{
    internal class PostRepository : IPostRepository
    {
        private readonly DataContext _context;

        public PostRepository(DataContext context)
        {
            _context = context;
        }
        //-----------------------------------------
        public Post? GetPostById(int id)
        {
            return _context.Set<Post>().Where(t => t.id == id).FirstOrDefault();
        }
        public IQueryable<Post> GetAll()
        {
            return _context.Set<Post>().AsQueryable();
        }
        //-----------------------------------------
        public bool Add(Post newEntity)
        {
            var entity = _context.Set<Post>().Add(newEntity);
            return true;
        }
        //-----------------------------------------
        public void Remove(Post entity)
        {
            _context.Set<Post>().Remove(entity);
        }
        //-----------------------------------------
        public void Update(Post entity)
        {
            _context.Set<Post>().Update(entity);
        }
        //-----------------------------------------
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
