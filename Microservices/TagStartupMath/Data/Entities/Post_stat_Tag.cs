using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TagStartupMath.Data.Entities
{
    [PrimaryKey(nameof(post_id), nameof(tag_id))]
    internal class Post_stat_Tag
    {
        public int post_id { get; set; }

        public int tag_id { get; set; }

        [Required]
        public int tag_count { get; set; }
    }
}
