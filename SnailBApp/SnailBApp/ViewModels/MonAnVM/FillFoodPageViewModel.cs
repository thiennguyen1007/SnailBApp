using SnailBApp.Data.FoodData;
using SnailBApp.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels.MonAnVM
{
    public class FillFoodPageViewModel : BaseViewModel
    {
        private IPageService _pageService;
        private IFoodStore _foodStore;
        private object collisionLock = new object();
        private string _id;
        private string _name;
        private string _price;
        private string _desc;

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
        public string Id { get => _id; set => SetProperty(ref _id, value); }
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public string Price { get => _price; set => SetProperty(ref _price, value); }
        public string Desc { get => _desc; set => SetProperty(ref _desc, value); }

        private void OnAddClicked()
        {
            if (Food != null)
            {
                if (Id != "" && Name != "" && Convert.ToSingle(Price) > 0 &&
                !string.IsNullOrEmpty(Id) && !string.IsNullOrWhiteSpace(Id)
                && !string.IsNullOrEmpty(Name) && !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrEmpty(Price) && !string.IsNullOrWhiteSpace(Price))
                {
                    Models.Food x = new Models.Food();
                    x.ID = int.Parse(Id);
                    x.Name = Name;
                    x.Price = Convert.ToSingle(Price);
                    x.IMG = IMG;
                    x.Desc = Desc;
                    //
                    try
                    {
                        lock (collisionLock)
                        {
                            _foodStore.AddFood(x);
                        }
                        _pageService.DisplayAlert("", "Success!", "ok");
                        _pageService.PopAsync();
                    }
                    catch (System.Exception e)
                    {

                        _pageService.DisplayAlert("Failed!", $"Error: {e.Message}", "ok");
                    }
                }
                else
                {
                    _pageService.DisplayAlert("Alert!", "Thiếu thông tin", "ok");
                }
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
