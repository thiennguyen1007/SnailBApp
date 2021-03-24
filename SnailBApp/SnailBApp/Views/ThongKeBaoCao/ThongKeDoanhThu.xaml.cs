using SnailBApp.Data;
using SnailBApp.Data.HoaDonData;
using SnailBApp.ViewModels.ThongKeVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SnailBApp.Views.ThongKeBaoCao
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThongKeDoanhThu : ContentPage
    {
        public ThongKeDoanhThu()
        {
            var hoaDonStore = new SQLiteHoaDonStore(DependencyService.Get<ISQLite>());           
            InitializeComponent();
            ViewModel = new ThongKeDoanhThuViewModel(hoaDonStore);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadDataCommad.Execute(null);
        }
        public ThongKeDoanhThuViewModel ViewModel
        {
            get { return BindingContext as ThongKeDoanhThuViewModel; }
            set { BindingContext = value; }
        }
    }
}