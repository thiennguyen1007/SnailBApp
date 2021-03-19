using SnailBApp.Data.NhanVienData;
using SnailBApp.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels.NhanVienVM
{
    class DetailNhanVienViewModel
    {
        private INhanVienStore _nhanVienStore;
        private IPageService _pageService;
        public NhanVienViewModel NhanVien { get; set; } = new NhanVienViewModel();
        public ICommand LoadDataCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public DetailNhanVienViewModel(NhanVienViewModel x, INhanVienStore nhanVienStore, IPageService pageService)
        {
            _nhanVienStore = nhanVienStore;
            _pageService = pageService;
            NhanVien = new NhanVienViewModel(x.ID, x.Name, x.Date, x.GioiTinh, x.Address, x.Desc, x.IMG, x.PhoneNumber);
            UpdateCommand = new Command(OnUpdateClicked);
        }
        private void OnUpdateClicked()
        {
            if (string.IsNullOrWhiteSpace(NhanVien.PhoneNumber))
            {
                _pageService.DisplayAlert("Failed!", "Number is invalid!", "ok");
            }
            else
            {
                try
                {
                    Models.NhanVien x = new Models.NhanVien();
                    x.ID = NhanVien.ID;
                    x.IMG = NhanVien.IMG;
                    x.Name = NhanVien.Name;
                    x.Desc = NhanVien.Desc;
                    x.GioiTinh = NhanVien.GioiTinh;
                    x.PhoneNumber = NhanVien.PhoneNumber;
                    x.Date = NhanVien.Date;
                    x.Address = NhanVien.Address;
                    _nhanVienStore.UpdateNhanVien(x);
                    _pageService.DisplayAlert("Success", "Updated.", "OK");
                    _pageService.PopAsync();
                }
                catch (System.Exception)
                {
                    _pageService.DisplayAlert("Error!", "Error when adding\nPlease wait for server.", "OK"); ;
                }
            }
        }
    }
}
