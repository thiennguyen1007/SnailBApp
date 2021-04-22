using Microcharts;
using SnailBApp.Data.HoaDonData;
using SnailBApp.Services;
using SnailBApp.ViewModels.HoaDonVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels.ThongKeVM
{
    public class ThongKeDoanhThuViewModel : BaseViewModel
    {
        private readonly IHoaDonStore _hoaDonStore;
        private readonly IPageService _pageService;
        private string _doanhThu;
        private bool _statusChart;
        private string _month;
        private string _year;
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
        //command
        public ICommand LoadDataCommad { get; private set; }
        public ICommand FilterCommand { get; private set; }
        public ICommand ChartCommand { get; private set; }
        public string Month
        {
            get => _month;
            set { SetProperty(ref _month, value); }
        }
        public string Year
        {
            get => _year;
            set { SetProperty(ref _year, value); }
        }
        public ObservableCollection<ChartEntry> Chart
        {
            get => _chart;
            set { SetProperty(ref _chart, value); }
        }
        public bool StatusChart { get => _statusChart; set => SetProperty(ref _statusChart, value); }

        //=============================================================
        public ThongKeDoanhThuViewModel(IHoaDonStore hoaDonStore, IPageService pageService)
        {
            _hoaDonStore = hoaDonStore;
            _pageService = pageService;
            //
            LoadDataCommad = new Command(async () => await LoadData());
            FilterCommand = new Command(OnFilterClicked);
            ChartCommand = new Command<string>(OnStatusChartClicked);
        }
        private async Task LoadData()
        {
            float TotalMoney = default;
            StatusChart = true;
            var lstHoaDon = await _hoaDonStore.GetHoaDonAsync();
            foreach (var item in lstHoaDon)
            {
                TotalMoney += item.Price;
            }
            DoanhThu = TotalMoney.ToString();
        }
        private async void OnFilterClicked()
        {
            float TotalMoney = 0;
            if (string.IsNullOrEmpty(Month) || string.IsNullOrWhiteSpace(Month) || Month == "")// month is null
            {
                Month = null;
                if (!string.IsNullOrWhiteSpace(Year) && !string.IsNullOrEmpty(Year) && Year != "")
                {
                    int numberYear = int.Parse(Year);
                    int dem = 0;
                    ObservableCollection<HoaDonVM.HoaDonViewModel> lstHoaDonToFilter = new ObservableCollection<HoaDonVM.HoaDonViewModel>();
                    var lstHoaDon = await _hoaDonStore.GetHoaDonAsync();
                    foreach (var item in lstHoaDon)
                    {
                        lstHoaDonToFilter.Add(new HoaDonVM.HoaDonViewModel(item));
                    }
                    for (int i = 0; i < lstHoaDonToFilter.Count; i++)
                    {
                        if (int.Parse(lstHoaDonToFilter[i].Date.Substring(6, 4)) == numberYear)
                        {
                            TotalMoney += lstHoaDonToFilter[i].Price;
                            dem++;
                        }
                    }
                    if (dem == 0)
                    {
                        DoanhThu = $"0\nNo data found for{Month}/{Year}";
                    }
                }
            }
            else// month # null
            {
                if (!string.IsNullOrWhiteSpace(Year) && !string.IsNullOrEmpty(Month) && Year != "")
                {
                    int numberMonth = int.Parse(Month);
                    int numberYear = int.Parse(Year);
                    if (numberMonth > 12 || numberMonth <= 0)
                    {
                        await _pageService.DisplayAlert("", "invalid month -_-", "ok");
                    }else
                    {
                        int dem = 0;
                        ObservableCollection<HoaDonVM.HoaDonViewModel> lstHoaDonToFilter = new ObservableCollection<HoaDonVM.HoaDonViewModel>();
                        var lstHoaDon = await _hoaDonStore.GetHoaDonAsync();
                        foreach (var item in lstHoaDon)
                        {
                            lstHoaDonToFilter.Add(new HoaDonVM.HoaDonViewModel(item));
                        }
                        for (int i = 0; i < lstHoaDonToFilter.Count; i++)
                        {
                            if (int.Parse(lstHoaDonToFilter[i].Date.Substring(6, 4)) == numberYear && int.Parse(lstHoaDonToFilter[i].Date.Substring(3, 2)) == numberMonth)
                            {
                                TotalMoney += lstHoaDonToFilter[i].Price;
                                dem++;
                            }
                        }
                        if (dem == 0)
                        {
                            DoanhThu = $"0\nNo data found for{Month}/{Year}";
                        }
                    }
                }
                else
                {
                    await _pageService.DisplayAlert("", "insert year to filter...", "ok");
                }
                DoanhThu = TotalMoney.ToString();
            }
        }
        private void OnStatusChartClicked(string parameter)
        {
            if(parameter=="0")
            {
                StatusChart = true;
            }else
            { StatusChart = false; }
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
            if (lstHoaDonToFilter.Any())
            {
                int index = lstHoaDonToFilter.Count;
                if (index < 3)
                {
                    await _pageService.DisplayAlert("Alert!", "Can not draw chart!\nNeed more data!", "OK");
                }
                else
                {
                    double janMoney, febMoney, marMoney, aprMoney, mayMoney, junMoney,
                          julMoney, augMoney, sepMoney, octMoney, novMoney, decMoney;
                    janMoney = febMoney = marMoney = aprMoney = mayMoney = junMoney =
                    julMoney = augMoney = sepMoney = octMoney = novMoney = decMoney = 0;
                    HoaDonViewModel x = new HoaDonViewModel(lstHoaDonToFilter[lstHoaDonToFilter.Count - 1]);
                    int nowMonth = int.Parse(x.Date.Substring(3, 2));
                    int nowYear = int.Parse(x.Date.Substring(6, 4));
                    for (int i = 0; i < index; i++)
                    {
                        int month = int.Parse(lstHoaDonToFilter[i].Date.Substring(3, 2));
                        int year = int.Parse(lstHoaDonToFilter[i].Date.Substring(6, 4));
                        if (year == nowYear)
                        {
                            if (month == 1)
                            {
                                janMoney += lstHoaDonToFilter[i].Price;
                            }
                            else if (month == 2)
                            {
                                febMoney += lstHoaDonToFilter[i].Price;
                            }
                            else if (month == 3)
                            {
                                marMoney += lstHoaDonToFilter[i].Price;
                            }
                            else if (month == 4)
                            { aprMoney += lstHoaDonToFilter[i].Price; }
                            else if (month == 5)
                            { mayMoney += lstHoaDonToFilter[i].Price; }
                            else if (month == 6)
                            { junMoney += lstHoaDonToFilter[i].Price; }
                            else if (month == 7)
                            { julMoney += lstHoaDonToFilter[i].Price; }
                            else if (month == 8)
                            { augMoney += lstHoaDonToFilter[i].Price; }
                            else if (month == 9)
                            { sepMoney += lstHoaDonToFilter[i].Price; }
                            else if (month == 10)
                            { octMoney += lstHoaDonToFilter[i].Price; }
                            else if (month == 11)
                            { novMoney += lstHoaDonToFilter[i].Price; }
                            else if (month == 12)
                            { decMoney += lstHoaDonToFilter[i].Price; }
                        }
                    }
                    if (nowMonth >= 6)// ko can lui nam
                    {
                        //add
                        int tempMonth = nowMonth;
                        for (int i = 0; i < 6; i++)
                        {
                            if (tempMonth == 1)
                            {
                                chartEntry = new ChartEntry((float)janMoney)
                                {
                                    ValueLabel = janMoney.ToString(),
                                    Label = "Jan",
                                };
                            }
                            else if (tempMonth == 2)
                            {
                                chartEntry = new ChartEntry((float)febMoney)
                                {
                                    ValueLabel = febMoney.ToString(),
                                    Label = "Feb",
                                };
                            }
                            else if (tempMonth == 3)
                            {
                                chartEntry = new ChartEntry((float)marMoney)
                                {
                                    ValueLabel = marMoney.ToString(),
                                    Label = "Mar",
                                };
                            }
                            else if (tempMonth == 4)
                            {
                                chartEntry = new ChartEntry((float)aprMoney)
                                {
                                    ValueLabel = aprMoney.ToString(),
                                    Label = "Apr",
                                };
                            }
                            else if (tempMonth == 5)
                            {
                                chartEntry = new ChartEntry((float)mayMoney)
                                {
                                    ValueLabel = mayMoney.ToString(),
                                    Label = "May",
                                };
                            }
                            else if (tempMonth == 6)
                            {
                                chartEntry = new ChartEntry((float)junMoney)
                                {
                                    ValueLabel = junMoney.ToString(),
                                    Label = "Jun",
                                };
                            }
                            else if (tempMonth == 7)
                            {
                                chartEntry = new ChartEntry((float)julMoney)
                                {
                                    ValueLabel = julMoney.ToString(),
                                    Label = "Jul",
                                };
                            }
                            else if (tempMonth == 8)
                            {
                                chartEntry = new ChartEntry((float)augMoney)
                                {
                                    ValueLabel = augMoney.ToString(),
                                    Label = "Aug",
                                };
                            }
                            else if (tempMonth == 9)
                            {
                                chartEntry = new ChartEntry((float)sepMoney)
                                {
                                    ValueLabel = sepMoney.ToString(),
                                    Label = "Sep",
                                };
                            }
                            else if (tempMonth == 10)
                            {
                                chartEntry = new ChartEntry((float)octMoney)
                                {
                                    ValueLabel = octMoney.ToString(),
                                    Label = "Oct",
                                };
                            }
                            else if (tempMonth == 11)
                            {
                                chartEntry = new ChartEntry((float)novMoney)
                                {
                                    ValueLabel = novMoney.ToString(),
                                    Label = "Nov",
                                };
                            }
                            else if (tempMonth == 12)
                            {
                                chartEntry = new ChartEntry((float)decMoney)
                                {
                                    ValueLabel = decMoney.ToString(),
                                    Label = "Dec",
                                };
                            }
                            chartTemp.Add(chartEntry);
                            //giam thang
                            tempMonth--;
                        }
                    }
                    else// lui year and caculate
                    {
                        double last_julMoney, last_augMoney, last_sepMoney, last_octMoney, last_novMoney, last_decMoney;
                        last_julMoney = last_augMoney = last_sepMoney = last_octMoney = last_novMoney = last_decMoney = 0;
                        int lastYear = nowYear - 1;
                        int tempMonth = nowMonth;
                        for (int i = 0; i < index; i++)
                        {
                            int month = int.Parse(lstHoaDonToFilter[i].Date.Substring(3, 2));
                            int year = int.Parse(lstHoaDonToFilter[i].Date.Substring(6, 4));
                            if (year == lastYear)
                            {
                                if (month == 5)
                                { last_julMoney += lstHoaDonToFilter[i].Price; }
                                else if (month == 8)
                                { last_augMoney += lstHoaDonToFilter[i].Price; }
                                else if (month == 9)
                                { last_sepMoney += lstHoaDonToFilter[i].Price; }
                                else if (month == 10)
                                { last_octMoney += lstHoaDonToFilter[i].Price; }
                                else if (month == 11)
                                { last_novMoney += lstHoaDonToFilter[i].Price; }
                                else if (month == 12)
                                { last_decMoney += lstHoaDonToFilter[i].Price; }
                            }
                        }
                        for (int i = 0; i < 6; i++)
                        {
                            if (tempMonth == 0)
                            {
                                tempMonth = 12;
                            }
                            if (tempMonth == 1)
                            {
                                chartEntry = new ChartEntry((float)janMoney)
                                {
                                    ValueLabel = janMoney.ToString(),
                                    Label = "Jan",
                                };
                            }
                            else if (tempMonth == 2)
                            {
                                chartEntry = new ChartEntry((float)febMoney)
                                {
                                    ValueLabel = febMoney.ToString(),
                                    Label = "Feb",
                                };
                            }
                            else if (tempMonth == 3)
                            {
                                chartEntry = new ChartEntry((float)marMoney)
                                {
                                    ValueLabel = marMoney.ToString(),
                                    Label = "Mar",
                                };
                            }
                            else if (tempMonth == 4)
                            {
                                chartEntry = new ChartEntry((float)aprMoney)
                                {
                                    ValueLabel = aprMoney.ToString(),
                                    Label = "Apr",
                                };
                            }
                            else if (tempMonth == 5)
                            {
                                chartEntry = new ChartEntry((float)mayMoney)
                                {
                                    ValueLabel = mayMoney.ToString(),
                                    Label = "May",
                                };
                            }
                            else if (tempMonth == 8)
                            {
                                chartEntry = new ChartEntry((float)last_augMoney)
                                {
                                    ValueLabel = last_augMoney.ToString(),
                                    Label = "Aug",
                                };
                            }
                            else if (tempMonth == 9)
                            {
                                chartEntry = new ChartEntry((float)last_sepMoney)
                                {
                                    ValueLabel = last_sepMoney.ToString(),
                                    Label = "Sep",
                                };
                            }
                            else if (tempMonth == 10)
                            {
                                chartEntry = new ChartEntry((float)last_octMoney)
                                {
                                    ValueLabel = last_octMoney.ToString(),
                                    Label = "Oct",
                                };
                            }
                            else if (tempMonth == 11)
                            {
                                chartEntry = new ChartEntry((float)last_novMoney)
                                {
                                    ValueLabel = last_novMoney.ToString(),
                                    Label = "Nov",
                                };
                            }
                            else if (tempMonth == 12)
                            {
                                chartEntry = new ChartEntry((float)last_decMoney)
                                {
                                    ValueLabel = last_decMoney.ToString(),
                                    Label = "Dec",
                                };
                            }
                            chartTemp.Add(chartEntry);
                            tempMonth--;
                        }
                    }
                    //sap xep lai mang, thang nao truoc thang nao sau
                    Chart = new ObservableCollection<ChartEntry>();
                    Chart.Clear();
                    for (int i = (chartTemp.Count - 1); i >= 0; i--)
                    {
                        Chart.Add(chartTemp[i]);
                    }
                    //set mau
                    for(int i =0; i<5; i++)
                    {
                        for(int j=i+1; j<6; j++)
                        {
                            if(Chart[i].Value<Chart[j].Value)
                            {
                                Chart[j].Color= SkiaSharp.SKColor.Parse("#004DCF");
                            }else
                            {
                                Chart[j].Color = SkiaSharp.SKColor.Parse("#FF001D");
                            }
                        }
                    }
                    Chart[0].Color = SkiaSharp.SKColor.Parse("#FFF176");
                }
            }
            return Chart;
        }
    }
}
