using SnailBApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnailBApp.Data.HoaDonData
{
    public interface IHoaDonStore
    {
        Task<IEnumerable<HoaDon>> GetHoaDonAsync();
        Task<HoaDon> GetHoaDon(int ID);
        Task AddHoaDon(HoaDon HD);
        Task UpdateHoaDon(HoaDon HD);
        Task DeleteHoaDon(HoaDon HD);
    }
}
