using SnailBApp.Data.HoaDonData;
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
        public string DoanhThu
        {
            get => _doanhThu;
            set
            {
                SetProperty(ref _doanhThu, value);
            }
        }
        public ICommand LoadDataCommad { get; private set; }
        public ThongKeDoanhThuViewModel(IHoaDonStore hoaDonStore)
        {
            _hoaDonStore = hoaDonStore;
            LoadDataCommad = new Command(async () => await LoadData());
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
            string x = TotalMoney.ToString();
            DoanhThu = x;
            //
            for (int i = 0; i < lstHoaDonToFilter.Count; i++)
            {
                System.Console.WriteLine(lstHoaDonToFilter[i].Date.Substring(3, 4));
            }
        }
    }
}
