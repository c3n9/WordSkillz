using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WordSkillz.Tools;

namespace WordSkillz.Models
{
    public partial class Category
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        [JsonIgnore]
        public int WordCount
        {
            get
            {
                var words = new ObservableCollection<Word>(DataManager.AllWords.Where(x => x.CategoryId == Id));
                return words.Count();
            }
        }

    }
}
