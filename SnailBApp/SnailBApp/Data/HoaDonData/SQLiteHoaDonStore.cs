using SnailBApp.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnailBApp.Data.HoaDonData
{
    public class SQLiteHoaDonStore : IHoaDonStore
    {
        private SQLiteAsyncConnection _connection;
        public SQLiteHoaDonStore(ISQLite db)
        {
            _connection = db.GetAsyncConnection();
            _connection.CreateTableAsync<HoaDon>();
        }
        public async Task AddHoaDon(HoaDon HD)
        {
            await _connection.InsertAsync(HD);
        }

        public async Task DeleteHoaDon(HoaDon HD)
        {
            await _connection.DeleteAsync(HD);
        }

        public Task<HoaDon> GetHoaDon(int ID)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<HoaDon>> GetHoaDonAsync()
        {
            return await _connection.Table<HoaDon>().ToListAsync();
        }

        public async Task UpdateHoaDon(HoaDon HD)
        {
            await _connection.UpdateAsync(HD);
        }
    }
}
