using SnailBApp.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnailBApp.Data.NhanVienData
{
    public class SQLiteNhanVienStore : INhanVienStore
    {
        private SQLiteAsyncConnection _connection;
        public SQLiteNhanVienStore(ISQLite db)
        {
            _connection = db.GetAsyncConnection();
            _connection.CreateTableAsync<NhanVien>();
        }
        public async Task AddNhanVien(NhanVien x)
        {
            await _connection.InsertAsync(x);
        }

        public async Task DeleteNhanVien(NhanVien x)
        {
            await _connection.DeleteAsync(x);
        }

        public async Task<NhanVien> GetNhanVien(int ID)
        {
            return await _connection.FindAsync<NhanVien>(ID);
        }

        public async Task<IEnumerable<NhanVien>> GetNhanViensAsync()
        {
            return await _connection.Table<NhanVien>().ToListAsync();
        }

        public async Task UpdateNhanVien(NhanVien x)
        {
            await _connection.UpdateAsync(x);
        }
    }
}
