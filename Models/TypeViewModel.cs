using System.ComponentModel.DataAnnotations;

namespace CodeWithMe.Models
{
    public class TypeViewModel
    {

        public string? FileName { set; get; }
        [Required]
        public string? Description { set; get; }
        [Required]
        public string? Name { set; get; }

        [Required]
        public List<int>? LanguagesId { set; get; }

        public IFormFile? Attachment { set; get; }
        public List<Types>? Attachments { get; set; }


    }
}
