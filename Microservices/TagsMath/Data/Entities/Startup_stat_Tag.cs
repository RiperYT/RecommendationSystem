using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace TagsMath.Data.Entities
{
    [PrimaryKey(nameof(startup_id), nameof(tag_id))]
    internal class Startup_stat_Tag
    {
        public int startup_id { get; set; }
        public int tag_id { get; set; }
        [Required]
        public int tag_count { get; set; }
    }
}
