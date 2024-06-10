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

        [Required(ErrorMessage = "Поле 'Имя' обязательно для заполнения.")]
        [MaxLength(50, ErrorMessage = "Поле 'Имя' не должно превышать 50 символов.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле 'Электронная почта' обязательно для заполнения.")]
        [EmailAddress(ErrorMessage = "Поле 'Электронная почта' должно содержать корректный адрес.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле 'Пароль' обязательно для заполнения.")]
        [MaxLength(50, ErrorMessage = "Поле 'Пароль' не должно превышать 50 символов.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле 'Изображение' обязательно для заполнения.")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Поле 'Номер телефона' обязательно для заполнения.")]
        [Phone(ErrorMessage = "Поле 'Номер телефона' должно содержать корректный номер.")]
        [RegularExpression(@"^8\d{10}$", ErrorMessage = "Поле 'Номер телефона' должно быть в формате 89999999999.")]
        public string PhoneNumber { get; set; }

        public int LearnedWordsCount { get; set; }
        public int IncorrectAnswersCount { get; set; }
        public int CorrectAnswersCount { get; set; }

    }
}
