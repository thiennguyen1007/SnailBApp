using SnailBApp.Data.HoaDonData;
using SQLite;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels.HoaDonVM
{
    public class ListHoaDonViewModel :BaseViewModel
    {
        private readonly IHoaDonStore _hoaDonStore;
        private int _numberHD = 0;
        private ObservableCollection<HoaDonViewModel> _lstHoaDon= new ObservableCollection<HoaDonViewModel>();
        //Command
        public ICommand LoadDataCommand { get; private set; }
        public ObservableCollection<HoaDonViewModel> LstHoaDon
        {
            get => _lstHoaDon;
            set { SetProperty(ref _lstHoaDon, value); }
        }
        public int NumberHD
        {
            get => _numberHD;
            set { SetProperty(ref _numberHD, value); }
        }
        //=================================================================
        public ListHoaDonViewModel(IHoaDonStore hoaDonStore)
        {
            _hoaDonStore = hoaDonStore;
            //Command
            LoadDataCommand = new Command(LoadData);
        }
        private async void LoadData()
        {
            ObservableCollection<HoaDonViewModel> lstTemp = new ObservableCollection<HoaDonViewModel>();
            var nhanViens = await _hoaDonStore.GetHoaDonAsync();
            foreach (var item in nhanViens)
            {
                lstTemp.Add(new HoaDonViewModel(item));
            }
            LstHoaDon = new ObservableCollection<HoaDonViewModel>(lstTemp);
            NumberHD = LstHoaDon.Count;
        }
    }
}
