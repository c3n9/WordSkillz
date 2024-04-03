using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WordSkillz.Models
{
    public class Account : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public Int32 Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        public string PhoneNumber { get; set; }
        private int learnedWordsCount;
        private int correctAnswersCount;
        private int incorrectAnswersCount;
        public int LearnedWordsCount
        {
            get { return learnedWordsCount; }
            set
            {
                if (value != learnedWordsCount)
                {
                    learnedWordsCount = value;
                    OnPropertyChanged("LearnedWordsCount");
                }
            }
        }

        public int CorrectAnswersCount
        {
            get { return correctAnswersCount; }
            set
            {
                if (value != correctAnswersCount)
                {
                    correctAnswersCount = value;
                    OnPropertyChanged("CorrectAnswersCount");
                }
            }
        }

        public int IncorrectAnswersCount
        {
            get { return incorrectAnswersCount; }
            set
            {
                if (value != incorrectAnswersCount)
                {
                    incorrectAnswersCount = value;
                    OnPropertyChanged("IncorrectAnswersCount");
                }
            }
        }
        private ImageSource _photoImageSource;
        [JsonIgnore]
        [Ignore]
        public ImageSource PhotoImageSource
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Photo))
                {
                    _photoImageSource = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Photo)));
                }
                return _photoImageSource;
            }
            set
            {
                _photoImageSource = value;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

