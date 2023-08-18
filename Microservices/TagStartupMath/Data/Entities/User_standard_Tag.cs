using Microsoft.EntityFrameworkCore;

namespace TagStartupMath.Data.Entities
{
    [PrimaryKey(nameof(user_id), nameof(tag_id))]
    internal class User_standard_Tag
    {
        public int user_id { get; set; }

        public int tag_id { get; set; }
    }
}
