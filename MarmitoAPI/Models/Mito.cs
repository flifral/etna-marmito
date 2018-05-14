using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarmitoAPI.Models
{
    public class Mito
    {
        public enum CategoryList
        {
            INDISP = 0,
            FAMILY = 1,
            WORK = 2,
            LOVE = 3
        };

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Content { get; set; }

        public CategoryList Category { get; set; }

        [ForeignKey("User")]
        public long AuthorId { get; set; }
    }
}