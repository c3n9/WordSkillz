using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSkillz.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }   
        public string Password { get; set; }    
        public byte[] Image { get; set; }
        public string PhoneNumber { get; set; }
        public int LearnedWordsCount { get; set; }  
        public int IncorrectAnswersCount { get; set; }  
        public int CorrectAnswersCount { get; set; }  
    }
}
