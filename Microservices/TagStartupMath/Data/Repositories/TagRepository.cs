using TagStartupMath.Data.Abstractions;
using TagStartupMath.Data.Entities;

namespace TagStartupMath.Data.Repositories
{
    internal class TagRepository : ITagRepository
    {
        private readonly DataContext _context;

        public TagRepository(DataContext context)
        {
            _context = context;
        }
        //-----------------------------------------
        public Tag? GetTagById(int id)
        {
            return _context.Set<Tag>().Where(t => t.id == id).FirstOrDefault();
        }
        public IQueryable<Tag> GetAll()
        {
            return _context.Set<Tag>().AsQueryable();
        }
        //-----------------------------------------
        public bool Add(Tag newEntity)
        {
            var entity = _context.Set<Tag>().Add(newEntity);
            return true;
        }
        //-----------------------------------------
        public void Remove(Tag entity)
        {
            _context.Set<Tag>().Remove(entity);
        }
        //-----------------------------------------
        public void Update(Tag entity)
        {
            _context.Set<Tag>().Update(entity);
        }
        //-----------------------------------------
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
