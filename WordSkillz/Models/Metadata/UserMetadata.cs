using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSkillz.Models.Metadata
{
    public class UserMetadata
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(50)]
        public string Password { get; set; }
        [Required]
        public string Image { get; set; }
        [Required, Phone]
        public string PhoneNumber { get; set; }
        public int LearnedWordsCount { get; set; }
        public int IncorrectAnswersCount { get; set; }
        public int CorrectAnswersCount { get; set; }
    }
}
