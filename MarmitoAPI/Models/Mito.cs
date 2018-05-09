using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarmitoAPI.Models
{
    public class Mito
    {
        public enum CategoryList
        {
            MITO = 0,
            EXCUSE = 1,
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Content { get; set; }

        public CategoryList Category { get; set; }

        [ForeignKey("User")]
        public long AuthorId { get; set; }
    }
}