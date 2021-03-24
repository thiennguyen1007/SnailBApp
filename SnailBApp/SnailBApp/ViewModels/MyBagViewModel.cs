using SnailBApp.Data.HoaDonData;
using SnailBApp.Services;
using SnailBApp.ViewModels.MonAnVM;
using SQLite;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels
{
    public class MyBagViewModel : BaseViewModel
    {
        private readonly IPageService _pageService;
        private readonly IHoaDonStore _hoaDonStore;
        // binding to show in MyBag page
        private ObservableCollection<FoodViewModel> _lstBag;
        private float _totalMoney;
        private string _name;
        private string _phoneNumber;
        private static bool _isAnyItem;
        private HoaDonVM.HoaDonViewModel _hoaDon;
        public ICommand ThanhToanCommand { get; private set; }
        public ICommand DeleteCommand { get; set; }
        public ObservableCollection<FoodViewModel> LstBag
        {
            get { return _lstBag; }
            set { SetProperty(ref _lstBag, value); }
        }
        public float TotalMoney
        {
            get { return _totalMoney; }
            set { SetProperty(ref _totalMoney, value); }
        }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { SetProperty(ref _phoneNumber, value); }
        }
        public bool IsAnyItem
        {
            get { return _isAnyItem; }
            set { SetProperty(ref _isAnyItem, value); }
        }
        public HoaDonVM.HoaDonViewModel HoaDon
        {
            get => _hoaDon;
            set { SetProperty(ref _hoaDon, value); }
        }
        //================================================
        public MyBagViewModel(ObservableCollection<FoodViewModel> x,IHoaDonStore hoaDonStore, IPageService pageService)
        {
            //open connect SQLite database
            _pageService = pageService;
            _hoaDonStore = hoaDonStore;
            //Load
            HoaDon = new HoaDonVM.HoaDonViewModel();
            LoadData(x);
            //Command
            ThanhToanCommand = new Command(OnThanhToanClicked);
            DeleteCommand = new Command(OnDeleteClicked);
        }
        private ObservableCollection<FoodViewModel> LoadData(ObservableCollection<FoodViewModel> x)
        {
            LstBag = x;
            TotalMoney = CaculateMoney();
            IsBusy = BagIsAny();
            IsAnyItem = IsBusy;
            return LstBag;
        }
        private float CaculateMoney()//caculate total money of All item in Bag
        {
            float total = 0;
            if (LstBag != null)
            {
                foreach (var item in LstBag)
                {
                    total += item.Price;
                }
            }
            return total;
        }
        private void OnThanhToanClicked()
        {
            
            if (string.IsNullOrWhiteSpace(HoaDon.Email) || string.IsNullOrWhiteSpace(HoaDon.PhoneNumber))
            {
                _pageService.DisplayAlert("Failed", "Thanh Toan thất bại!\nFill all information in box", "OK");
            }
            else
            {
                string lstFoodString = "";
                foreach (var item in LstBag)
                {
                    lstFoodString += item.SL.ToString() + item.Name.ToString();
                }
                HoaDon.Date =DateTime.Now.ToString("dd-MM-yyyy");
                HoaDon.Price = TotalMoney;
                HoaDon.Foods = lstFoodString;
                Models.HoaDon hoaDonAdd = new Models.HoaDon();
                hoaDonAdd.Email = HoaDon.Email;
                hoaDonAdd.Foods = HoaDon.Foods;
                hoaDonAdd.Date = HoaDon.Date;
                hoaDonAdd.PhoneNumber = HoaDon.PhoneNumber;
                hoaDonAdd.Price = HoaDon.Price;
                try
                {
                    _hoaDonStore.AddHoaDon(hoaDonAdd);
                    _pageService.DisplayAlert("","Success!","OK");
                    LstBag.Clear();
                    //check to show PopUp "Thanh Toan" or not
                    IsAnyItem = false;
                    // delete list bag in orderPage.
                    OrderViewModel order = new OrderViewModel(null, null);
                    order.LstBagTemp.Clear();
                }
                catch (SQLiteException e)
                {
                    _pageService.DisplayAlert("Failed!",$"Failed! \n {e.Message}", "Ok") ;
                }
                
            }
        }
        private async void OnDeleteClicked(object obj)
        {
            var x = obj as FoodViewModel;
            if (await _pageService.DisplayAlert("Are you sure?", "Delete this food from your cart?", "OK", "Cancel"))
            {
                LstBag.Remove(x);
                TotalMoney -= x.Price;
                //check to show PopUp "Thanh Toan" or not
                IsBusy = BagIsAny();
                IsAnyItem = IsBusy;
                // delete list bag in orderPage.
                OrderViewModel order = new OrderViewModel(null, null);
                order.LstBagTemp.Remove(x);
            }
            else
            {
                return;
            }
        }
        private bool BagIsAny()// check number of item in List Bag
        {
            if (LstBag == null)
            {
                return false;
            }
            else if (LstBag.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
