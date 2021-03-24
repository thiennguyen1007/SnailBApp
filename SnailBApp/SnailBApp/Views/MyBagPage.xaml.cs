using SnailBApp.Data;
using SnailBApp.Data.FoodData;
using SnailBApp.Data.HoaDonData;
using SnailBApp.ViewModels;
using SnailBApp.ViewModels.MonAnVM;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SnailBApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyBagPage : ContentPage
    {
        public MyBagViewModel ViewModel
        {
            get { return BindingContext as MyBagViewModel; }
            set { BindingContext = value; }
        }
        public MyBagPage(ObservableCollection<FoodViewModel> x)
        {
            //var dataAccessFood = new SQLiteFoodStore(DependencyService.Get<ISQLite>());
            var dataAccessHoaDon = new SQLiteHoaDonStore(DependencyService.Get<ISQLite>());
            var pageService = new Services.PageService();
            ViewModel = new MyBagViewModel(x, dataAccessHoaDon, pageService);
            this.BindingContext = ViewModel;
            InitializeComponent();
        }
    }
}