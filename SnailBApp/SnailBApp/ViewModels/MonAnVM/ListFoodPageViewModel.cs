using SnailBApp.Data.FoodData;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels.MonAnVM
{
    public class ListFoodPageViewModel : BaseViewModel
    {
        private IFoodStore _foodSore;
        private int _numberFood;
        //
        private ObservableCollection<FoodViewModel> _lstFoods;
        public ICommand LoadDataCommand { get; private set; }
        public ObservableCollection<FoodViewModel> LstFoods { get => _lstFoods; set => SetProperty(ref _lstFoods, value); }
        public int NumberFood { get => _numberFood; set => SetProperty(ref _numberFood, value); }

        public ListFoodPageViewModel(IFoodStore foodSore)
        {
            _foodSore = foodSore;
            LoadDataCommand = new Command(LoadData);
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
    }
}
