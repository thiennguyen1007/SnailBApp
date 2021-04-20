using Microcharts;
using SnailBApp.Data;
using SnailBApp.Data.HoaDonData;
using SnailBApp.Services;
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
            InitializeComponent();           
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var hoaDonStore = new SQLiteHoaDonStore(DependencyService.Get<ISQLite>());
            var pageService = new PageService();
            ViewModel = new ThongKeDoanhThuViewModel(hoaDonStore, pageService);
            ViewModel.LoadDataCommad.Execute(null);           
            chartView.Chart = new LineChart
            {
                ValueLabelOrientation = Orientation.Horizontal,
                Entries = await ViewModel.LoadChartAsync(),
                LabelTextSize = 30
            };
        }
        private ThongKeDoanhThuViewModel ViewModel
        {
            get { return BindingContext as ThongKeDoanhThuViewModel; }
            set { BindingContext = value; }
        }
    }
}