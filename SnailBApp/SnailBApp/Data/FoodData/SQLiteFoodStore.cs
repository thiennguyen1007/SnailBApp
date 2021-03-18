using SnailBApp.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnailBApp.Data.FoodData
{
    class SQLiteFoodStore : IFoodStore
    {
        private SQLiteAsyncConnection _connection;
        public SQLiteFoodStore(ISQLite db)
        {
            _connection =db.GetAsyncConnection();
            _connection.CreateTableAsync<Food>();
        }
        public async Task AddFood(Food f)
        {
            await _connection.InsertAsync(f);
        }
        public async Task UpdateFood(Food f)
        {
            await _connection.UpdateAsync(f);
        }
        public async Task DeleteFood(Food f)
        {
            await _connection.DeleteAsync(f);
        }
        public async Task<Food> GetFood(int ID)
        {
            return await _connection.FindAsync<Food>(ID);
        }
        public async Task<IEnumerable<Food>> GetFoodsAsync()
        {
            return await _connection.Table<Food>().ToListAsync();
        }

        
    }
}
