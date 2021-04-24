using SnailBApp.Data.FoodData;
using SnailBApp.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels.MonAnVM
{
    public class FillFoodPageViewModel : BaseViewModel
    {
        private IPageService _pageService;
        private IFoodStore _foodStore;
        private object collisionLock = new object();
        //Command
        public ICommand AddCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }
        //
        private FoodViewModel _food;
        private string _iMG;
        public FillFoodPageViewModel(IPageService pageService, IFoodStore foodStore)
        {
            _pageService = pageService;
            _foodStore = foodStore;
            Food = new FoodViewModel();
            //
            AddCommand = new Command(OnAddClicked);
        }

        public FoodViewModel Food { get => _food; set => SetProperty(ref _food, value); }
        public string IMG { get => _iMG; set => SetProperty(ref _iMG, value); }
        private void OnAddClicked()
        {
            if (Food.ID.ToString() != "" && Food.Name.ToString() != "" && Food.Price > 0 &&
                !string.IsNullOrEmpty(Food.ID.ToString()) && !string.IsNullOrWhiteSpace(Food.ID.ToString())
                && !string.IsNullOrEmpty(Food.Name) && !string.IsNullOrWhiteSpace(Food.Name)
                && !string.IsNullOrEmpty(Food.Price.ToString()) && !string.IsNullOrWhiteSpace(Food.Price.ToString()))
            {
                Models.Food x = new Models.Food();
                x.ID = Food.ID;
                x.Name = Food.Name;
                x.Price = Food.Price;
                x.IMG = Food.IMG;
                x.Desc = Food.Desc;
                //
                try
                {
                    lock(collisionLock)
                    {
                        _foodStore.AddFood(x);
                    }
                    _pageService.DisplayAlert("", "Success!", "ok");
                    _pageService.PopAsync();
                }
                catch (System.Exception e)
                {

                    _pageService.DisplayAlert("Failed!", $"Error: {e.Message}","ok");
                }               
            }
            else
            {
                _pageService.DisplayAlert("Alert!","Thiếu thông tin", "ok");
            }
        }
        public void NumberUnfocus()
        {
            if (Food.Price.ToString().Equals("-"))
            {
                _pageService.DisplayAlert("Alert!", "Invalid Price of Food", "OK");
                Food.Price = 0;
            }
        }
    }
}
