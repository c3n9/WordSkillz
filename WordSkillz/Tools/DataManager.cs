using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordSkillz.Models;

namespace WordSkillz.Tools
{
    public static class DataManager
    {
        public static readonly string WordsImportPath = "word.json";
        public static readonly string CategoryImportPath = "category.json";
        public static string WordCachePath { get => Path.Combine(FileSystem.Current.AppDataDirectory, "wordCache.json"); }
        public static string CategoryCachePath { get => Path.Combine(FileSystem.Current.AppDataDirectory, "categoryCache.json"); }
        private static ObservableCollection<Word> words;
        private static ObservableCollection<Category> categories;
        public static ObservableCollection<Word> AllWords
        {
            get
            {
                if (words == null)
                    words = GetData<ObservableCollection<Word>>(WordCachePath);
                return words;
            }
            private set
            {
                words = value;
                SetData(WordCachePath, words);
            }
        }
        public static ObservableCollection<Category> AllCategories
        {
            get
            {
                if (categories == null)
                    categories = GetData<ObservableCollection<Category>>(CategoryCachePath);
                return categories;
            }
            private set
            {
                categories = value;
                SetData(CategoryCachePath, categories);
            }
        }
        public static ObservableCollection<Category> GetCategories()
        {
            var categories = GetData<ObservableCollection<Category>>(CategoryCachePath);
            AllCategories = categories;
            return categories;
        }
        public static ObservableCollection<Word> GetWords()
        {
            var words = GetData<ObservableCollection<Word>>(WordCachePath);
            AllWords = words;
            return words;
        }
        private static T GetData<T>(string fileName)
        {
            var data = JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName));
            return data;
        }

        public static void SetWord(ObservableCollection<Word> words)
        {
            AllWords = words;
            SetData(WordCachePath, words);
        }
        public static void SetWord(Word word)
        {
            AllWords.Add(word);
            SetData(WordCachePath, AllWords);
        }
        public static void SetCategory(ObservableCollection<Category> categories)
        {
            AllCategories = categories;
            SetData(CategoryCachePath, categories);
        }
        public static void SetCategory(Category category)
        {
            AllCategories.Add(category);
            SetData(CategoryCachePath, AllCategories);
        }
        public static void RemoveCategory(Category category)
        {
            AllCategories.Remove(category);
            SetData(CategoryCachePath, AllCategories);
        }
        private static void SetData<T>(string fileName, T data) where T : IEnumerable
        {
            var jsonData = JsonConvert.SerializeObject(data);
            File.WriteAllText(fileName, jsonData);
        }
        public static async void InitDataFile(string outputFileName, string sourceFileName)
        {
            if (!File.Exists(outputFileName))
            {
                var file = File.Create(outputFileName);
                file.Close();
                File.WriteAllText(outputFileName, await ReadAsset(sourceFileName));
            }
        }

        private static async Task<string> ReadAsset(string assetPath)
        {
            using (var sourceStream = await FileSystem.OpenAppPackageFileAsync(assetPath))
            {
                using (var reader = new StreamReader(sourceStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
