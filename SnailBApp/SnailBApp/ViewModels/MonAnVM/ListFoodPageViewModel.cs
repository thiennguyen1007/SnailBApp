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
        public ICommand DeleteCommand { get; private set; }
        //get set
        public ObservableCollection<FoodViewModel> LstFoods { get => _lstFoods; set => SetProperty(ref _lstFoods, value); }
        public int NumberFood { get => _numberFood; set => SetProperty(ref _numberFood, value); }
        //implement
        public ListFoodPageViewModel(IFoodStore foodSore, IPageService pageService)
        {
            _foodSore = foodSore;
            _pageService = pageService;
            //command
            LoadDataCommand = new Command(LoadData);
            AddCommand = new Command(OnAddClicked);
            DeleteCommand = new Command(OnDeleteClicked);
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
        public void OnLstSelectItem(FoodViewModel f)
        {
            _pageService.PushAsync(new DetailFoodPage(f));
        }
        private async void OnDeleteClicked(object obj)
        {
            var x = obj as FoodViewModel;
            //
            Models.Food f = new Models.Food();
            f.ID = x.ID;
            f.Name = x.Name;
            f.Price = x.Price;
            f.Desc = x.Desc;
            f.IMG = x.IMG;
            //
            try
            {
                if (await _pageService.DisplayAlert("Alert!", $"{x.Name} will be delete.\nAre you sure?", "Ok", "Cancel"))
                {
                    await _foodSore.DeleteFood(f);
                    LstFoods.Remove(x);
                    await _pageService.DisplayAlert("", "Success!", "Ok");
                }
            }
            catch (System.Exception e)
            {
                await _pageService.DisplayAlert("Error!", $"Error: {e.Message}","ok");
            }
        }
    }
}
