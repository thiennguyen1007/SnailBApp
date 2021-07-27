using SnailBApp.Data.HoaDonData;
using SnailBApp.Services;
using SnailBApp.Views.HoaDonPage;
using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels.HoaDonVM
{
    public class ListHoaDonViewModel : BaseViewModel
    {
        private readonly IHoaDonStore _hoaDonStore;
        private readonly IPageService _pageService;
        private int _numberHD = 0;
        private ObservableCollection<HoaDonViewModel> _lstHoaDon = new ObservableCollection<HoaDonViewModel>();
        //Command
        public ICommand LoadDataCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand StatusCommand { get; private set; }
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
        public ListHoaDonViewModel(IHoaDonStore hoaDonStore, IPageService pageService)
        {
            _hoaDonStore = hoaDonStore;
            _pageService = pageService;
            //Command
            LoadDataCommand = new Command(LoadData);
            DeleteCommand = new Command(OnDeleteClicked);
            StatusCommand = new Command<string>(OnStatusClicked);
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
        public void OnLstSelectItem(HoaDonViewModel hd)
        {
            _pageService.PushAsync(new DetailHoaDonPage(hd));
        }
        private async void OnDeleteClicked(object obj)
        {
            if (await _pageService.DisplayAlert("Alert!", "Are you sure!", "OK", "Cancel"))
            {
                var x = obj as HoaDonViewModel;
                Models.HoaDon hd = new Models.HoaDon();
                hd.ID = x.ID;
                hd.Email = x.Email;
                hd.Date = x.Date;
                hd.Foods = x.Foods;
                hd.PhoneNumber = x.PhoneNumber;
                hd.Price = x.Price;
                try
                {
                    await _hoaDonStore.DeleteHoaDon(hd);
                    LstHoaDon.Remove(x);
                    await _pageService.DisplayAlert("", "Success!", "Ok");
                }
                catch (SQLiteException e)
                {

                    await _pageService.DisplayAlert("Failed!", "Failed when delete...\nError: " + e.Message, "ok");
                }
            }
        }
        private void OnStatusClicked(string parameter)
        {
            //if(parameter=="+")
            //{

            //}
            //else
        }
        public void OnSearchTextChange(string newTxt)
        {
            if (!string.IsNullOrWhiteSpace(newTxt) && !string.IsNullOrEmpty(newTxt) && newTxt != "")
            {
                ObservableCollection<HoaDonViewModel> x = new ObservableCollection<HoaDonViewModel>();
                foreach (var item in SearchNhanVien(newTxt))
                {
                    x.Add(item);
                }
                LstHoaDon = new ObservableCollection<HoaDonViewModel>(x);
            }
            else
                LoadData();
        }
        private IEnumerable<HoaDonViewModel> SearchNhanVien(string txtNV)
        {
            ObservableCollection<HoaDonViewModel> temp = new ObservableCollection<HoaDonViewModel>(LstHoaDon);
            var x = new List<HoaDonViewModel>(temp);
            return x.Where(f => f.PhoneNumber.Contains(txtNV));
        }
    }
}
