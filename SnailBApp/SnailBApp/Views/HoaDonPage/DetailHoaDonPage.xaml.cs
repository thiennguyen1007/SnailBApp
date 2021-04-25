using SnailBApp.Data;
using SnailBApp.Data.HoaDonData;
using SnailBApp.Services;
using SnailBApp.ViewModels.HoaDonVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SnailBApp.Views.HoaDonPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailHoaDonPage : ContentPage
    {
        public DetailHoaDonPage(HoaDonViewModel hd)
        {
            var pageService = new PageService();
            var dataAccess = new SQLiteHoaDonStore(DependencyService.Get<ISQLite>());
            ViewModel = new DetailHoaDonPageViewModel(dataAccess, hd, pageService);
            InitializeComponent();
        }
        private DetailHoaDonPageViewModel ViewModel
        {
            get => BindingContext as DetailHoaDonPageViewModel;
            set { BindingContext = value; }
        }
    }
}