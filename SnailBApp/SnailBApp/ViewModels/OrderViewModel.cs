using SnailBApp.Data.FoodData;
using System.Windows.Input;

namespace SnailBApp.ViewModels
{
    class OrderViewModel : BaseViewModel
    {
        //to edit SQL
        private IFoodStore _foodStore;
        //to go to StartPage
        public ICommand HomeCommand { get; private set; }
        //
        public ICommand MyBagCommand { get; private set; }
        //
        public ICommand AddCommand { get; private set; }
        public OrderViewModel()
        {
            
        }
    }
}
