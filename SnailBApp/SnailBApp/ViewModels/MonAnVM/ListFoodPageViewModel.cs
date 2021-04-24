using SnailBApp.Data.FoodData;
using SnailBApp.Services;
using SnailBApp.Views.MonAnPage;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels.MonAnVM
{
    public class ListFoodPageViewModel : BaseViewModel
    {
        private IFoodStore _foodSore;
        private IPageService _pageService;
        private int _numberFood;
        //
        private ObservableCollection<FoodViewModel> _lstFoods;
        //commmand
        public ICommand LoadDataCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        //get set
        public ObservableCollection<FoodViewModel> LstFoods { get => _lstFoods; set => SetProperty(ref _lstFoods, value); }
        public int NumberFood { get => _numberFood; set => SetProperty(ref _numberFood, value); }
        //implement
        public ListFoodPageViewModel(IFoodStore foodSore, IPageService pageService)
        {
            _foodSore = foodSore;
            _pageService = pageService;
            LoadDataCommand = new Command(LoadData);
            AddCommand = new Command(OnAddClicked);
        }
        private async void LoadData()
        {
            OrderViewModel temp = new OrderViewModel(null, null);
            LstFoods = new ObservableCollection<FoodViewModel>(temp.LstKhoiTao());
            var foods = await _foodSore.GetFoodsAsync();
            foreach (var item in foods)
            {
                LstFoods.Add(new FoodViewModel(item));
            }
            NumberFood = LstFoods.Count;
        }
        private async void OnAddClicked()
        {
            await _pageService.PushAsync(new FillFoodPage());
        }
    }
}
