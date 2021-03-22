using SnailBApp.Data;
using SnailBApp.Data.FoodData;
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
            var dataAccess = new SQLiteFoodStore(DependencyService.Get<ISQLite>());
            var pageService = new Services.PageService();
            ViewModel = new MyBagViewModel(x, dataAccess, pageService);
            this.BindingContext = ViewModel;
            InitializeComponent();
        }
    }
}