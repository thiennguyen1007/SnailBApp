using SnailBApp.Data.FoodData;
using SnailBApp.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels.MonAnVM
{
    public class DetailFoodPageViewModel : BaseViewModel
    {
        private IPageService _pageService;
        private IFoodStore _foodStore;
        //Command
        public ICommand UpdateCommand { get; private set; }
        //
        private FoodViewModel _food;
        public FoodViewModel Food { get => _food; set => SetProperty(ref _food, value); }
        //
        public DetailFoodPageViewModel(IPageService pageService, IFoodStore foodStore,FoodViewModel f)
        {
            _pageService = pageService;
            _foodStore = foodStore;
            //
            Food = new FoodViewModel();
            Food.ID = f.ID;
            Food.Name = f.Name;
            Food.Desc = f.Desc;
            Food.IMG = f.IMG;
            Food.Price = f.Price;
            //commamnd
            UpdateCommand = new Command(OnUpdateClicked);
        }
        private void OnUpdateClicked()
        {
            if(!string.IsNullOrEmpty(Food.Name) && !string.IsNullOrWhiteSpace(Food.Name)
                && Food.Price>0)
            {
                Models.Food f = new Models.Food();
                f.ID = Food.ID;
                f.Name = Food.Name;
                f.Price = Food.Price;
                f.Desc = Food.Desc;
                f.IMG = Food.IMG;
                try
                {
                    _foodStore.UpdateFood(f);
                    _pageService.DisplayAlert("","Success!","ok");
                    _pageService.PopAsync();
                }
                catch (SQLite.SQLiteException e)
                {
                    _pageService.DisplayAlert("Alert!",$"Error: {e.Message}", "ok");
                }                
            }else
            {
                _pageService.DisplayAlert("Alert!","Lỗi cú pháp","ok");
            }
        }
        
    }
}
