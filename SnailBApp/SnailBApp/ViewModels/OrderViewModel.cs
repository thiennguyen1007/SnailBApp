using SnailBApp.Data.FoodData;
using SnailBApp.Services;
using SnailBApp.ViewModels.MonAnVM;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        //to edit SQL
        private IFoodStore _foodStore;
        private IPageService _pageService;
        //Binding in OrderPage
        private ObservableCollection<FoodViewModel> _lstFoods;
        // store food to show in my bag
        private ObservableCollection<FoodViewModel> _lstBag;
        public ObservableCollection<FoodViewModel> LstBagTemp { get; set; } = new ObservableCollection<FoodViewModel>();       
        //to Load data, show data OrderPage
        public ICommand LoadDataCommand { get; private set; }
        //go to StartPage
        public ICommand BackCommand { get; private set; }
        //go to MyBagPage
        public ICommand MyBagCommand { get; private set; }
        //add Food item to cart
        public ICommand AddCommand { get; private set; }
        public ObservableCollection<FoodViewModel> LstFoods
        {
            get { return _lstFoods; }
            set { SetProperty(ref _lstFoods, value); }
        }
        public ObservableCollection<FoodViewModel> LstBag
        {
            get { return _lstBag; }
            set { SetProperty(ref _lstBag, value); }
        }
        
        //==================================================================
        public OrderViewModel(IFoodStore foodStore, IPageService pageService)
        {
            //Open connect SQLite
            _foodStore = foodStore;
            _pageService = pageService;
            //Command
            LoadDataCommand = new Command(async()=> await LoadData());
            AddCommand = new Command(OnAddClicked);
            BackCommand = new Command(OnBackClicked);
            MyBagCommand = new Command(OnBagClicked);
        }
        private async Task LoadData()
        {
            LstFoods = new ObservableCollection<FoodViewModel>(LstKhoiTao());
            var lstFoods = await _foodStore.GetFoodsAsync();
            foreach(var x in lstFoods)
            {
                FoodViewModel f = new FoodViewModel();
                f.ID = x.ID;
                f.IMG = x.IMG;
                f.Name = x.Name;
                f.Price = x.Price;
                f.Desc = x.Desc;
                LstFoods.Add(f);
            }
        }
        private async void OnBackClicked()
        {
            if (await _pageService.DisplayAlert("Are you sure!", "All Food in your cart will be clear.", "Ok", "Cancel"))
            {
                await Application.Current.MainPage.Navigation.PopToRootAsync();
                Application.Current.MainPage = new NavigationPage(new StartPage());
            }          
        }
        private async void OnBagClicked()
        {
            LstBag = new ObservableCollection<FoodViewModel>(LstBagTemp);        
            await _pageService.PushAsync(new Views.MyBagPage(LstBag));
        }
        private async void OnAddClicked(object obj)
        {
            int numberOfFood=default;
            float moneyOfItem=default;
            //get food & add to LstBag
            var x = obj as FoodViewModel;
            numberOfFood = x.SL;
            if (numberOfFood <= 0 || numberOfFood == default)//is true, notification error & do nothing
            {
                await _pageService.DisplayAlert("Failed!", "Number of Food is not invalid. ", "OK");
            }
            else // 1: list.count=0; 2: list.count>0 
            {
                if (LstBagTemp.Any() == false || LstBagTemp.Count == 0) //list bag =0, caculate MoneyOfItem & add Item Food to the listbag
                {
                    moneyOfItem = numberOfFood * x.Price;
                    x.Price = moneyOfItem;
                    LstBagTemp.Add(x);
                    await _pageService.DisplayAlert("Success", $"{x.SL} {x.Name} added to your cart.", "OK");
                }
                else
                {
                    int temp = 0;
                    for (int i = 0; i < LstBagTemp.Count; i++)// run check all item in list bag
                    {
                        if (x.ID == LstBagTemp[i].ID)// duplicate item is true, caculate numberOfFood & moneyOf item
                        {
                            LstBagTemp[i].SL += x.SL;
                            LstBagTemp[i].Price = LstBagTemp[i].SL * x.Price;
                            temp++;
                        }
                    }
                    if (temp == 0)//not duplicate => add & caculate
                    {
                        moneyOfItem = numberOfFood * x.Price;
                        x.Price = moneyOfItem;
                        LstBagTemp.Add(x);
                    }
                    await _pageService.DisplayAlert("Success", $"{x.SL} {x.Name} added your cart", "OK");
                }
                LstBag = new ObservableCollection<FoodViewModel>(LstBagTemp);              
            }
        }            
        private ObservableCollection<FoodViewModel> LstKhoiTao()
        {
            ObservableCollection<FoodViewModel> x = new ObservableCollection<FoodViewModel>();
            FoodViewModel f = new FoodViewModel
            {
                ID = 1,
                Name = "Chicken Rice",
                Desc = "Nhung con ga vung Tay A, mui vi that ngon va tuoi mat",
                IMG = "IMG05.png",
                Price = (float)23.99,
            };
            FoodViewModel f1 = new FoodViewModel
            {
                ID = 2,
                Name = "Banana Cream",
                Desc = "Kem chuối là món ngon, món quà vặt tuổi thơ quen thuộc của nhiều người.",
                IMG = "IMG04.png",
                Price = (float)10.99,
            };
            FoodViewModel f2 = new FoodViewModel
            {
                ID = 3,
                Name = "Juicy Chicken",
                Desc = "Sụn gà rang muối là một món ăn cực kỳ hấp dẫn, được nhiều người like.",
                IMG = "IMG01.png",
                Price = (float)23.99,
            };
            FoodViewModel f3 = new FoodViewModel
            {
                ID = 4,
                Name = "Pizza",
                Desc = "Nguyên liệu lúa mì thượng hạng của vùng Bắc Nga, công thức nấu độc nhâts.",
                IMG = "pizza.png",
                Price = (float)25.99,
            };
            // add to the list
            x.Add(f);
            x.Add(f1);
            x.Add(f2);
            x.Add(f3);
            return x;
        }
    }
}
