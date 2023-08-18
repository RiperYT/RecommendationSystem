using TagStartupMath.Data.Entities;

namespace TagStartupMath.Data.Abstractions
{
    internal interface IUser_standard_TagRepository
    {
        //-----------------------------------------
        public User_standard_Tag? GetUser_standard_TagByIds(int user_id, int tag_id);
        public IQueryable<User_standard_Tag> GetUser_standard_TagByUserId(int user_id);
        public IQueryable<User_standard_Tag> GetAll();
        //-----------------------------------------
        public bool Add(User_standard_Tag newEntity);
        //-----------------------------------------
        public void Remove(User_standard_Tag entity);
        //-----------------------------------------
        public void Update(User_standard_Tag entity);
        //-----------------------------------------
        public int SaveChanges();
    }
}
