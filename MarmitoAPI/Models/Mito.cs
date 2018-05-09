using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarmitoAPI.Models
{
    public class Mito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        [ForeignKey("User")]
        public long AuthorId { get; set; }
    }
}