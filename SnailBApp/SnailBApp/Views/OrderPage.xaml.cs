using SnailBApp.Data;
using SnailBApp.Data.FoodData;
using SnailBApp.Services;
using SnailBApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SnailBApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : ContentPage
    {
        public OrderPage()
        {           
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var pageService = new PageService();
            var dataAccess = new SQLiteFoodStore(DependencyService.Get<ISQLite>());
            ViewModel = new OrderViewModel(dataAccess, pageService);
            ViewModel.LoadDataCommand.Execute(null);
        }
        protected override bool OnBackButtonPressed()
        {
            Application.Current.MainPage = new NavigationPage(new StartPage());                      
            return true;
        }
        public OrderViewModel ViewModel
        {
            get { return BindingContext as OrderViewModel; }
            set { BindingContext = value; }
        }
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.SearchChanged(e.NewTextValue);
        }
    }
}