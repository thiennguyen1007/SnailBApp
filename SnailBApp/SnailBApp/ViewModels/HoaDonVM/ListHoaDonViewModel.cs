using SnailBApp.Data.HoaDonData;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels.HoaDonVM
{
    public class ListHoaDonViewModel :BaseViewModel
    {
        private readonly IHoaDonStore _hoaDonStore;
        private ObservableCollection<HoaDonViewModel> _lstHoaDon= new ObservableCollection<HoaDonViewModel>();
        //Command
        public ICommand LoadDataCommand { get; private set; }
        public ObservableCollection<HoaDonViewModel> LstHoaDon
        {
            get => _lstHoaDon;
            set { SetProperty(ref _lstHoaDon, value); }
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
            var nhanViens = await _hoaDonStore.GetHoaDonAsync();
            foreach (var item in nhanViens)
            {
                LstHoaDon.Add(new HoaDonViewModel(item));
            }
            //NumberHD = LstNhanViens.Count;
        }
    }
}
