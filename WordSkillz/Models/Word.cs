using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSkillz.Models
{
    public partial class Word
    {
        public Int32 Id { get; set; }
        public String OriginalWord { get; set; }
        public String TranslatedWord { get; set; }
        public Int32 CategoryId { get; set; }

    }
}
