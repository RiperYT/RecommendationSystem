using TagsMath.Data.Entities;

namespace TagsMath.Data.Abstractions
{
    internal interface IPost_stat_TagRepository
    {
        //-----------------------------------------
        public Post_stat_Tag? GetPost_stat_TagByIds(int post_id, int tag_id);
        public IQueryable<Post_stat_Tag> GetAll();
        public IQueryable<Post_stat_Tag> GetPost_stat_TagByPostId(int post_id);
        //-----------------------------------------
        public bool Add(Post_stat_Tag newEntity);
        //-----------------------------------------
        public void Remove(Post_stat_Tag entity);
        //-----------------------------------------
        public void Update(Post_stat_Tag entity);
        //-----------------------------------------
        public int SaveChanges();
    }
}
