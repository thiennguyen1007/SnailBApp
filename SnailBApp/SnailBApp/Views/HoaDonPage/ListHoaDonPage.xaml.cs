
using SnailBApp.Data;
using SnailBApp.Data.HoaDonData;
using SnailBApp.Services;
using SnailBApp.ViewModels.HoaDonVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SnailBApp.Views.HoaDonPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListHoaDonPage : ContentPage
    {
        public ListHoaDonPage()
        {
            var dataAccess = new SQLiteHoaDonStore(DependencyService.Get<ISQLite>());
            var pageService = new PageService();
            ViewModel = new ListHoaDonViewModel(dataAccess, pageService);
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadDataCommand.Execute(null);
        }
        public ListHoaDonViewModel ViewModel
        {
            get => BindingContext as ListHoaDonViewModel;
            set { BindingContext = value; }
        }
    }
}