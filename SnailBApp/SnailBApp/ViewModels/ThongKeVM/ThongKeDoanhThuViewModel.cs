using Microcharts;
using SkiaSharp;
using SnailBApp.Data.HoaDonData;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels.ThongKeVM
{
    public class ThongKeDoanhThuViewModel : BaseViewModel
    {
        private readonly IHoaDonStore _hoaDonStore;
        private string _doanhThu;
        private int _month;
        private int _year;
        private static ObservableCollection<ChartEntry> _chart;
        public ObservableCollection<HoaDonVM.HoaDonViewModel> lstHoaDonToFilter { get; set; } = new ObservableCollection<HoaDonVM.HoaDonViewModel>();
        public string DoanhThu
        {
            get => _doanhThu;
            set
            {
                SetProperty(ref _doanhThu, value);
            }
        }
        public ICommand LoadDataCommad { get; private set; }
        public ICommand FilterCommand { get; private set; }
        public int Month
        {
            get => _month;
            set { SetProperty(ref _month, value); }
        }
        public int Year
        {
            get => _year;
            set { SetProperty(ref _year, value); }
        }
        public ObservableCollection<ChartEntry> Chart {
            get => _chart;
            set { SetProperty(ref _chart, value); }
        }
        //=============================================================
        public ThongKeDoanhThuViewModel(IHoaDonStore hoaDonStore)
        {
            _hoaDonStore = hoaDonStore;
            LoadDataCommad = new Command(async () => await LoadData());
            FilterCommand = new Command(OnFilterClicked);
        }
        private async Task LoadData()
        {
            float TotalMoney = default;
            int temp = 0;
            
            var lstHoaDon = await _hoaDonStore.GetHoaDonAsync();
            foreach (var item in lstHoaDon)
            {
                TotalMoney += item.Price;
                lstHoaDonToFilter.Add(new HoaDonVM.HoaDonViewModel(item));
                temp++;
            }
            float x = TotalMoney;
            DoanhThu = x.ToString();
            //         
        }
        private async void OnFilterClicked()
        {            
            if (Month > 12 || Month <= 0)
            {
                DoanhThu = "0\nKhông có dữ liệu";
            }
            else {
                float TotalMoney = 0;
                int dem = 0;
                ObservableCollection<HoaDonVM.HoaDonViewModel> lstHoaDonToFilter = new ObservableCollection<HoaDonVM.HoaDonViewModel>();
                var lstHoaDon = await _hoaDonStore.GetHoaDonAsync();
                foreach (var item in lstHoaDon)
                {
                    lstHoaDonToFilter.Add(new HoaDonVM.HoaDonViewModel(item));
                }
                for(int i =0; i< lstHoaDonToFilter.Count; i++)
                {
                    if (int.Parse(lstHoaDonToFilter[i].Date.Substring(3, 2)) == Month && int.Parse(lstHoaDonToFilter[i].Date.Substring(6, 4)) == Year)
                    {
                        TotalMoney += lstHoaDonToFilter[i].Price;
                        dem++;
                    }
                }
                if (dem == 0)
                {
                    DoanhThu = $"0\nNo data found for{Month}/{Year}";
                }
                DoanhThu = TotalMoney.ToString();
            }
        }
        public async Task<IEnumerable<ChartEntry>> LoadChartAsync()
        {
            await LoadData();
            ObservableCollection<ChartEntry> chartTemp = new ObservableCollection<ChartEntry>();
            Chart = new ObservableCollection<ChartEntry>();
            ChartEntry chartEntry = new ChartEntry(default);
            System.Console.WriteLine($"{lstHoaDonToFilter.Count}");
            //add item to chart[]
            for (int i = 0; i < lstHoaDonToFilter.Count; i++)
            {
                chartEntry.ValueLabel = lstHoaDonToFilter[i].Price.ToString();
                chartEntry = new ChartEntry((float)lstHoaDonToFilter[i].Price)
                {
                    ValueLabel = lstHoaDonToFilter[i].Price.ToString(),
                    Label = lstHoaDonToFilter[i].Date.Substring(3, 2),
                    Color = SKColor.Parse("#FC0000"),
                };
                chartTemp.Add(chartEntry);
                System.Console.WriteLine($"{chartTemp.Count}");
            }
            chartTemp[0].Color = SKColor.Parse("#FC0000");
            chartTemp[1].Color = SKColor.Parse("#08FF00");
            chartTemp[2].Color = SKColor.Parse("#1976D2");
            Chart = new ObservableCollection<ChartEntry>(chartTemp);
            return Chart;
        }
    }
}
