﻿using Microcharts;
using SkiaSharp;
using SnailBApp.Data.HoaDonData;
using SnailBApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SnailBApp.ViewModels.HoaDonVM;
using System.Linq;

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
            //
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
            DoanhThu = TotalMoney.ToString();
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
                    string lastTimeOfItem = x.Date.Substring(3, 7);
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
                        for (int i = 0; i < 5; i++)
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
                        double last_janMoney, last_febMoney, last_marMoney, last_aprMoney, last_mayMoney, last_junMoney,
                          last_julMoney, last_augMoney, last_sepMoney, last_octMoney, last_novMoney, last_decMoney;
                        last_janMoney = last_febMoney = last_marMoney = last_aprMoney = last_mayMoney = last_junMoney =
                        last_julMoney = last_augMoney = last_sepMoney = last_octMoney = last_novMoney = last_decMoney = 0;
                        int lastYear = nowYear--;
                        int tempMonth = nowMonth;
                        for(int i=0; i<index; i++)
                        {
                            int month = int.Parse(lstHoaDonToFilter[i].Date.Substring(3, 2));
                            int year = int.Parse(lstHoaDonToFilter[i].Date.Substring(6, 4));
                            if (year == lastYear)
                            {
                                if (month == 1)
                                {
                                    last_janMoney += lstHoaDonToFilter[i].Price;
                                }
                                else if (month == 2)
                                {
                                    last_febMoney += lstHoaDonToFilter[i].Price;
                                }
                                else if (month == 3)
                                {
                                    last_marMoney += lstHoaDonToFilter[i].Price;
                                }
                                else if (month == 4)
                                { last_aprMoney += lstHoaDonToFilter[i].Price; }
                                else if (month == 5)
                                { last_mayMoney += lstHoaDonToFilter[i].Price; }
                                else if (month == 6)
                                { last_junMoney += lstHoaDonToFilter[i].Price; }
                                else if (month == 7)
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
                        for (int i = 0; i < 5; i++)
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
                            else if (tempMonth == 8)
                            {
                                chartEntry = new ChartEntry((float)augMoney)
                                {
                                    ValueLabel = last_augMoney.ToString(),
                                    Label = "Aug",
                                };
                            }
                            else if (tempMonth == 9)
                            {
                                chartEntry = new ChartEntry((float)sepMoney)
                                {
                                    ValueLabel = last_sepMoney.ToString(),
                                    Label = "Sep",
                                };
                            }
                            else if (tempMonth == 10)
                            {
                                chartEntry = new ChartEntry((float)octMoney)
                                {
                                    ValueLabel = last_octMoney.ToString(),
                                    Label = "Oct",
                                };
                            }
                            else if (tempMonth == 11)
                            {
                                chartEntry = new ChartEntry((float)novMoney)
                                {
                                    ValueLabel = last_novMoney.ToString(),
                                    Label = "Nov",
                                };
                            }
                            else if (tempMonth == 12)
                            {
                                chartEntry = new ChartEntry((float)decMoney)
                                {
                                    ValueLabel = last_decMoney.ToString(),
                                    Label = "Dec",
                                };
                            }
                            chartTemp.Add(chartEntry);
                            tempMonth = tempMonth == 0 ? 12: tempMonth--;
                            //tempMonth--;
                        }
                    }
                    //sap xep lai mang, thang nao truoc thang nao sau
                    Chart = new ObservableCollection<ChartEntry>();
                    Chart.Clear();
                    for (int i = (chartTemp.Count - 1); i >= 0; i--)
                    {
                        Chart.Add(chartTemp[i]);
                    }
                }
            }
            return Chart;
        }
    }
}
