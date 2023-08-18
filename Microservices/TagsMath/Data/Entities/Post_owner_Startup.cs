using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TagsMath.Data.Entities
{
    [PrimaryKey(nameof(startup_id), nameof(post_id))]
    internal class Post_owner_Startup
    {
        public int startup_id { get; set; }
        public int post_id { get; set; }
        [Required]
        public int writer_id { get; set; }
        [Required]
        public bool show_writer { get; set; }
    }
}
