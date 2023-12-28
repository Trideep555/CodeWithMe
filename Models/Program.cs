using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CodeWithMe.Data;

namespace CodeWithMe.Models
{
    public class Program
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { set; get; }
        [Required]
        public string? Code { set; get; }
        [Required(ErrorMessage = "Language is required")]
        
        public int? LanguageId { set; get; }
        [Required(ErrorMessage = "Type is required")]
        public int? TypeId { set; get; }


    }
}
