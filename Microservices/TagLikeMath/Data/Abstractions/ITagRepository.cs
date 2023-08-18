using TagLikeMath.Data.Entities;

namespace TagLikeMath.Data.Abstractions
{
    internal interface ITagRepository
    {
        //-----------------------------------------
        public Tag? GetTagById(int id);
        public IQueryable<Tag> GetAll();
        //-----------------------------------------
        public bool Add(Tag newEntity);
        //-----------------------------------------
        public void Remove(Tag entity);
        //-----------------------------------------
        public void Update(Tag entity);
        //-----------------------------------------
        public int SaveChanges();
    }
}
