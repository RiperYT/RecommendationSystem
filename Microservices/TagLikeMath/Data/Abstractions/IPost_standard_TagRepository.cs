using TagLikeMath.Data.Entities;

namespace TagLikeMath.Data.Abstractions
{
    internal interface IPost_standard_TagRepository
    {
        //-----------------------------------------
        public Post_standard_Tag? GetPost_standard_TagByIds(int post_id, int tag_id);
        public IQueryable<Post_standard_Tag> GetPost_standard_TagByPostId(int post_id);
        public IQueryable<Post_standard_Tag> GetAll();
        //-----------------------------------------
        public bool Add(Post_standard_Tag newEntity);
        //-----------------------------------------
        public void Remove(Post_standard_Tag entity);
        //-----------------------------------------
        public void Update(Post_standard_Tag entity);
        //-----------------------------------------
        public int SaveChanges();
    }
}
