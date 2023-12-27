using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace CodeWithMe.Models
{
    public class LanguageViewModel
    {

            public string? FileName { set; get; }
        [Required]
        public string? Description { set; get; }
        [Required]
        public string? Name { set; get; }

            public IFormFile? Attachment { set; get; }
            public List<Languages>? Attachments { get; set; }
        
    }
}
