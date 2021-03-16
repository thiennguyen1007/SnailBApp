using SnailBApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnailBApp.Data.FoodData
{
    public interface IFoodStore
    {
        Task<IEnumerable<Food>> GetNhanViensAsync();
        Task<Food> GetNhanVien(int ID);
        Task AddNhanVien(Food f);
        Task UpdateNhanVien(Food f);
        Task DeleteNhanVien(Food f);
    }
}
