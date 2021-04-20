using SnailBApp.Data.NhanVienData;
using SnailBApp.Models;
using SnailBApp.Services;
using SQLite;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels.NhanVienVM
{
    public class ListNhanVienPageViewModel: BaseViewModel
    {
        #region KhaiBao
        private readonly IPageService _pageService;
        private readonly INhanVienStore _nhanVienStore;
        private int _numberNV;
        public ICommand AddCommand { get; private set; }
        public ICommand LoadDataCommand { get; private set; }
        public ICommand FirstLstCommand { get; private set; }
        public ICommand BackLstCommand { get; private set; }
        public ICommand NextLstCommand { get; private set; }
        public ICommand LastLstCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        private ObservableCollection<NhanVienViewModel> _lstNhanViens;
        public ObservableCollection<NhanVienViewModel> LstNhanViens
        {
            get => _lstNhanViens;
            set { SetProperty(ref _lstNhanViens, value); }
        }
        public int NumberNV
        {
            get => _numberNV;
            set { SetProperty(ref _numberNV, value); }
        }
        #endregion
        public ListNhanVienPageViewModel(INhanVienStore nhanVienStore, IPageService pageService)
        {
            //Open connect SQLite, Page
            _nhanVienStore = nhanVienStore;
            _pageService = pageService;
            //Command
            AddCommand = new Command(OnAddClicked);
            LoadDataCommand = new Command(LoadData);
            DeleteCommand = new Command(OnDeleteClicked);
        }
        private async void LoadData()
        {
            LstNhanViens = new ObservableCollection<NhanVienViewModel>(KhoiTao());
            var nhanViens = await _nhanVienStore.GetNhanViensAsync();
            foreach (var item in nhanViens)
            {
                LstNhanViens.Add(new NhanVienViewModel(item));
            }
            NumberNV = LstNhanViens.Count;
        }
        private void OnAddClicked()// navigation to FillNhanVienPage.xaml
        {
            _pageService.PushAsync(new Views.NhanVienPage.FillNhanVienPage());
        }
        //selected item and navigation to DetailNhanVienPage; 
        public async System.Threading.Tasks.Task ItemSelectedAsync(NhanVienViewModel x)
        {
            PageService page = new PageService();
            if (x == null)
            {
                return;
            }
            else
            {
                await page.PushAsync(new Views.NhanVienPage.DetailNhanVien(x));
            }           
        }
        private async void OnDeleteClicked(object obj)
        {
            var x = obj as NhanVienViewModel;
            NhanVien nv = new NhanVien();
            nv.ID = x.ID;
            nv.Name = x.Name;
            nv.GioiTinh = x.GioiTinh;
            nv.IMG = x.IMG;
            nv.PhoneNumber = x.PhoneNumber;
            nv.Address = x.Address;
            nv.Date = x.Date;
            nv.Desc = x.Desc;
            try
            {
                if (await _pageService.DisplayAlert("Alert!", $"{x.Name} will be delete.\nAre you sure?", "Ok", "Cancel"))
                {
                    await _nhanVienStore.DeleteNhanVien(nv);
                    LstNhanViens.Remove(x);
                    await _pageService.DisplayAlert("","Success!","Ok");
                }
                else
                {
                    return;
                }
            }
            catch (SQLiteException e)
            {

               await _pageService.DisplayAlert("Failed!", "Failed when delete...\nError: " + e.Message, "ok");
            }
        }
        private ObservableCollection<NhanVienViewModel> KhoiTao()
        {
            ObservableCollection<NhanVienViewModel> x = new ObservableCollection<NhanVienViewModel>();
            NhanVienViewModel nv = new NhanVienViewModel
            {
                ID = 1,
                Name = "A Dat",
                GioiTinh = "Nam",
                Date = "01/01/1996",
                Address = "Hai Duong province",
                Desc = "Khi bong lung em lai nga ve ben doi hoa cuc, con duong in giau trong tim... & mua ha nam day em qua da chon day cam giac du day cua anh...",
                IMG = "https://images.unsplash.com/photo-1520451644838-906a72aa7c86?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=474&q=80",
                PhoneNumber = "0101020304",
            };
            NhanVienViewModel nv1 = new NhanVienViewModel
            {
                ID = 2,
                Name = "A Long",
                GioiTinh = "Nam",
                Date = "01/01/1997",
                Address = "pentHouse tren Da Lat",
                Desc = "Va ai day da quen ta da... doi khi tinh kai no chi vi co mot nu cuoi quanh san ",
                IMG = "https://images.unsplash.com/photo-1485110168560-69d4ac37b23e?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=500&q=80",
                PhoneNumber = "0103020304",
            };
            NhanVienViewModel nv2 = new NhanVienViewModel
            {
                ID = 3,
                Name = "A Bong",
                GioiTinh = "Nam",
                Date = "01/01/1996",
                Address = "Lao Cai",
                Desc = "Hoa cuc oi em to doi muoi sao chang biet cuoi, de ta tim cuoc tinh nam ay qua bao thang tram da phu reu phong...",
                IMG = "https://images.unsplash.com/photo-1516585427167-9f4af9627e6c?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=500&q=80",
                PhoneNumber = "0105020304",
            };
            x.Add(nv);
            x.Add(nv1);
            x.Add(nv2);
            return x;
        }
    }
}
