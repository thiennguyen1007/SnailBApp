using SnailBApp.Data.NhanVienData;
using SnailBApp.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels.NhanVienVM
{
    public class FillNhanVienPageViewModel : BaseViewModel
    {
        private readonly INhanVienStore _nhanVienStore;
        private readonly IPageService _pageService;
        private static object collisionLock = new object();
        private bool _sex;
        public NhanVienViewModel NhanVien { get; set; } = new NhanVienViewModel();
        public ICommand AddCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }
        private string _img;
        public string IMG
        {
            get => _img;
            set
            {
                SetProperty(ref _img, value);
            }
        }
        public bool Sex
        {
            get => _sex;
            set
            {
                SetProperty(ref _sex, value);
            }
        }
        public FillNhanVienPageViewModel(INhanVienStore nhanVienStore, IPageService pageService)
        {
            //open connect SQLite, & Page
            _nhanVienStore = nhanVienStore;
            _pageService = pageService;
            //Command
            LoadCommand = new Command(OnLoadIMGClicked);
            AddCommand = new Command(OnAddClicked);
        }
        private void OnLoadIMGClicked()//Load Path image to show in page
        {
            IMG = NhanVien.IMG;
        }
        private void OnAddClicked()
        {
            if (string.IsNullOrWhiteSpace(NhanVien.Name) || string.IsNullOrWhiteSpace(NhanVien.PhoneNumber))// name or number is invalid, do nothing
            {
                _pageService.DisplayAlert("Failed", "Name or Phone number is Invalid!", "Ok");
            }
            else {
                if (Sex == true)
                {
                    NhanVien.GioiTinh = "Nam";
                }
                else
                {
                    NhanVien.GioiTinh = "Nu";
                }
                Models.NhanVien x = new Models.NhanVien();
                x.IMG = NhanVien.IMG;
                x.Name = NhanVien.Name;
                x.Desc = NhanVien.Desc;
                x.Date = NhanVien.Date;
                x.GioiTinh = NhanVien.GioiTinh;
                x.PhoneNumber = NhanVien.PhoneNumber;
                x.Address = NhanVien.Address;
                //add in SQL
                try
                {
                    lock (collisionLock)
                    {
                        _nhanVienStore.AddNhanVien(x);
                    }
                    _pageService.DisplayAlert("Success","Added.", "Ok");
                    //go back to list NhanVienPage
                    _pageService.PopAsync();
                }
                catch (System.Exception e)
                {
                    _pageService.DisplayAlert("Failed!", $"Error when adding!\n {e.Message}", "Ok");
                }               
            }           
        }
    }
}
