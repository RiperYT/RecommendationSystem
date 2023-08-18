using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagStartupMath.Data.Entities;

namespace TagStartupMath.Data.Abstractions
{
    internal interface IStartup_stat_TagRepository
    {
        public Startup_stat_Tag? GetStartup_stat_TagByIds(int startup_id, int tag_id);
        public IQueryable<Startup_stat_Tag> GetAll();
        public IQueryable<Startup_stat_Tag> GetStartup_stat_TagByStartupId(int startup_id);
        //-----------------------------------------
        public bool Add(Startup_stat_Tag newEntity);
        //-----------------------------------------
        public void Remove(Startup_stat_Tag entity);
        //-----------------------------------------
        public void Update(Startup_stat_Tag entity);
        //-----------------------------------------
        public int SaveChanges();
    }
}

