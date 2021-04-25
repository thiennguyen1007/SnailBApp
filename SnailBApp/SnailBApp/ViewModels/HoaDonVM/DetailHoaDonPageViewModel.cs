using SnailBApp.Data.HoaDonData;
using SnailBApp.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels.HoaDonVM
{
    public class DetailHoaDonPageViewModel : BaseViewModel
    {
        private IHoaDonStore _hoaDonStore;
        private IPageService _pageService;
        //
        private HoaDonViewModel _hoaDon;
        //Command
        public ICommand UpdateCommand { get; private set; }
        //
        public HoaDonViewModel HoaDon { get => _hoaDon; set => _hoaDon = value; }
        public DetailHoaDonPageViewModel(IHoaDonStore hoaDonStore, HoaDonViewModel hd, IPageService pageService)
        {
            _pageService = pageService;
            _hoaDonStore = hoaDonStore;
            //
            HoaDon = new HoaDonViewModel(hd);
            UpdateCommand = new Command(OnUpdateClicked);
        }
        private void OnUpdateClicked()
        {
            if (HoaDon.Price > 0)
            {
                try
                {
                    Models.HoaDon x = new Models.HoaDon();
                    x.ID = HoaDon.ID;
                    x.Email = HoaDon.Email;
                    x.PhoneNumber = HoaDon.PhoneNumber;
                    x.Price = HoaDon.Price;
                    x.Foods = HoaDon.Foods;
                    x.Date = HoaDon.Date;
                    //
                    _hoaDonStore.UpdateHoaDon(x);
                    _pageService.DisplayAlert("", "Success!", "ok");
                }
                catch (SQLite.SQLiteException e)
                {

                    _pageService.DisplayAlert("Alert!", $"Error: {e.Message}", "ok");
                }
            }
            else
            {
                _pageService.DisplayAlert("Error", "Lỗi cú pháp", "ok");
            }
        }
    }
}
