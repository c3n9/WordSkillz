using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WordSkillz.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        public string PhoneNumber { get; set; }
        [JsonIgnore]
        private ImageSource _photoImageSource;
        [JsonIgnore]
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
    }
}

