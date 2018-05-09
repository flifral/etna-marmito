using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarmitoAPI.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
    }
}