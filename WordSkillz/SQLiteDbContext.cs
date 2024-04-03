using SQLite;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordSkillz.Models;

namespace WordSkillz
{
    public class SQLiteDbContext
    {
        const string DataBaseFileName = "sqlitedatabase.db3";
        static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DataBaseFileName);
        const SQLite.SQLiteOpenFlags Flags = SQLite.SQLiteOpenFlags.ReadWrite
                                             | SQLite.SQLiteOpenFlags.Create
                                             | SQLite.SQLiteOpenFlags.SharedCache;
        SQLiteAsyncConnection Database;

        public SQLiteDbContext()
        {
            Database = new SQLiteAsyncConnection(DatabasePath, Flags);
            InitializeDatabase();
        }

        async Task InitializeDatabase()
        {
            if (Database is not null)
            {
                await Database.CreateTableAsync<Category>();
                await Database.CreateTableAsync<Word>();
                await Database.CreateTableAsync<Account>();
            }
        }

        public async Task<List<Category>> GetAllCategory()
        {
            await InitializeDatabase();

            var categories = await Database.Table<Category>().ToListAsync();
            var words = await GetAllWord();

            foreach (var category in categories)
            {
                category.WordCount = words.Count(word => word.CategoryId == category.Id);
            }

            return categories;
        }

        public async Task<int> AddCategoryAsync(Category category)
        {
            await InitializeDatabase();
            if (category.Id != 0)
            {
                return await Database.UpdateAsync(category);
            }
            else
            {
                return await Database.InsertAsync(category);
            }
        }
        public async Task<List<Word>> GetAllWord()
        {
            await InitializeDatabase();
            var words = await Database.Table<Word>().ToListAsync();
            return words;
        }

        public async Task<Word> GetWordAsync(int wordId)
        {
            await InitializeDatabase();
            return await Database.Table<Word>().Where(x => x.Id == wordId).FirstOrDefaultAsync();
        }

        public async Task<int> AddWordAsync(Word word)
        {
            await InitializeDatabase();
            if (word.Id != 0)
            {
                return await Database.UpdateAsync(word);
            }
            else
            {
                return await Database.InsertAsync(word);
            }
        }
        public async Task<int> DeleteCategoryAsync(Category category)
        {
            await InitializeDatabase();
            return await Database.DeleteAsync(category);
        }

        public async Task<int> DeleteWordAsync(Word word)
        {
            await InitializeDatabase();
            return await Database.DeleteAsync(word);
        }
        public async Task<int> AddAccountAsync(Account account)
        {
            await InitializeDatabase();
            return await Database.InsertAsync(account);
        }

        public async Task<Account> GetAccountAsync(int accountId)
        {
            await InitializeDatabase();
            return await Database.Table<Account>().Where(x => x.Id == accountId).FirstOrDefaultAsync();
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            await InitializeDatabase();
            return await Database.Table<Account>().ToListAsync();
        }

        public async Task<int> UpdateAccountAsync(Account account)
        {
            await InitializeDatabase();
            return await Database.UpdateAsync(account);
        }

        public async Task<int> DeleteAccountAsync(Account account)
        {
            await InitializeDatabase();
            return await Database.DeleteAsync(account);
        }
    }

}
