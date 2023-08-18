using TagsMath.Data.Entities;

namespace TagsMath.Data.Abstractions
{
    internal interface IPost_owner_StartupRepository
    {
        public Post_owner_Startup? GetPost_owner_StartupByIds(int startup_id, int post_id);
        public IQueryable<Post_owner_Startup> GetAll();
        public Post_owner_Startup? GetPost_owner_StartupByPostId(int post_id);
        public IQueryable<Post_owner_Startup> GetPost_owner_StartupByStartupId(int startup_id);
        //-----------------------------------------
        public bool Add(Post_owner_Startup newEntity);
        //-----------------------------------------
        public void Remove(Post_owner_Startup entity);
        //-----------------------------------------
        public void Update(Post_owner_Startup entity);
        //-----------------------------------------
        public int SaveChanges();
    }
}
