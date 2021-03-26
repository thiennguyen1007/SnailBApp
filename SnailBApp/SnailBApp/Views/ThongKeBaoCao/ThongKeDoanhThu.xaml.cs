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
        //public ObservableCollection<ChartEntry> ChartEntries;
        public ThongKeDoanhThu()
        {
            var hoaDonStore = new SQLiteHoaDonStore(DependencyService.Get<ISQLite>());
            var pageService = new PageService();
            InitializeComponent();
            ViewModel = new ThongKeDoanhThuViewModel(hoaDonStore, pageService);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadDataCommad.Execute(null);
            
            chartView.Chart = new LineChart
            {
                ValueLabelOrientation = Orientation.Horizontal,
                Entries = await ViewModel.LoadChartAsync(),
                LabelTextSize = 30
            };
        }
        public ThongKeDoanhThuViewModel ViewModel
        {
            get { return BindingContext as ThongKeDoanhThuViewModel; }
            set { BindingContext = value; }
        }
    }
}