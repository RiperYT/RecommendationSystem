using TagLikeMath.Data.Entities;

namespace TagLikeMath.Data.Abstractions
{
    internal interface IPostRepository
    {
        //-----------------------------------------
        public Post? GetPostById(int id);
        public IQueryable<Post> GetAll();
        //-----------------------------------------
        public bool Add(Post newEntity);
        //-----------------------------------------
        public void Remove(Post entity);
        //-----------------------------------------
        public void Update(Post entity);
        //-----------------------------------------
        public int SaveChanges();
    }
}
