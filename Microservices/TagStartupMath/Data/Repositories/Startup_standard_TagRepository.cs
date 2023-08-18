using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagStartupMath.Data.Abstractions;
using TagStartupMath.Data.Entities;

namespace TagStartupMath.Data.Repositories
{
    internal class Startup_standard_TagRepository : IStartup_standard_TagRepository
    {
        private readonly DataContext _context;

        public Startup_standard_TagRepository(DataContext context)
        {
            _context = context;
        }
        //-----------------------------------------
        public Startup_standard_Tag? GetStartup_standard_TagByIds(int startup_id, int tag_id)
        {
            return _context.Startup_standard_Tag.Where(t => t.startup_id == startup_id && t.tag_id == tag_id).First();
        }
        public IQueryable<Startup_standard_Tag> GetAll()
        {
            return _context.Startup_standard_Tag.AsQueryable();
        }
        public IQueryable<Startup_standard_Tag> GetStartup_standard_TagByStartupId(int startup_id)
        {
            return _context.Startup_standard_Tag.Where(t => t.startup_id == startup_id);
        }
        //-----------------------------------------
        public bool Add(Startup_standard_Tag newEntity)
        {
            var entity = _context.Startup_standard_Tag.Add(newEntity);
            return true;
        }
        //-----------------------------------------
        public void Remove(Startup_standard_Tag entity)
        {
            _context.Startup_standard_Tag.Remove(entity);
        }
        //-----------------------------------------
        public void Update(Startup_standard_Tag entity)
        {
            _context.Startup_standard_Tag.Update(entity);
        }
        //-----------------------------------------
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
