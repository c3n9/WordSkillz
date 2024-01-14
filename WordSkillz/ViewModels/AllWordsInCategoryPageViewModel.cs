using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WordSkillz.Models;
using WordSkillz.Tools;

namespace WordSkillz.ViewModels
{
    public class AllWordsInCategoryPageViewModel : Models.BindableObject
    {
        public Category ContextCategory { get; set; }
        public ObservableRangeCollection<Word> Words { get; set; } = new ObservableRangeCollection<Word>();

        private string _searchString;
        public string SearchString 
        { 
            get { return _searchString; }
            set { SetProperty(ref _searchString, value); TextChanged(value); }
        }

        public ICommand ButtonCommand { get; set; }

        public AllWordsInCategoryPageViewModel(Category contextCategory)
        {
            ButtonCommand = new DelegateCommand<Word>(Button);

            ContextCategory = contextCategory;
            Words.AddRange(DataManager.AllWords.Where(x => x.CategoryId == contextCategory.Id));
        }

        private void Button(Word word)
        {
            DataManager.RemoveWord(word);
            Words.Remove(word);
        }

        private void TextChanged(string newText)
        {
            var words = DataManager.AllWords.Where(x => !x.TranslatedWord.ToLower().Contains(newText.ToLower()) && x.CategoryId == ContextCategory.Id);
            foreach (var word in words)
            {
                Words.Remove(word);
            }
        }
    }
}
