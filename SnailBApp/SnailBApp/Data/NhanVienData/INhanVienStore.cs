using SnailBApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnailBApp.Data.NhanVienData
{
    public interface INhanVienStore
    {
        Task<IEnumerable<NhanVien>> GetNhanViensAsync();
        Task<NhanVien> GetNhanVien(int ID);
        Task AddNhanVien(NhanVien x);
        Task UpdateNhanVien(NhanVien x);
        Task DeleteNhanVien(NhanVien x);
    }
}
