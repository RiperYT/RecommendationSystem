using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TagStartupMath.Data.Entities
{
    [PrimaryKey(nameof(user_id), nameof(tag_id))]
    internal class User_stat_Tag
    {
        public int user_id { get; set; }

        public int tag_id { get; set; }

        [Required]
        public int tag_count { get; set; }
    }
}
