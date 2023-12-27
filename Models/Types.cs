using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CodeWithMe.Models
{
    public class Types
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Languages are required")]
        public int LangaugeId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { set; get; }
        [AllowNull]
        public string? File { set; get; }



        [Column(TypeName = "varbinary(MAX)")]
        [AllowNull]
        public byte[]? Attachment { set; get; }

    }
}
