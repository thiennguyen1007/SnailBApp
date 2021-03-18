using SnailBApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnailBApp.Data.FoodData
{
    public interface IFoodStore
    {
        Task<IEnumerable<Food>> GetFoodsAsync();
        Task<Food> GetFood(int ID);
        Task AddFood(Food f);
        Task UpdateFood(Food f);
        Task DeleteFood(Food f);
    }
}
