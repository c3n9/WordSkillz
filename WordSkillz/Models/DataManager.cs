using Microsoft.Maui.Controls.Compatibility;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace WordSkillz.Models
{
    public static class DataManager
    {
        public static readonly string WordsImportPath = "words.json";
        public static string WordsCachePath { get => Path.Combine(FileSystem.Current.AppDataDirectory, "words.json"); }

        //private static List<Words> words;
        //private static List<Category> categories;
        //public static List<Word> AllWords
        //{

        //    get
        //    {
        //        if (words == null)
        //            words = GetData<List<News>>(NewsCachePath);
        //        return words;
        //    }
        //    private set
        //    {
        //        words = value;
        //        SetData(WordsCachePath, words);
        //    }
        //}
        //public static List<Word> GetWords()
        //{
        //    var words = GetData<List<Word>>(WordsCachePath);
        //    AllWords = words.ToList();
        //    return words;
        //}
        //private static T GetData<T>(string fileName)
        //{
        //    var data = JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName));
        //    return data;

        //}

        //public static void SetWord(List<Word> words)
        //{
        //    AllWords = words.ToList();
        //    SetData(WordsCachePath, words);
        //}

        //public static void SetNews(Word word)
        //{
        //    AllWords.Add(word);
        //    SetData(WordsCachePath, AllWords);
        //}

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
