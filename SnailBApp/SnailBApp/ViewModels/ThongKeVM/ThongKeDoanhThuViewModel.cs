using Microcharts;
using SkiaSharp;
using SnailBApp.Data.HoaDonData;
using SnailBApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SnailBApp.ViewModels.HoaDonVM;

namespace SnailBApp.ViewModels.ThongKeVM
{
    public class ThongKeDoanhThuViewModel : BaseViewModel
    {
        private readonly IHoaDonStore _hoaDonStore;
        private readonly IPageService _pageService;
        private string _doanhThu;
        private int _month;
        private int _year;
        private ObservableCollection<ChartEntry> _chart;
        public ObservableCollection<HoaDonViewModel> lstHoaDonToFilter { get; set; } = new ObservableCollection<HoaDonViewModel>();
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
        public ObservableCollection<ChartEntry> Chart
        {
            get => _chart;
            set { SetProperty(ref _chart, value); }
        }
        //=============================================================
        public ThongKeDoanhThuViewModel(IHoaDonStore hoaDonStore, IPageService pageService)
        {
            _hoaDonStore = hoaDonStore;
            _pageService = pageService;
            LoadDataCommad = new Command(async () => await LoadData());
            FilterCommand = new Command(OnFilterClicked);
        }
        private async Task LoadData()
        {
            float TotalMoney = default;
            var lstHoaDon = await _hoaDonStore.GetHoaDonAsync();
            foreach (var item in lstHoaDon)
            {
                TotalMoney += item.Price;
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
            else
            {
                float TotalMoney = 0;
                int dem = 0;
                ObservableCollection<HoaDonVM.HoaDonViewModel> lstHoaDonToFilter = new ObservableCollection<HoaDonVM.HoaDonViewModel>();
                var lstHoaDon = await _hoaDonStore.GetHoaDonAsync();
                foreach (var item in lstHoaDon)
                {
                    lstHoaDonToFilter.Add(new HoaDonVM.HoaDonViewModel(item));
                }
                for (int i = 0; i < lstHoaDonToFilter.Count; i++)
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
            //get list hoa don
            ObservableCollection<HoaDonViewModel> lstTemp = new ObservableCollection<HoaDonViewModel>();
            var lstHoaDon = await _hoaDonStore.GetHoaDonAsync();
            foreach (var item in lstHoaDon)
            {
                lstTemp.Add(new HoaDonViewModel(item));
            }
            lstHoaDonToFilter = new ObservableCollection<HoaDonViewModel>(lstTemp);
            //
            ObservableCollection<ChartEntry> chartTemp = new ObservableCollection<ChartEntry>();
            Chart = new ObservableCollection<ChartEntry>();
            ChartEntry chartEntry = new ChartEntry(default);
            System.Console.WriteLine($"{lstHoaDonToFilter.Count}");
            //add item to chart[]
            int index = lstHoaDonToFilter.Count;
            if (index == 0)
            {
                await _pageService.DisplayAlert("Alert!", "No data!", "OK");
            }
            else if (index < 3)
            {
                await _pageService.DisplayAlert("Alert!", "Can not draw chart!\nNeed more data!", "OK");
            }
            else
            {
                float janMoney, febMoney, marMoney, aprMoney, mayMoney, junMoney,
                    julMoney, augMoney, sepMoney, octMoney, novMoney, decMoney;
                janMoney = febMoney = marMoney = aprMoney = mayMoney = junMoney =
                julMoney = augMoney = sepMoney = octMoney = novMoney = decMoney = 0;
                for (int i = 0; i < index; i++)
                {
                    string check = lstHoaDonToFilter[i].Date.Substring(3, 2);
                    if (check.Equals("01"))
                    {
                        janMoney += lstHoaDonToFilter[i].Price;
                    }
                    else if (check.Equals("02"))
                    {
                        febMoney += lstHoaDonToFilter[i].Price;
                    }
                    else if (check.Equals("03"))
                    { marMoney += lstHoaDonToFilter[i].Price; }
                    else if (check.Equals("04"))
                    { aprMoney += lstHoaDonToFilter[i].Price; }
                    else if (check.Equals("05"))
                    { mayMoney += lstHoaDonToFilter[i].Price; }
                    else if (check.Equals("06"))
                    { junMoney += lstHoaDonToFilter[i].Price; }
                    else if (check.Equals("07"))
                    { julMoney += lstHoaDonToFilter[i].Price; }
                    else if (check.Equals("08"))
                    { augMoney += lstHoaDonToFilter[i].Price; }
                    else if (check.Equals("09"))
                    { sepMoney += lstHoaDonToFilter[i].Price; }
                    else if (check.Equals("10"))
                    { octMoney += lstHoaDonToFilter[i].Price; }
                    else if (check.Equals("11"))
                    { novMoney += lstHoaDonToFilter[i].Price; }
                    else if (check.Equals("12"))
                    { decMoney += lstHoaDonToFilter[i].Price; }
                }
                //month now, add
                int lastMonth = int.Parse(lstHoaDonToFilter[lstHoaDonToFilter.Count - 1].Date.Substring(3, 2));
                if (lastMonth == 1)
                {
                    chartEntry = new ChartEntry((float)janMoney)
                    {
                        ValueLabel = janMoney.ToString(),
                        Label = "Jan",
                        Color = SKColor.Parse("#FC0000"),
                    };
                    chartTemp.Add(chartEntry);
                }
                else if (lastMonth == 2)
                {
                    chartEntry = new ChartEntry((float)febMoney)
                    {
                        ValueLabel = febMoney.ToString(),
                        Label = "Feb",
                        Color = SKColor.Parse("#FC0000"),
                    };
                    chartTemp.Add(chartEntry);
                }
                else if (lastMonth == 3)
                {
                    chartEntry = new ChartEntry((float)marMoney)
                    {
                        ValueLabel = marMoney.ToString(),
                        Label = "Mar",
                        Color = SKColor.Parse("#FC0000"),
                    };
                    chartTemp.Add(chartEntry);
                }
                else if (lastMonth == 4)
                {
                    chartEntry = new ChartEntry((float)aprMoney)
                    {
                        ValueLabel = aprMoney.ToString(),
                        Label = "Apr",
                        Color = SKColor.Parse("#FC0000"),
                    };
                    chartTemp.Add(chartEntry);
                }
                else if (lastMonth == 5)
                {
                    chartEntry = new ChartEntry((float)mayMoney)
                    {
                        ValueLabel = mayMoney.ToString(),
                        Label = "May",
                        Color = SKColor.Parse("#FC0000"),
                    };
                    chartTemp.Add(chartEntry);
                }
                else if (lastMonth == 6)
                {
                    chartEntry = new ChartEntry((float)junMoney)
                    {
                        ValueLabel = junMoney.ToString(),
                        Label = "Jun",
                        Color = SKColor.Parse("#FC0000"),
                    };
                    chartTemp.Add(chartEntry);
                }
                else if (lastMonth == 7)
                {
                    chartEntry = new ChartEntry((float)julMoney)
                    {
                        ValueLabel = julMoney.ToString(),
                        Label = "Jul",
                        Color = SKColor.Parse("#FC0000"),
                    };
                    chartTemp.Add(chartEntry);
                }
                else if (lastMonth == 8)
                {
                    chartEntry = new ChartEntry((float)augMoney)
                    {
                        ValueLabel = augMoney.ToString(),
                        Label = "Aug",
                        Color = SKColor.Parse("#FC0000"),
                    };
                    chartTemp.Add(chartEntry);
                }
                else if (lastMonth == 9)
                {
                    chartEntry = new ChartEntry((float)sepMoney)
                    {
                        ValueLabel = sepMoney.ToString(),
                        Label = "Sep",
                        Color = SKColor.Parse("#FC0000"),
                    };
                    chartTemp.Add(chartEntry);
                }
                else if (lastMonth == 10)
                {
                    chartEntry = new ChartEntry((float)octMoney)
                    {
                        ValueLabel = octMoney.ToString(),
                        Label = "Oct",
                        Color = SKColor.Parse("#FC0000"),
                    };
                    chartTemp.Add(chartEntry);
                }
                else if (lastMonth == 11)
                {
                    chartEntry = new ChartEntry((float)novMoney)
                    {
                        ValueLabel = novMoney.ToString(),
                        Label = "Nov",
                        Color = SKColor.Parse("#FC0000"),
                    };
                    chartTemp.Add(chartEntry);
                }
                else if (lastMonth == 12)
                {
                    chartEntry = new ChartEntry((float)decMoney)
                    {
                        ValueLabel = decMoney.ToString(),
                        Label = "Dec",
                        Color = SKColor.Parse("#FC0000"),
                    };
                    chartTemp.Add(chartEntry);
                }
                //lùi month, add
                for (int i = 0; i < 5; i++)
                {
                    //giam thang
                    lastMonth--;
                    if (lastMonth == -1)
                    { lastMonth = 0; }
                    if (lastMonth == 0)
                    {
                        lastMonth = 12;
                    }
                    if (lastMonth == 1)
                    {
                        chartEntry = new ChartEntry((float)janMoney)
                        {
                            ValueLabel = janMoney.ToString(),
                            Label = "Jan",
                            Color = SKColor.Parse("#FC0000"),
                        };
                        chartTemp.Add(chartEntry);
                    }
                    else if (lastMonth == 2)
                    {
                        chartEntry = new ChartEntry((float)febMoney)
                        {
                            ValueLabel = febMoney.ToString(),
                            Label = "Feb",
                            Color = SKColor.Parse("#FC0000"),
                        };
                        chartTemp.Add(chartEntry);
                    }
                    else if (lastMonth == 3)
                    {
                        chartEntry = new ChartEntry((float)marMoney)
                        {
                            ValueLabel = marMoney.ToString(),
                            Label = "Mar",
                            Color = SKColor.Parse("#FC0000"),
                        };
                        chartTemp.Add(chartEntry);
                    }
                    else if (lastMonth == 4)
                    {
                        chartEntry = new ChartEntry((float)aprMoney)
                        {
                            ValueLabel = aprMoney.ToString(),
                            Label = "Apr",
                            Color = SKColor.Parse("#FC0000"),
                        };
                        chartTemp.Add(chartEntry);
                    }
                    else if (lastMonth == 5)
                    {
                        chartEntry = new ChartEntry((float)mayMoney)
                        {
                            ValueLabel = mayMoney.ToString(),
                            Label = "May",
                            Color = SKColor.Parse("#FC0000"),
                        };
                        chartTemp.Add(chartEntry);
                    }
                    else if (lastMonth == 6)
                    {
                        chartEntry = new ChartEntry((float)junMoney)
                        {
                            ValueLabel = junMoney.ToString(),
                            Label = "Jun",
                            Color = SKColor.Parse("#FC0000"),
                        };
                        chartTemp.Add(chartEntry);
                    }
                    else if (lastMonth == 7)
                    {
                        chartEntry = new ChartEntry((float)julMoney)
                        {
                            ValueLabel = julMoney.ToString(),
                            Label = "Jul",
                            Color = SKColor.Parse("#FC0000"),
                        };
                        chartTemp.Add(chartEntry);
                    }
                    else if (lastMonth == 8)
                    {
                        chartEntry = new ChartEntry((float)augMoney)
                        {
                            ValueLabel = augMoney.ToString(),
                            Label = "Aug",
                            Color = SKColor.Parse("#FC0000"),
                        };
                        chartTemp.Add(chartEntry);
                    }
                    else if (lastMonth == 9)
                    {
                        chartEntry = new ChartEntry((float)sepMoney)
                        {
                            ValueLabel = sepMoney.ToString(),
                            Label = "Sep",
                            Color = SKColor.Parse("#FC0000"),
                        };
                        chartTemp.Add(chartEntry);
                    }
                    else if (lastMonth == 10)
                    {
                        chartEntry = new ChartEntry((float)octMoney)
                        {
                            ValueLabel = octMoney.ToString(),
                            Label = "Oct",
                            Color = SKColor.Parse("#FC0000"),
                        };
                        chartTemp.Add(chartEntry);
                    }
                    else if (lastMonth == 11)
                    {
                        chartEntry = new ChartEntry((float)novMoney)
                        {
                            ValueLabel = novMoney.ToString(),
                            Label = "Nov",
                            Color = SKColor.Parse("#FC0000"),
                        };
                        chartTemp.Add(chartEntry);
                    }
                    else if (lastMonth == 12)
                    {
                        chartEntry = new ChartEntry((float)decMoney)
                        {
                            ValueLabel = decMoney.ToString(),
                            Label = "Dec",
                            Color = SKColor.Parse("#FC0000"),
                        };
                        chartTemp.Add(chartEntry);
                    }
                }
                //sap xep lai mang, thang nao truoc thang nao sau
                Chart = new ObservableCollection<ChartEntry>();
                Chart.Clear();
                for (int i = (chartTemp.Count-1); i >= 0; i--)
                {
                    Chart.Add(chartTemp[i]);
                }               
            }
            return Chart;
        }
    }
}
