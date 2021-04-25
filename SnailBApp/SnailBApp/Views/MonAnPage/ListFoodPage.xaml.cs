using SnailBApp.Data;
using SnailBApp.Data.FoodData;
using SnailBApp.Services;
using SnailBApp.ViewModels.MonAnVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SnailBApp.Views.MonAnPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListFoodPage : ContentPage
    {
        public ListFoodPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var pageService = new PageService();
            var dataAccess = new SQLiteFoodStore(DependencyService.Get<ISQLite>());
            ViewModel = new ListFoodPageViewModel(dataAccess,pageService);
            base.OnAppearing();
            ViewModel.LoadDataCommand.Execute(null);
        }
        public ListFoodPageViewModel ViewModel
        {
            get { return BindingContext as ListFoodPageViewModel; }
            set { BindingContext = value; }
        }
        private void list_NV_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.OnLstSelectItem(e.SelectedItem as FoodViewModel);
        }
    }
}