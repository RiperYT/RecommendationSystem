using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TagLikeMath.Data.Entities
{
    internal class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [StringLength(30)]
        public string login { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [StringLength(400)]
        public string description { get; set; }

        [Required]
        public bool verify { get; set; }

        [Required]
        public DateTime date_joining { get; set; }

        [Required]
        [StringLength(256)]
        public string password { get; set; }
    }
}
