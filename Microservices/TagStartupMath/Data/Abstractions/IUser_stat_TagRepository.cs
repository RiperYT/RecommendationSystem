using TagStartupMath.Data.Entities;

namespace TagStartupMath.Data.Abstractions
{
    internal interface IUser_stat_TagRepository
    {
        //-----------------------------------------
        public User_stat_Tag? GetUser_stat_TagByIds(int user_id, int tag_id);
        public IQueryable<User_stat_Tag> GetAll();
        public IQueryable<User_stat_Tag> GetUser_stat_TagByUserId(int user_id);
        //-----------------------------------------
        public bool Add(User_stat_Tag newEntity);
        //-----------------------------------------
        public void Remove(User_stat_Tag entity);
        //-----------------------------------------
        public void Update(User_stat_Tag entity);
        //-----------------------------------------
        public int SaveChanges();
    }
}
