using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TagsMath.Data.Entities
{
    internal class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [StringLength(150)]
        public string title { get; set; }

        [Required]
        [StringLength(5000)]
        public string text { get; set; }

        [Required]
        public DateTime date_posting { get; set; }
    }
}
