using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagStartupMath.Data.Abstractions;
using TagStartupMath.Data.Entities;

namespace TagStartupMath.Data.Repositories
{
    internal class Startup_stat_TagRepository : IStartup_stat_TagRepository
    {
        private readonly DataContext _context;

        public Startup_stat_TagRepository(DataContext context)
        {
            _context = context;
        }
        //-----------------------------------------
        public Startup_stat_Tag? GetStartup_stat_TagByIds(int startup_id, int tag_id)
        {
            return _context.Startup_stat_Tag.Where(t => t.startup_id == startup_id && t.tag_id == tag_id).First();
        }
        public IQueryable<Startup_stat_Tag> GetAll()
        {
            return _context.Startup_stat_Tag.AsQueryable();
        }
        public IQueryable<Startup_stat_Tag> GetStartup_stat_TagByStartupId(int startup_id)
        {
            return _context.Startup_stat_Tag.Where(t => t.startup_id == startup_id);
        }
        //-----------------------------------------
        public bool Add(Startup_stat_Tag newEntity)
        {
            var entity = _context.Startup_stat_Tag.Add(newEntity);
            return true;
        }
        //-----------------------------------------
        public void Remove(Startup_stat_Tag entity)
        {
            _context.Startup_stat_Tag.Remove(entity);
        }
        //-----------------------------------------
        public void Update(Startup_stat_Tag entity)
        {
            _context.Startup_stat_Tag.Update(entity);
        }
        //-----------------------------------------
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
