using SnailBApp.Data.HoaDonData;
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
            ObservableCollection<HoaDonVM.HoaDonViewModel> lstHoaDonToFilter = new ObservableCollection<HoaDonVM.HoaDonViewModel>();
            var lstHoaDon = await _hoaDonStore.GetHoaDonAsync();
            foreach (var item in lstHoaDon)
            {
                TotalMoney += item.Price;
                lstHoaDonToFilter.Add(new HoaDonVM.HoaDonViewModel(item));
                temp++;
            }
            float x = TotalMoney;
            DoanhThu = x.ToString();
        }
        private async void OnFilterClicked()
        {            
            if (Month > 12 || Month <= 0)
            {
                DoanhThu = "0\nKhông có dữ liệu";
            }
            else {
                float TotalMoney = 0;
                ObservableCollection<HoaDonVM.HoaDonViewModel> lstHoaDonToFilter = new ObservableCollection<HoaDonVM.HoaDonViewModel>();
                var lstHoaDon = await _hoaDonStore.GetHoaDonAsync();
                foreach (var item in lstHoaDon)
                {
                    lstHoaDonToFilter.Add(new HoaDonVM.HoaDonViewModel(item));
                }
                for(int i =0; i< lstHoaDonToFilter.Count; i++)
                {
                    if (int.Parse(lstHoaDonToFilter[i].Date.Substring(3, 2)) == Month)
                    {
                        TotalMoney += lstHoaDonToFilter[i].Price;
                    }
                }
                DoanhThu = TotalMoney.ToString();
            }
        }
    }
}
