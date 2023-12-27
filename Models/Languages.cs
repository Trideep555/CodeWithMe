using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CodeWithMe.Models
{
    public class Languages
    {
        public int Id { get; set; }
        [Required]
        public  string? Name { get; set; }
        [Required]
        public string? Description { set; get; }
        [AllowNull]
        public string? File { set; get; }



        [Column(TypeName = "varbinary(MAX)")]
        [AllowNull]
        public byte[]? Attachment { set; get; }


        

         }
}
