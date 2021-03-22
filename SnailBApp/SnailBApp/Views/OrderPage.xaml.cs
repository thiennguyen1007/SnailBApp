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
            var pageService = new PageService();
            var dataAccess = new SQLiteFoodStore(DependencyService.Get<ISQLite>());
            this.BindingContext = new OrderViewModel(dataAccess, pageService);
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadDataCommand.Execute(null);
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