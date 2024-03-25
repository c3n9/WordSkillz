using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WordSkillz.Tools;

namespace WordSkillz.Models
{
    public partial class Category
    {
        [PrimaryKey, AutoIncrement]
        public Int32 Id { get; set; }
        public String Name { get; set; }
        private int _wordCount;
        public int WordCount { get; set; }
    }

}
