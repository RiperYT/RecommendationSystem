using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsMath.Data.Entities;

namespace TagsMath.Data.Abstractions
{
    internal interface IStartup_standard_TagRepository
    {
        public Startup_standard_Tag? GetStartup_standard_TagByIds(int startup_id, int tag_id);
        public IQueryable<Startup_standard_Tag> GetAll();
        public IQueryable<Startup_standard_Tag> GetStartup_standard_TagByStartupId(int startup_id);
        //-----------------------------------------
        public bool Add(Startup_standard_Tag newEntity);
        //-----------------------------------------
        public void Remove(Startup_standard_Tag entity);
        //-----------------------------------------
        public void Update(Startup_standard_Tag entity);
        //-----------------------------------------
        public int SaveChanges();
    }
}
